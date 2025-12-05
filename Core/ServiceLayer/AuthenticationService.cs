using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Exeptions;
using DomanLayer.Models.Identity_models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceApstractionLayer;
using Shared.DTOS.IdentityDtos;

namespace ServiceLayer
{
    public class AuthenticationService 
        (UserManager<ApplicationUser> _userManager,
        IConfiguration _configuration)
        : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //chek if email exist
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
                throw new UserNotFoundException(loginDto.Email);
            //check password
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    Email = User.Email!,
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
                throw new UnauthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Convert Dto Entity
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            var res= await _userManager.CreateAsync(User, registerDto.Password);
            if (res.Succeeded) return new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token =await CreateTokenAsync(User)
            };
            else
            {
                var Errors = res.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }
         
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim (ClaimTypes.Email,user.Email!),
                new Claim (ClaimTypes.Name,user.UserName!),
                new Claim (ClaimTypes.NameIdentifier,user.Id),

            };
            var roles =await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                     claims.Add(new Claim(ClaimTypes.Role, role));


            var secretKey = _configuration["JwtOptains:Issuer"];
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds =new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtOptains:Issuer"],
                audience: _configuration["JwtOptains:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

             return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
