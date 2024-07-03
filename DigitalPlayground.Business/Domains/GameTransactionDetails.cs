using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
   public class GameTransactionDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public GameTransactionDetails(string name, string description, decimal rating, decimal amount, DateTime date)
        {
            Name = name;
            Description = description;
            Rating = rating;
            Amount = amount;
            Date = date;
        }
    }
}
