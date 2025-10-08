using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMS.app.Dtos;
using TMS.app.Interfaces;
using TMS.core.Entities;

namespace TMS.app.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<AuthResponseDto> Login(LoginDto userDto)
        {
            var user = await _repository.GetUser(userDto.Username, userDto.Password);
            if (user == null)
                throw new Exception("Invalid username or password");
            var token = GenerateToken(user);

            return new AuthResponseDto(token);
        }

        public async Task<AuthResponseDto> Register(RegisterDto registerDto)
        {
            var userExist = await _repository.UserExists(registerDto.Username);

            if (!userExist)
            {
                var Newuser = new User
                {
                    Username = registerDto.Username,
                    Password = registerDto.Password
                };
                await _repository.CreateUser(Newuser);
                var token = GenerateToken(Newuser);
                return new AuthResponseDto(token);
            }
            else
                throw new Exception("User already exists");
        }
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("username", user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BrightRiverStoneMagicForestLight"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "TMS Api",
                audience: "TMS Frontend",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
