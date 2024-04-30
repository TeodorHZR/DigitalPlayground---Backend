using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface IGameCategoryRepository
    {
        void AddGameToCategory(int gameId, int categoryId);
        void RemoveGameFromCategory(int gameId, int categoryId);
        int[] GetCategoriesForGame(int gameId);

        int[] GetGamesForCategory(int categoryId);
    }
}
