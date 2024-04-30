using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class GameTransactionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int GameId { get; set; }
        public DateTime Date { get; set; }

        public GameTransactionModel() { }
        public GameTransactionModel(GameTransaction entity)
        {

            Id = entity.Id;
            UserId = entity.UserId;
            Amount = entity.Amount;
            GameId = entity.GameId;
            Date = entity.Date;

        }
    }
}
