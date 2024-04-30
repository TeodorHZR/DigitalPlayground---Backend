using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class SkinModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
        public SkinModel() { }
        public SkinModel(Skin entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            UserId = entity.UserId;
            ImagePath = entity.ImagePath;
        }
    }
}
