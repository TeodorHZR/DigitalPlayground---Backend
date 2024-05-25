using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TeamModel() { }
        public TeamModel(Team entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
