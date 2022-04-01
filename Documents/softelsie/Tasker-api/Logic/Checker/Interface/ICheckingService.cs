
using Logic.Common;
using Models.Checker;
using Models.Common;
using Models.Extra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public interface ICheckingService : IBService<CheckingModel> 
    {
        Task<Response<CheckingModel>> PayChecking(CheckingModel entity);
        Task<ListResponse<CheckingModel>> AddAsyc(CheckingModel cheking, string name);
        Task<Response<CheckingModel>> CurrentChecking();
        Task<Response<CheckingModel>> ApproveChecking(int taskId);
        Task<PagedList<CheckingTable>> GetByCheckingTable(string search, string ownerId, int pageNumber);
        Task<ListResponse<CheckingModel>> GetByUser(string userId);

        Task<List<CheckingTable>> GetAlll();
        Task<Response<CheckingModel>> OpenChecking(int id);
    }
}
