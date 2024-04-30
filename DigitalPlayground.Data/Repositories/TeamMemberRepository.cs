using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly IConnectionString _connectionString;

        public TeamMemberRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(TeamMember teamMember)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO TeamMember (TeamId, UserId) VALUES (@TeamId, @UserId)";
            db.Connection.Execute(sql, teamMember);
        }

        public TeamMember GetById(int teamId, int userId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM TeamMember WHERE TeamId = @TeamId AND UserId = @UserId";
            return db.Connection.QueryFirstOrDefault<TeamMember>(sql, new { TeamId = teamId, UserId = userId });
        }

        public void Delete(int teamId, int userId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM TeamMember WHERE TeamId = @TeamId AND UserId = @UserId";
            db.Connection.Execute(sql, new { TeamId = teamId, UserId = userId });
        }
        public void Update(TeamMember teamMember)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE TeamMember SET TeamId = @TeamId WHERE UserId = @UserId";
            db.Connection.Execute(sql, teamMember);
        }

        public IEnumerable<User> GetPlayersByTeam(int teamId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"SELECT u.* 
                        FROM TeamMember tm
                        INNER JOIN [User] u ON tm.UserId = u.Id
                        WHERE tm.TeamId = @TeamId";
            return db.Connection.Query<User>(sql, new { TeamId = teamId });
        }
    }
}
