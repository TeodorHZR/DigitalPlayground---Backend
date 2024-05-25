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

        IEnumerable<Skin> GetAll();

        IEnumerable<Skin> GetAllAvailableForUser(int userId);
        IEnumerable<Skin> GetAllAvailableForGame(int gameId, int excludeUserId);

        void UpdateUser(int skinId, int userId);

        IEnumerable<Skin> GetSkinsOrderedByPrice(bool ascending, int gameId, int excludeUserId);

        IEnumerable<Skin> GetSkinsByMaxPrice(float maxPrice, int gameId, int excludeUserId);
    }
}
