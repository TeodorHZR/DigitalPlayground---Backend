using DigitalPlayground.Business.Domains;
namespace DigitalPlayground.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public float Money {  get; set; }
        public UserModel() { }
        public UserModel(User entity)
        {
            Id = entity.Id;
            Username = entity.Username;
            Password = entity.Password;
            IsAdmin = entity.IsAdmin;
            Money = entity.Money;
        }
    }
    
}
