using Models.Common;
using System.Threading.Tasks;

namespace Logic.Common
{
    public interface IBaseService<T> 
    {
        Task<Response<T>> Get(int Id);
        Task<ListResponse<T>> GetAll(int id);
        Task<Response<T>> Add(T Item);
        Task<Response<T>> Update(T Item);
        Task<Response<T>> Delete(int Id);
    }
}
