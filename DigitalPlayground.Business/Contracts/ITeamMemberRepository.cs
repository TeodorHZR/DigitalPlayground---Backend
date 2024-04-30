using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface ITeamMemberRepository
    {
        void Insert(TeamMember teamMember);
        TeamMember GetById(int teamId, int userId);
        void Delete(int teamId, int userId);
        void Update(TeamMember teamMember);
        IEnumerable<User> GetPlayersByTeam(int teamId);
    }
}
