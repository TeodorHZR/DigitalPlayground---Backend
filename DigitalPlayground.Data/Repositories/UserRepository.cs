using Dapper;
using DigitalPlayground.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalPlayground.Business.Domains;
using DigitalPlayground.Business.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace DigitalPlayground.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IConnectionString connectionString;

        public UserRepository(IConnectionString connectionString)
        {

            this.connectionString = connectionString;
        }
        public User GetByUsername(string username)
        {
            using var db = new SqlDataContext(connectionString);
            string query = "SELECT * FROM [User] WHERE Username = @Username";
            var parameters = new { Username = username };
            var result = db.Connection.Query<User>(query, parameters).FirstOrDefault();

            return result;
        }

        public int Insert(User user)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "INSERT INTO [User] (Username, Password, IsAdmin) VALUES (@Username, @Password, @IsAdmin); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, user);
        }
        public User GetById(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT * FROM User WHERE Id = @id";
            return db.Connection.Query<User>(sql, new { id }).FirstOrDefault();
        }
        public void Delete(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "DELETE FROM [User] WHERE Id = @id";
            db.Connection.Execute(sql, new { id });
        }

        public void UpdateAdminStatus(int userId, bool isAdmin)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "UPDATE [User] SET IsAdmin = @isAdmin WHERE Id = @userId";
            db.Connection.Execute(sql, new { userId, isAdmin });
        }

    }
}