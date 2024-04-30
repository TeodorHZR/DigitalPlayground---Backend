using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public Game() { }
        public Game(int gameId, string name, string description, decimal price, decimal rating)
        {
            Id = gameId;
            Name = name;
            Description = description;
            Price = price;
            Rating = rating;
        }
    }
}
