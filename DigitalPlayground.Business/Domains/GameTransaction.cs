using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class GameTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }
        public GameTransaction() { }

        public GameTransaction(int gameTransactionId, int userId, decimal amount, int gameId, DateTime date)
        {
            Id = gameTransactionId;
            UserId = userId;
            Amount = amount;
            GameId = gameId;
            Date = date;
        }
    }
}
