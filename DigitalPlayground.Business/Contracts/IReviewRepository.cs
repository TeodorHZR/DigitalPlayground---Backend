﻿using DigitalPlayground.Business.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DigitalPlayground.Business.Contracts
{
    public interface IReviewRepository
    {
        int Insert(Review review);
        Review GetById(int id);
        void Update(Review review);
        void Delete(int id);

        int InsertOrUpdate(Review review);

        List<string> GetReviewMessages(string gameName, int offset, int limit);
        List<string> GetReviewMessagesByGame(string gameName);
    }
}
