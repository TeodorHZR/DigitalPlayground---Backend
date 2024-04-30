using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface ITeamRepository
    {
        void Insert(Team team);
        Team GetById(int id);
        IEnumerable<Team> GetAll();
        void Update(Team team);
        void Delete(int id);

    }
}
