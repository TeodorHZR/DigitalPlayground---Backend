using DigitalPlayground.Business.Domains;

namespace DigitalPlayground.Models
{
    public class GameTransactionDetailsModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public GameTransactionDetailsModel() { }

        public GameTransactionDetailsModel(GameTransactionDetails entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            Rating = entity.Rating;
            Amount = entity.Amount;
            Date = entity.Date;
        }
    }

}
