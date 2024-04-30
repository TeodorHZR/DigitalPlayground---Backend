using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryModel() { }


        public CategoryModel(Category entity)
        {

            Id = entity.Id;
            Name = entity.Name;

        }
    }
}
