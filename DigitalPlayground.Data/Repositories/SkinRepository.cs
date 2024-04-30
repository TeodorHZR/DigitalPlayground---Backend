using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class SkinRepository : ISkinRepository
    {
        private readonly IConnectionString _connectionString;

        public SkinRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(Skin skin)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO Skin (Name, Description, UserId, ImagePath) VALUES (@Name, @Description, @UserId, @ImagePath); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, skin);
        }

        public Skin GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM Skin WHERE Id = @Id";
            return db.Connection.Query<Skin>(sql, new { Id = id }).FirstOrDefault();
        }

        public void Update(Skin skin)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE Skin SET Name = @Name, Description = @Description, UserId = @UserId, ImagePath = @ImagePath WHERE Id = @Id";
            db.Connection.Execute(sql, skin);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM Skin WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
    }
}
