using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class SkinTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int SkinId { get; set; }
        public DateTime Date { get; set; }

        public SkinTransaction() { }

        public SkinTransaction(int skinTransactionId, int userId, decimal amount, int skinId, DateTime date)
        {
            Id = skinTransactionId;
            UserId = userId;
            Amount = amount;
            SkinId = skinId;
            Date = date;
        }
    }
}
