using Logic.Common;
using Models.Checker;
using Models.Common;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public interface IUserService : IBService<User>
    {
        public Task<Response<User>> getUserByUserName(string userName);
    }
}
