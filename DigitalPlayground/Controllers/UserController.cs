using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using DigitalPlayground.Models;
using DigitalPlayground.Business.Contracts;
using System.Security.Cryptography;
using System.Collections.Generic;
using DigitalPlayground.Business.Domains;
using DigitalPlayground.Data.Repositories;

namespace DigitalPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokensRepository _refreshTokenRepository;
        private readonly int _tokenExpirationMinutes = 15;


        public UserController(IUserRepository userRepository, IConfiguration configuration, IRefreshTokensRepository refreshTokensRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokensRepository;
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel model)
        {
            var user = _userRepository.GetByUsername(model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user.Username);
            var refreshToken = GenerateRefreshToken();

            _refreshTokenRepository.SaveRefreshToken(user.Id, refreshToken);

            return Ok(new { token, refreshToken });
        }


        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokensModel model)
        {
            RefreshTokens storedRefreshToken = _refreshTokenRepository.GetRefreshToken(model.RefreshToken);
            if (storedRefreshToken == null || !storedRefreshToken.IsValid || storedRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                
                return Unauthorized();
            }

            User user = _userRepository.GetById(storedRefreshToken.UserId);
            if (user == null)
            {
                return Unauthorized();
            }


            var token = GenerateJwtToken(user.Username);

            return Ok(new { token });
        }
        [HttpPost("insert")]
        public void Insert([FromBody] UserModel user)
        {
            var us = new User(user.Id, user.Username, user.Password, user.IsAdmin);
            us.EncryptPassword();
            user.Id = _userRepository.Insert(us);


        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}/updateAdminStatus")]
        public IActionResult UpdateAdminStatus(int id, bool isAdmin)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.UpdateAdminStatus(id, isAdmin);

            return Ok();
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
