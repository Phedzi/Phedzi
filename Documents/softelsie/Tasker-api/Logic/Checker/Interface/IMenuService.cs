using Logic.Common;
using Models.Checker;
using Models.Common;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public interface IMenuService : IBService<MenuModel>
    {
        Task<Response<MenuModel>> UpdateSub(MenuModel model);
    }
}
