using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public interface IAgreementRepository : IBaseRepository<AgreementModel>
    {
        Task<IEnumerable<AgreementModel>> GetByAssignee(UserModel assignee);
        Task<IEnumerable<AgreementModel>> Find(Expression<Func<AgreementModel, bool>> predicte);
    }
}
