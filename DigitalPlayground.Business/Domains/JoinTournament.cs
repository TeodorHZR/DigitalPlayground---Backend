using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class JoinTournament
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public JoinTournament() { }

        public JoinTournament(int joinTournamentId, int tournamentId, int userId)
        {
            Id = joinTournamentId;
            TournamentId = tournamentId;
            UserId = userId;
        }
    }
}
