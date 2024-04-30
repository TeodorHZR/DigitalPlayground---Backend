using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface ISkinTransactionRepository
    {
        int Insert(SkinTransaction skinTransaction);
        SkinTransaction GetById(int id);
        void Update(SkinTransaction skinTransaction);
        void Delete(int id);

    }
}
