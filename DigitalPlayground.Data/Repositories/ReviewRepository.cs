using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IConnectionString _connectionString;

        public ReviewRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(Review review)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO Review (Message, Rating, GameId, UserId) VALUES (@Message, @Rating, @GameId, @UserId); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, review);
        }

        public Review GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Review WHERE Id = @Id";
            return db.Connection.Query<Review>(sql, new { Id = id }).FirstOrDefault();
        }

        public void Update(Review review)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE Review SET Message = @Message, Rating = @Rating, GameId = @GameId, UserId = @UserId WHERE Id = @Id";
            db.Connection.Execute(sql, review);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM Review WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
    }
}
