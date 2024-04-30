using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class JoinTournamentModel
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int UserId { get; set; }

        public JoinTournamentModel() { }
        public JoinTournamentModel(JoinTournament entity)
        {
            Id = entity.Id;
            TournamentId = entity.TournamentId;
            UserId = entity.UserId;
        }
    }
}
