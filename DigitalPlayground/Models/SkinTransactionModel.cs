using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class SkinTransactionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int SkinId { get; set; }
        public DateTime Date { get; set; }
        public SkinTransactionModel() { }

        public SkinTransactionModel(SkinTransaction entity)
        {
            Id = entity.Id;
            UserId = entity.UserId;
            Amount = entity.Amount;
            SkinId = entity.SkinId;
            Date = entity.Date;
        }
    }
}
