using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User_Identity : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
