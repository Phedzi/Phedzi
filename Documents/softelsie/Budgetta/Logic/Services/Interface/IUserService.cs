using Logic.Common;
using Models.Model;
using Models.Common;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IUserService : IBService<User>
    {
        public Task<Response<User>> getUserByUserName(string userName);
    }
}
