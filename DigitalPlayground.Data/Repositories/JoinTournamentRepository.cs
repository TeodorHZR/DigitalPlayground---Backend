using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class JoinTournamentRepository : IJoinTournamentRepository
    {
        private readonly IConnectionString _connectionString;

        public JoinTournamentRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(JoinTournament joinTournament)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO JoinTournament (TournamentId, UserId) VALUES (@TournamentId, @UserId); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, joinTournament);
        }

        public JoinTournament GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM JoinTournament WHERE Id = @Id";
            return db.Connection.Query<JoinTournament>(sql, new { Id = id }).FirstOrDefault();
        }

        public void Update(JoinTournament joinTournament)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE JoinTournament SET TournamentId = @TournamentId, UserId = @UserId WHERE Id = @Id";
            db.Connection.Execute(sql, joinTournament);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM JoinTournament WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
    }
}
