using Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Common
{
    public interface IBService<T> 
    {
        Task<Response<T>> Get(int id);
        Task<ListResponse<T>> GetAll();
        Task<ListResponse<T>> Find(string search);

        Task<Response<T>> AddAsyc(T entity);
        Task<Response<T>> UpdateAsyc(T entity);
        Task<Response<T>> DeleteAsyc(int Id);
    }
}
