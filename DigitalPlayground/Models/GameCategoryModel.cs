using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class GameCategoryModel
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }

        public GameCategoryModel() { }
        public GameCategoryModel(GameCategory entity)
        {
            GameId = entity.GameId;
            CategoryId = entity.CategoryId;
        }
    }
}
