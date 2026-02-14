
using DomanLayer.Models.Identity_models;
using Shared.DTOS.IdentityDtos;

namespace ServiceApstractionLayer
{
    public interface IAuthenticationService
    {
        //Login User
        Task<UserDto> LoginAsync(LoginDto loginDto);
        //Register User
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> CheckEmailAsync(string email);

        Task<UserDto> GetCurrentUser(string email);

        Task<Address> GetUserAddress(string email);

        Task<Address> UpdateCurrentUserAddress(string email, Address address);

    }
}
