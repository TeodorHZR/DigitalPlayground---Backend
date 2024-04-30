using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public GameModel() { }
        public GameModel(Game entity)
        {

            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Price = entity.Price;
            Rating = entity.Rating;
        }
    }
}
