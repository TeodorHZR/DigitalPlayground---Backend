using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using DigitalPlayground.Data;

namespace DigitalPlayground.Repositories
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly IConnectionString _connectionString;

        public RefreshTokensRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO RefreshTokens (UserId, RefreshToken, ExpiresAt, IsValid) VALUES (@UserId, @RefreshToken, @ExpiresAt, @IsValid)";
                var parameters = new
                {
                    UserId = userId,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    IsValid = true
                };
                db.Connection.Execute(sql, parameters);
            
        }
        public RefreshTokens GetRefreshToken(string refreshToken)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM RefreshTokens WHERE RefreshToken = @RefreshToken";
            return db.Connection.QueryFirstOrDefault<RefreshTokens>(sql, new { RefreshToken = refreshToken });
            
        }
    }
}
