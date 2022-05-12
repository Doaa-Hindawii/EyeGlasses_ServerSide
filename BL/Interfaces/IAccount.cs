using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAccount
    {
        Task<Authentication_DTO> RegisterAsync(Register_DTO _DTO, bool isAdmin);
        Task<Authentication_DTO> LoginAsync(Login_DTO _DTO);
    }
}
