
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Common
{
    public interface IBaseRepository<T>
    {
        Task<T> Get(int Id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T model);
        Task<T> Update(T model);
    }
}
