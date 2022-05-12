using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BL.Configuration;
using DAL.Models;
using Microsoft.AspNetCore.WebUtilities;
using AutoMapper;
using BL.DTO;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BL.AppServices;

namespace BL.Services
{
    public class Authentication_Service : IAccount
    {
        private readonly UserManager<User_Identity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly ShoppingCart_AppService _shoppingcart;


        public Authentication_Service(UserManager<User_Identity> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ShoppingCart_AppService shoppingcart)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _shoppingcart = shoppingcart;
        }

        [Obsolete]
        public async Task<Authentication_DTO> RegisterAsync(Register_DTO register_DTO, bool isAdmin)
        {
            var userbyEmail = await _userManager.FindByEmailAsync(register_DTO.Email);
            if (userbyEmail != null)
                return new Authentication_DTO { Message = "Email is already registered!" };
            var userbyName = await _userManager.FindByNameAsync(register_DTO.User_Name);

            if (userbyName != null)
                return new Authentication_DTO { Message = "Username is already registered!" };

            User_Identity user = new User_Identity
            {
                UserName = register_DTO.User_Name,
                Email = register_DTO.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, register_DTO.Password);
            IdentityResult roleResult;

            
            var role = "";
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new Authentication_DTO { Message = errors };
            }
            if (isAdmin)
            {
                // Add the Admin role to the database
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                role = "Admin";
                // add the Admin role to the user
                await _userManager.AddToRoleAsync(user,role);
            }
            else
            {
                // Add the User role to the database
                roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
                role = "User";
                // add the User role to the user
                await _userManager.AddToRoleAsync(user, role);
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            //Create Shopping Cart for this User
            ShoppingCart_DTO shoppingCart_DTO = new ShoppingCart_DTO() { User_ID = user.Id };
            _shoppingcart.CreateUserCart(shoppingCart_DTO.User_ID);

            return new Authentication_DTO
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                User_Name = user.UserName,
                ID = user.Id,
            };
        }

        public async Task<Authentication_DTO> LoginAsync(Login_DTO login_DTO)
        {
            var authModel = new Authentication_DTO();

            var user = await _userManager.FindByNameAsync(login_DTO.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, login_DTO.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.User_Name = user.UserName;
            authModel.ID = user.Id;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User_Identity user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret_Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Duration),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
