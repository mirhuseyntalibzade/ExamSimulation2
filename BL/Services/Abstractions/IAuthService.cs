using BL.DTOs.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstractions
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDTO user);
        Task RegisterAsync(RegisterDTO user);
    }
}
