using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public interface ICheckingRepository : IBaseRepository<CheckingModel>
    {
        void AddList(List<CheckingModel> Items);
        Task<CheckingModel> PayChecking(CheckingModel Item);
        Task<IEnumerable<CheckingModel>> Find(Expression<Func<CheckingModel, bool>> predicte);
        Task<List<CheckingModel>> GetByUser(string userId);
        Task<CheckingModel> CheckingByWeight(string userId, int weight);
        Task<CheckingModel> CurrentChecking(string ownerId=null);
    }
}
