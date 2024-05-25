using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface ITournamentRepository
    {
        int Insert(Tournament tournament);
        Tournament GetById(int id);
        IEnumerable<Tournament> GetAll();
        void Update(Tournament tournament);
        void Delete(int id);
        Tournament GetUpcomingTournament();

    }
}
