using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IConnectionString _connectionString;

        public TeamRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(Team team)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO Team (Name, Description) VALUES (@Name, @Description)";
            db.Connection.Execute(sql, team);
        }

        public Team GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Team WHERE Id = @Id";
            return db.Connection.QueryFirstOrDefault<Team>(sql, new { Id = id });
        }

        public IEnumerable<Team> GetAll()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Team";
            return db.Connection.Query<Team>(sql);
        }

        public void Update(Team team)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE Team SET Name = @Name WHERE Id = @Id";
            db.Connection.Execute(sql, team);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM Team WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
    }
}
