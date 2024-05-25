using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartingTime { get; set; }
        public float Prize { get; set; }
        public TournamentModel() { }
        public TournamentModel(Tournament entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            StartingTime = entity.StartingTime;
            Prize = entity.Prize;
        }

    }
}
