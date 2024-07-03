using Dapper;
using DigitalPlayground.Business;
using DigitalPlayground.Business.Contracts;
using DigitalPlayground.Business.Domains;
using System.Data.SqlClient;
using System.Linq;

namespace DigitalPlayground.Data.Repositories
{
    public class SkinTransactionRepository : ISkinTransactionRepository
    {
        private readonly IConnectionString _connectionString;

        public SkinTransactionRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(SkinTransaction skinTransaction)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "INSERT INTO SkinTransaction (UserId, Amount, SkinId, Date) VALUES (@UserId, @Amount, @SkinId, @Date); SELECT SCOPE_IDENTITY()";
            return db.Connection.ExecuteScalar<int>(sql, skinTransaction);
        }

        public SkinTransaction GetById(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM SkinTransaction WHERE Id = @Id";
            return db.Connection.Query<SkinTransaction>(sql, new { Id = id }).FirstOrDefault();
        }

        public void Update(SkinTransaction skinTransaction)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "UPDATE SkinTransaction SET UserId = @UserId, Amount = @Amount, SkinId = @SkinId, Date = @Date WHERE Id = @Id";
            db.Connection.Execute(sql, skinTransaction);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "DELETE FROM SkinTransaction WHERE Id = @Id";
            db.Connection.Execute(sql, new { Id = id });
        }
        public IEnumerable<SkinTransaction> GetAll()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = "SELECT * FROM SkinTransaction";
            return db.Connection.Query<SkinTransaction>(sql);
        }

        public IEnumerable<Skin> GetTop3SkinsByPrice()
        {
            using var db = new SqlDataContext(_connectionString);
            var sql = @"
                SELECT TOP 3 s.*
                FROM SkinTransaction st
                INNER JOIN Skin s ON st.SkinId = s.Id
                ORDER BY s.Price DESC";
            return db.Connection.Query<Skin>(sql);
        }

    }
}
