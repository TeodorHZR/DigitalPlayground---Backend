using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly IConnectionString _connectionString;

        public TournamentRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(Tournament tournament)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO Tournament (Name, StartingTime, prize) VALUES (@Name, @StartingTime, @Prize); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, tournament);
        }

        public Tournament GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Tournament WHERE Id = @Id";
            return db.Connection.QueryFirstOrDefault<Tournament>(sql, new { Id = id });
        }

        public IEnumerable<Tournament> GetAll()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Tournament WHERE startingTime > @CurrentDateTime";
            return db.Connection.Query<Tournament>(sql, new { CurrentDateTime = DateTime.Now });
        }

        public void Update(Tournament tournament)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE Tournament SET Name = @Name, StartingTime = @StartingTime WHERE Id = @Id";
            db.Connection.Execute(sql, tournament);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM Tournament WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
        public Tournament GetUpcomingTournament()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
                SELECT TOP 1 * 
                FROM Tournament 
                WHERE startingTime > @CurrentDateTime 
                ORDER BY startingTime ASC";
            return db.Connection.QueryFirstOrDefault<Tournament>(sql, new { CurrentDateTime = DateTime.Now });
        }
    }
}
