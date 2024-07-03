using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class User
    {
        public User() { }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public float Money {  get; set; }
        public void EncryptPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }
        public User(int userId, string username, string password, bool isAdmin, float money)
        {
            Id = userId;
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            Money = money;
        }
    }
}
