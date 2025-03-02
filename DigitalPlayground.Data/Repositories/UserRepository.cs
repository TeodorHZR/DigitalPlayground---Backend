﻿using Dapper;
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
            var sql = "INSERT INTO [User] (Username, Password, IsAdmin) VALUES (@Username, @Password, 0); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, user);
        }
        public User GetById(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT * FROM [User] WHERE Id = @id";
            return db.Connection.Query<User>(sql, new { id }).FirstOrDefault();
        }
        public void Delete(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sqll = "DELETE FROM [RefreshTokens] WHERE Userid = @id";
            db.Connection.Execute(sqll, new { id });
            var sql = "DELETE FROM [User] WHERE Id = @id";
            db.Connection.Execute(sql, new { id });
        }
        public (int Id, float Money) GetIdAndMoneyByUsername(string username)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT Id, [Money] FROM [User] WHERE Username = @Username";
            return db.Connection.QueryFirstOrDefault<(int, float)>(sql, new { Username = username });
        }

        public void UpdateAdminStatus(int userId, bool isAdmin)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "UPDATE [User] SET IsAdmin = @isAdmin WHERE Id = @userId";
            db.Connection.Execute(sql, new { userId, isAdmin });
        }
        public void UpdateMoney(int userId, float updatedMoney)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "UPDATE [User] SET [Money] = @updatedMoney WHERE Id = @userId";
            db.Connection.Execute(sql, new { userId, updatedMoney });
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "UPDATE [User] SET Password = @newPassword WHERE Id = @userId";
            db.Connection.Execute(sql, new { userId, newPassword });
        }
        public IEnumerable<User> GetAll()
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT * FROM [User]";
            return db.Connection.Query<User>(sql).ToList();
        }

    }
}