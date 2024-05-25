using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByUsername(string username);
        int Insert(User user);
        void Delete(int id);
        void UpdateAdminStatus(int userId, bool isAdmin);
        public (int Id, float Money) GetIdAndMoneyByUsername(string username);
        public void UpdateMoney(int userId, float money);

    }
}
