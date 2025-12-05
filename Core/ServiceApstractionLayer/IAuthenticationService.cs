using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.IdentityDtos;

namespace ServiceApstractionLayer
{
    public interface IAuthenticationService
    {
        //Login User
        Task<UserDto> LoginAsync(LoginDto loginDto);
        //Register User
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

    }
}
