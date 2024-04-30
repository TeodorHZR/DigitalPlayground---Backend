using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class TeamMember
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }

        public TeamMember() { }

        public TeamMember(int TeamId, int UserId)
        {
            this.TeamId = TeamId;
            this.UserId = UserId;
        }
    }
}
