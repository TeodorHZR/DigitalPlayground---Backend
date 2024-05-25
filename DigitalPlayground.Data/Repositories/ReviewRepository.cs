using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

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
        public int InsertOrUpdate(Review review)
        {
            using var db = new SqlDataContext(_connectionString);
            var existingReview = db.Connection.QueryFirstOrDefault<Review>(
                "SELECT * FROM Review WHERE GameId = @GameId AND UserId = @UserId",
                new { GameId = review.GameId, UserId = review.UserId });

            if (existingReview != null)
            {
                existingReview.Message = review.Message;
                existingReview.Rating = review.Rating;

                var sql = "UPDATE Review SET Message = @Message, Rating = @Rating WHERE Id = @Id";
                db.Connection.Execute(sql, existingReview);

                return existingReview.Id;
            }
            else
            {
                var sql = "INSERT INTO Review (Message, Rating, GameId, UserId) VALUES (@Message, @Rating, @GameId, @UserId); SELECT SCOPE_IDENTITY()";
                return db.Connection.ExecuteScalar<int>(sql, review);
            }
        }

        public List<string> GetReviewMessages(string gameName, int offset, int limit)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
        SELECT Message 
        FROM (
            SELECT A.Message, ROW_NUMBER() OVER (ORDER BY A.Id) AS RowNumber
            FROM Review AS A 
            INNER JOIN Game AS B ON A.GameId = B.Id 
            WHERE B.Name = @GameName
        ) AS SubQuery
        WHERE RowNumber > @Offset AND RowNumber <= @Offset + @Limit";
            return db.Connection.Query<string>(sql, new { GameName = gameName, Offset = offset, Limit = limit }).ToList();
        }
        public List<string> GetReviewMessagesByGame(string gameName)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"SELECT R.Message FROM Review R INNER JOIN Game G ON R.GameId = G.Id WHERE G.Name = @GameName; ";
            return db.Connection.Query<string>(sql, new { GameName = gameName}).ToList();
        }
    }
}
