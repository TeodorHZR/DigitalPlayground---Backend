using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class GameTransactionRepository : IGameTransactionRepository
    {
        private readonly IConnectionString _connectionString;

        public GameTransactionRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(GameTransaction gameTransaction)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO GameTransaction (UserId, Amount, GameId, Date) VALUES (@UserId, @Amount, @GameId, @Date); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, gameTransaction);
        }

        public GameTransaction GetById(int id)
        {
            try
            {
                using var db = new SqlDataContext(_connectionString);
                var sql = "SELECT * FROM gameTransaction WHERE Id = @Id";
                return db.Connection.QueryFirstOrDefault<GameTransaction>(sql, new { Id = id });

            }
            catch (Exception ex)
            {
                throw new Exception("Eroare la preluarea tranzacției de joc din baza de date.", ex);
            }
        }

        public void Update(GameTransaction gameTransaction)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE GameTransaction SET UserId = @UserId, Amount = @Amount, GameId = @GameId, Date = @Date WHERE Id = @Id";
            db.Connection.Execute(sql, gameTransaction);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM GameTransaction WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
        public IEnumerable<GameTransactionDetails> GetUserPurchasedGames(int userId, int offset, int limit)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
        SELECT g.Name, g.Description, g.Rating, gt.Amount, gt.Date
        FROM GameTransaction gt
        INNER JOIN Game g ON gt.GameId = g.Id
        WHERE gt.UserId = @UserId
        ORDER BY gt.Date DESC
        OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            return db.Connection.Query<GameTransactionDetails>(sql, new { UserId = userId, Offset = offset, Limit = limit }).ToList();
        }

        public int GetTotalUserPurchasedGames(int userId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
        SELECT COUNT(*)
        FROM GameTransaction
        WHERE UserId = @UserId";
            return db.Connection.ExecuteScalar<int>(sql, new { UserId = userId });
        }
        public IEnumerable<GameTransactionDetails> GetAllTransactions()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
            SELECT g.Name, g.Description, g.Rating, gt.Amount, gt.Date
            FROM GameTransaction gt
            INNER JOIN Game g ON gt.GameId = g.Id
            ORDER BY gt.Date DESC";
            return db.Connection.Query<GameTransactionDetails>(sql).ToList();
        }
        public IEnumerable<GameSalesStats> GetGameSalesStatistics()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
        SELECT g.Name, COUNT(*) as SalesCount
        FROM GameTransaction gt
        INNER JOIN Game g ON gt.GameId = g.Id
        GROUP BY g.Name
        ORDER BY SalesCount DESC";
            return db.Connection.Query<GameSalesStats>(sql).ToList();
        }

    }
}
