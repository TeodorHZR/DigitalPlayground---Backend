using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface IGameTransactionRepository
    {
        int Insert(GameTransaction gameTransaction);
        GameTransaction GetById(int id);
        void Update(GameTransaction gameTransaction);
        void Delete(int id);
    }
}
