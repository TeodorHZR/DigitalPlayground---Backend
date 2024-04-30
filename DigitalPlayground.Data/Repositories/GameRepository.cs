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
    public class GameRepository : IGameRepository
    {
        private IConnectionString connectionString;

        public GameRepository(IConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Game> GetAll(int offset, int limit)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT Id, Name, Description, Price, Rating FROM Game ORDER BY Id OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY;";
            var list = db.Connection.Query<Game>(sql, new { offset, limit }).ToList();
            return list;
        }

        public Game GetById(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT Id, Name, Description, Price, Rating FROM Game WHERE Id = @id";
            return db.Connection.QueryFirstOrDefault<Game>(sql, new { id });
        }

        public List<Game> GetByCategoryId(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "SELECT Id, Name, Description, Price, Rating FROM Game WHERE CategoryId = @id ORDER BY Id";
            var list = db.Connection.Query<Game>(sql, new { id }).ToList();
            return list;
        }

        public int Insert(Game game)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "INSERT INTO Game(Name, Description, Price, Rating) VALUES (@Name, @Description, @Price, @Rating); SELECT SCOPE_IDENTITY()";
            var gameId = db.Connection.ExecuteScalar<int>(sql, game);
            return gameId;
        }

        public void Update(Game game)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "UPDATE Game SET Name = @Name, Description = @Description, Price = @Price, Rating = @Rating WHERE Id = @Id";
            db.Connection.Execute(sql, game);
        }

        public void Delete(int id)
        {
            using var db = new SqlDataContext(connectionString);
            var sql = "DELETE FROM Game WHERE Id = @id";
            db.Connection.Execute(sql, new { id });
        }
    }
}
