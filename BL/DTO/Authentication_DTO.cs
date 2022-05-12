using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class Authentication_DTO
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string ID { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
