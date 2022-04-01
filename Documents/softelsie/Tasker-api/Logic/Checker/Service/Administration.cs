using AutoMapper;
using Logic.Common;
using Microsoft.AspNetCore.Identity;
using Models.Checker;
using Models.Common;
using Models.Extra;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class Administration : IAdministration
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IResponseService<string> response;
        private readonly IJwtFactory jwtFactory;
        private readonly IMapper mapper;

        private int PageSize { get; } = 5;

        public Administration(
                UserManager<UserModel> userManager,
                IResponseService<string> response,
                 IMapper mapper,
                IJwtFactory jwtFactory)
        {
            this.userManager = userManager;
            this.response = response;
            this.jwtFactory = jwtFactory;
        }

        public async Task<Response<string>> ChangePassword(ChangePasswordUser user)
        {
            var _user = await userManager.FindByIdAsync(user.Id);
           
            if (_user == null)
            {
                return response.ResultMessage("User Not Found", "Fail");
            }
            var result = await userManager.ChangePasswordAsync(_user, user.OldPassword, user.NewPassword);

            if(result.Succeeded)
                return response.ResultMessage("User Password Changed", "OK");

           return response.ResultMessage("User Password too week Hint: include numbers", "Fail");
        }

        public async Task<Response<string>> ResetPassword(UserModel user)
        {
            var _user = await userManager.FindByIdAsync(user.Id);
            if (_user == null)
            {
                return response.ResultMessage("User Not Found", "Fail");
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(_user);

            await userManager.ResetPasswordAsync(_user, token, "123user");
            return response.ResultMessage("User Password reserted with password 123user", "OK");
        }

        public async Task<Response<string>> CreateAsync(User _user)
        {
            UserModel user = new UserModel();
            user.Email = _user.Email;
            user.Name = _user.Name;
            user.UserName = _user.UserName;
            user.PhoneNumber = _user.PhoneNumber;
            user.Type.Id = _user.Type.Id;
            List<Claim> claims = new List<Claim>() { new Claim("User", "taxi Owner") };

            if (_user.Type.Id == 2)
            {
                claims.Add(new Claim("Admin", "taxi Admin"));
            }

            var result = await userManager.CreateAsync(user, _user.Password);


            if (!result.Succeeded)
                return response.ResultMessage(result.Errors.ToString() + " Something went wrong User note Created", "Failed");
            await userManager.AddClaimsAsync(user, claims);

            return response.ResultMessage("User Successfully created", "Ok");
        }

        public async Task<PagedList<UserModel>> GetByFilter(string Id,string search, int pageNumber)
        {
            var query = userManager.Users.Where(u => u.Id != Id).AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()) ||
                        p.UserName.ToLower().Contains(search.ToLower()) ||
                        p.Email.ToLower().Contains(search.ToLower()));

            return new PagedList<UserModel>(query, pageNumber, PageSize);
        }

        public async Task<AuthResponse> SingInAsync(User user)
        {
            var identity = await GetClaimsIdentity(user.UserName, user.Password);
            if (identity == null)
            {
                return new AuthResponse() { Message = "Failed" };
            }

            //useer already found and authenticated
            var _user = await userManager.FindByNameAsync(user.UserName);
            return await Tokens.GenerateJwt(identity, jwtFactory, user.UserName,"Admin");
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);


            var userToVerify = await userManager.FindByNameAsync(userName);
            if (userToVerify == null)
                return await Task.FromResult<ClaimsIdentity>(null);

            if (await userManager.CheckPasswordAsync(userToVerify, password))
            {
                var claims = await userManager.GetClaimsAsync(userToVerify);
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, claims.ToList()));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
