using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class GameSalesStats
    {
        public string Name { get; set; }
        public int SalesCount { get; set; }

        public GameSalesStats() { }
        public GameSalesStats(string name, int salesCount)
        {
            Name = name;
            SalesCount = salesCount;
        }
    }
}
