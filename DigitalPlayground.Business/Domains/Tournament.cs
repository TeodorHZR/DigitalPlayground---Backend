using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartingTime { get; set; }
        public float Prize {  get; set; }
        public Tournament(int tournamentId, string name, DateTime startingTime, float prize)
        {
            Id = tournamentId;
            Name = name;
            StartingTime = startingTime;
            Prize = prize;
        }
        public Tournament() { }
    }
}
