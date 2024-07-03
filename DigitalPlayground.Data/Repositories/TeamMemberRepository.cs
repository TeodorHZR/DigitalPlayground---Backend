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

        public void InsertOrUpdate(TeamMember teamMember)
        {
            using var db = new SqlDataContext(_connectionString);

            var sqlSelect = "SELECT COUNT(*) FROM TeamMember WHERE UserId = @UserId";
            var userExists = db.Connection.ExecuteScalar<int>(sqlSelect, new { teamMember.UserId }) > 0;

            if (userExists)
            {
                var sqlUpdate = "UPDATE TeamMember SET TeamId = @TeamId WHERE UserId = @UserId";
                db.Connection.Execute(sqlUpdate, teamMember);
            }
            else
            {
                var sqlInsert = "INSERT INTO TeamMember (TeamId, UserId) VALUES (@TeamId, @UserId)";
                db.Connection.Execute(sqlInsert, teamMember);
            }
        }


        public bool GetById(int teamId, int userId)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT COUNT(1) FROM TeamMember WHERE TeamId = @TeamId AND UserId = @UserId";
            var x =  db.Connection.ExecuteScalar<int>(sql, new { TeamId = teamId, UserId = userId }) > 0;
            return x;
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
