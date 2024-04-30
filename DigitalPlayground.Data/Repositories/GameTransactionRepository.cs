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
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM GameTransaction WHERE Id = @Id";
            return db.Connection.Query<GameTransaction>(sql, new { Id = id }).FirstOrDefault();
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
    }
}
