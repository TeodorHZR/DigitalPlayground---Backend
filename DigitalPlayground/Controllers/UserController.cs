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

        [HttpGet("getall")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel model)
        {
            var user = _userRepository.GetByUsername(model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                return Unauthorized();
            }

            var jwtToken = GenerateJwtToken(user.Username);
            var refreshToken = GenerateRefreshToken();
            var expirationDate = DateTime.UtcNow.AddMinutes(_tokenExpirationMinutes);

            _refreshTokenRepository.SaveRefreshToken(user.Id, refreshToken);

            return Ok(new { jwtToken, refreshToken, expirationDate });
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
        [HttpGet("id-and-money/{username}")]
        public IActionResult GetIdAndMoneyByUsername(string username)
        {
            var (userId, money) = _userRepository.GetIdAndMoneyByUsername(username);
            if (userId == 0)
            {
                return NotFound($"Utilizatorul cu numele '{username}' nu a fost găsit.");
            }

            return Ok(new { UserId = userId, Money = money });
        }
        [HttpPost("insert")]
        public void Insert([FromBody] UserModel user)
        {
            var us = new User(user.Id, user.Username, user.Password, user.IsAdmin, user.Money);
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


        [HttpPut("{id}/updateMoney")]
        public IActionResult UpdateMoney(int id, [FromBody] UpdateMoneyModel model)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.UpdateMoney(id, model.UpdatedMoney);

            return Ok();
        }
        [HttpGet("{username}")]
        public IActionResult GetUserData(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }

        [HttpPut("{id}/updatePassword")]
        public IActionResult UpdatePassword(int id, [FromBody] UpdatePasswordModel model)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

            _userRepository.UpdatePassword(id, encryptedPassword);

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
