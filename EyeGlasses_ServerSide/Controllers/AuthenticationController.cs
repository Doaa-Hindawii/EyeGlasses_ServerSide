using BL.AppServices;
using BL.DTO;
using DAL.Models;
using BL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EyeGlasses_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<User_Identity> _userManager;
        private readonly IAccount _account;
        private readonly ShoppingCart_AppService _shoppingcart;

        public AuthenticationController(IAccount account, UserManager<User_Identity> userManager)
        {
            _account = account;
            _userManager = userManager;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Register_DTO register_DTO, bool isAdmin = false)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _account.RegisterAsync(register_DTO, isAdmin);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            var user = await _userManager.FindByNameAsync(register_DTO.User_Name);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login_DTO login_DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _account.LoginAsync(login_DTO);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
