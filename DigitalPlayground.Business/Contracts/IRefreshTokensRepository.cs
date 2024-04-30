using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlayground.Business.Contracts
{
    public interface IRefreshTokensRepository
    {
        RefreshTokens GetRefreshToken(string refreshToken);
        void SaveRefreshToken(int id, string refreshToken);
    }
}
