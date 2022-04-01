using Models.Checker;
using Models.Common;
using Models.Extra;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public interface IAdministration
    {
        Task<Response<string>> CreateAsync(User user);
        Task<AuthResponse> SingInAsync(User user);
        Task<Response<string>> ChangePassword(ChangePasswordUser user);
        Task<Response<string>> ResetPassword(UserModel user);
        Task<PagedList<UserModel>> GetByFilter(string Id, string search, int pageNumber);
    }
}
