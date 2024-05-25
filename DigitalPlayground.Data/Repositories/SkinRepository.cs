using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class SkinRepository : ISkinRepository
    {
        private readonly IConnectionString _connectionString;

        public SkinRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(Skin skin)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO Skin (Name, Description, UserId, ImagePath, IsForSale, Price, GameId) VALUES (@Name, @Description, @UserId, @ImagePath, 0, @Price, @GameId); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, skin);
        }

        public Skin GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Skin WHERE Id = @Id";
            return db.Connection.Query<Skin>(sql, new { Id = id }).FirstOrDefault();
        }

        public void Update(Skin skin)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE Skin SET Name = @Name, Description = @Description, UserId = @UserId, ImagePath = @ImagePath, IsForSale = @IsForSale, Price = @Price, GameId = @GameId WHERE Id = @Id";
            db.Connection.Execute(sql, skin);
        }
        public void UpdateUser(int skinId, int userId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE Skin SET UserId = @UserId, isForSale = @isForSale WHERE Id = @Id";
            db.Connection.Execute(sql, new { UserId = userId, isForSale = 0, Id = skinId });
        }

        public IEnumerable<Skin> GetAll()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Skin";
            return db.Connection.Query<Skin>(sql).ToList();
        }
        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM Skin WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }

        public IEnumerable<Skin> GetAllAvailableForUser(int userId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Skin WHERE isForSale = @isForSale AND userId = @userId";
            return db.Connection.Query<Skin>(sql, new { isForSale = 1, userId });
        }
        public IEnumerable<Skin> GetAllAvailableForGame(int gameId, int excludeUserId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Skin WHERE isForSale = @isForSale AND gameId = @gameId AND userId != @excludeUserId";
            return db.Connection.Query<Skin>(sql, new { isForSale = 1, gameId, excludeUserId });
        }
        public IEnumerable<Skin> GetSkinsByMaxPrice(float maxPrice, int gameId, int excludeUserId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Skin WHERE Price <= @MaxPrice AND isForSale = @isForSale AND gameId = @gameId AND userId != @excludeUserId";
            return db.Connection.Query<Skin>(sql, new { MaxPrice = maxPrice, isForSale = 1, gameId, excludeUserId }).ToList();
        }
        public IEnumerable<Skin> GetSkinsOrderedByPrice(bool ascending, int gameId, int excludeUserId)
        {
            using var db = new SqlDataContext(_connectionString);
            var orderBy = ascending ? "ASC" : "DESC";
            var sql = "SELECT * FROM Skin WHERE isForSale = @isForSale AND gameId = @gameId AND userId != @excludeUserId ORDER BY Price " + orderBy;
            return db.Connection.Query<Skin>(sql, new { isForSale = 1, gameId, excludeUserId }).ToList();
        }

    }
}
