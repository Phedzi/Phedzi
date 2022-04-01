using DAL.Checker;
using Logic.Common;
using Microsoft.AspNetCore.Identity;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IAgreementRepository repository;
        private readonly IResponseService<User> response;

        public UserService(
             IResponseService<User> response,
            UserManager<UserModel> userManager,
            IAgreementRepository repository)
        {
            this.userManager = userManager;
            this.response = response;
            this.repository = repository;
        }

        public Task<Response<User>> AddAsyc(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> DeleteAsyc(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<User>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListResponse<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<User>> getUserByUserName(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
                return response.ResultMessage(null, "User could not be found");

            var _user = new User();

            _user.Email = user.Email;
            _user.UserName = user.UserName;
            _user.PhoneNumber = user.PhoneNumber;
            _user.Agreements = (List<AgreementModel>)await repository.GetByAssignee(user);

            return response.Result(_user);
        }

        public Task<Response<User>> UpdateAsyc(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
