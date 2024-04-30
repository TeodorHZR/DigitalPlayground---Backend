using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface ICategoryRepository
    {
        List<Category> GetAll(int offset, int limit, string sortOrder);

        Category GetById(int id);

        int Insert(Category category);

        void Update(Category category);
        void Delete(int id);
        int GetId(string name);
        List<Category> GetCategoriesPaginated(int page, int itemsPerPage, string sortOrder);
    }
}
