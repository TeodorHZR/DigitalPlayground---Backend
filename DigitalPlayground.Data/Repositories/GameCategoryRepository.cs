using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class GameCategoryRepository : IGameCategoryRepository
    {
        private readonly IConnectionString _connectionString;

        public GameCategoryRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddGameToCategory(GameCategory gameCategory)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO GameCategory (GameId, CategoryId) VALUES (@GameId, @CategoryId)";
            db.Connection.Execute(sql, gameCategory);
        }

        public void RemoveGameFromCategory(GameCategory gameCategory)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM GameCategory WHERE GameId = @GameId AND CategoryId = @CategoryId";
            db.Connection.Execute(sql, gameCategory);
        }

        public int[] GetCategoriesForGame(int gameId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT CategoryId FROM GameCategory WHERE GameId = @GameId";
            return db.Connection.Query<int>(sql, new { GameId = gameId }).ToArray();
        }

        public int[] GetGamesForCategory(int categoryId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT GameId FROM GameCategory WHERE CategoryId = @CategoryId";
            return db.Connection.Query<int>(sql, new { CategoryId = categoryId }).ToArray();
        }
    }
}
