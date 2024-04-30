using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface ISkinRepository
    {
        int Insert(Skin skin);
        Skin GetById(int id);
        void Update(Skin skin);
        void Delete(int id);
    }
}
