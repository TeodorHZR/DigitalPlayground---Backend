using DigitalPlayground.Business.Domains;

namespace DigitalPlayground.Models
{
    public class GameSalesStatsModel
    {
        public string Name { get; set; }
        public int SalesCount { get; set; }
        public GameSalesStatsModel() { }

        public GameSalesStatsModel(GameSalesStats entity)
        {
            Name = entity.Name;
            SalesCount = entity.SalesCount;
        }

    }
}
