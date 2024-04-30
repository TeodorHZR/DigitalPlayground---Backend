using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class RefreshTokens
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsValid { get; set; }

        public RefreshTokens() { }

        public RefreshTokens(int id, int userId, string token, DateTime dateTime, bool isValid)
        {
            Id= id;
            UserId= userId;
            RefreshToken = token;
            ExpiresAt= dateTime;
            IsValid= isValid;
        }
    }
}
