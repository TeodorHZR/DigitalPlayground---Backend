using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Domains
{
    public class GameCategory
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }

        public GameCategory() { }

        public GameCategory(int GameId, int CategoryId)
        {
            this.GameId = GameId;
            this.CategoryId = CategoryId;
        }
    }
}
