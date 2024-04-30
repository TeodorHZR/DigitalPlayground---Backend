using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface IGameRepository
    {
        List<Game> GetAll(int offset, int limit);
        Game GetById(int id);
        List<Game> GetByCategoryId(int id);
        int Insert(Game game);
        void Update(Game game);
        void Delete(int id);
    }
}
