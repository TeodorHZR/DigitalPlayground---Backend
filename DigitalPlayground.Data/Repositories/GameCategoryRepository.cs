using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
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

        public void AddGameToCategory(int gameId, int categoryId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO GameCategory (GameId, CategoryId) VALUES (@GameId, @CategoryId)";
            db.Connection.Execute(sql, new { GameId = gameId, CategoryId = categoryId });
        }

        public void RemoveGameFromCategory(int gameId, int categoryId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM GameCategory WHERE GameId = @GameId AND CategoryId = @CategoryId";
            db.Connection.Execute(sql, new { GameId = gameId, CategoryId = categoryId });
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
