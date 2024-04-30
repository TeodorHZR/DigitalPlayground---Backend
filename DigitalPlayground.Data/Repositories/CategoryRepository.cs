using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using DigitalPlayground.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DigitalPlayground.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IConnectionString connectionString;

        public CategoryRepository(IConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Category> GetAll(int offset, int limit, string sortOrder)
        {
            using var db = new SqlDataContext(connectionString);

            var validSortOrders = new[] { "ASC", "DESC" };
            if (!validSortOrders.Contains(sortOrder.ToUpper()))
            {
                throw new ArgumentException("Invalid sortOrder.");
            }

            var sql = $"SELECT Id, Name FROM Category ORDER BY Name {sortOrder} OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY;";
            var list = db.Connection.Query<Category>(sql, new { offset, limit }).ToList();
            return list;
        }

        public Category GetById(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT [Id], [Name] FROM Category WHERE Id = @id";
            return db.Connection.Query<Category>(sql, new { id }).FirstOrDefault();
        }

        public int Insert(Category category)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "INSERT INTO Category (Name) VALUES (@Name); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, category);
        }


        public void Update(Category category)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "UPDATE Category set Name = @Name where Id = @Id";
            db.Connection.Execute(sql, category);
        }


        public void Delete(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "DELETE FROM Category WHERE Id = @id";
            db.Connection.Execute(sql, new { id });
        }

        public int GetId(string name)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT Id FROM Category WHERE Name = @name";
            return db.Connection.ExecuteScalar<int>(sql, new { name });
        }

        public List<Category> GetCategoriesPaginated(int page, int itemsPerPage, string sortOrder)
        {
            using var db = new SqlDataContext(connectionString);
            if (page <= 0 || itemsPerPage <= 0)
            {
                throw new ArgumentException("Invalid page or itemsPerPage values. They should be positive integers.");
            }

            var offset = (page - 1) * itemsPerPage;
            var sql = $"SELECT Id, Name FROM Category ORDER BY Name {sortOrder} OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY;";
            return db.Connection.Query<Category>(sql, new { offset, limit = itemsPerPage }).ToList();
        }
    }

}
