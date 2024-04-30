using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class TeamMemberModel
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }

        public TeamMemberModel() { }
        public TeamMemberModel(TeamMember entity)
        {
            TeamId = entity.TeamId;
            UserId = entity.UserId;
        }
    }
}
