using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class Team
   {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Team() { }
        public Team(int TeamId, string Name, string description)
        {
            this.Id = TeamId;
            this.Name = Name;
            Description = description;
        }
    }
}
