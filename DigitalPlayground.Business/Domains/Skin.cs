using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class Skin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
        public Skin() { }

        public Skin(int skinId, string name, string description, int userId, string imagePath)
        {
            Id = skinId;
            Name = name;
            Description = description;
            UserId = userId;
            ImagePath = imagePath;

        }
    }
}
