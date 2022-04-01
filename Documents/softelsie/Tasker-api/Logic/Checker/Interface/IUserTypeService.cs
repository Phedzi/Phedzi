using Logic.Common;
using Models.Checker;
using Models.Common;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public interface IUserTypeService : IBService<UserTypeModel>
    {
        Task<Response<UserTypeModel>> UpdateMenu(UserTypeModel model);
    }
}
