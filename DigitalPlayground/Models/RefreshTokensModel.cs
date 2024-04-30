using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class RefreshTokensModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsValid { get; set; }
        public RefreshTokensModel() { }
        public RefreshTokensModel(RefreshTokens refreshToken)
        {
            Id = refreshToken.Id;
            UserId = refreshToken.UserId;
            RefreshToken = refreshToken.RefreshToken;
            ExpiresAt = refreshToken.ExpiresAt;
            IsValid = refreshToken.IsValid;

        }

    }
}
