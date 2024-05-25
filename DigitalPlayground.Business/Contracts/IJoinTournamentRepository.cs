using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface IJoinTournamentRepository
    {
        int InsertOrUpdate(JoinTournament joinTournament);
        JoinTournament GetById(int id);
        void Update(JoinTournament joinTournament);
        void Delete(int id);

    }
}
