using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }

        public ReviewModel() { }
        public ReviewModel(Review entity)
        {

            Id = entity.Id;
            Message = entity.Message;
            Rating = entity.Rating;
            GameId = entity.GameId;
            UserId = entity.UserId;
        }
    }
}
