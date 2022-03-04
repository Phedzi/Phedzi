using DAL.Repositories;
using Logic.Common;
using Models.Budget;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository repository;
        private readonly IResponseService<UserTypeModel> response;

        public UserTypeService(
             IResponseService<UserTypeModel> response,
            IUserTypeRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<UserTypeModel>> AddAsyc(UserTypeModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<UserTypeModel>> DeleteAsyc(int Id)
        {
            UserTypeModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<UserTypeModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UserTypeModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<UserTypeModel>> GetAll()
        {
            return response.Results((List<UserTypeModel>)(await repository.GetAll()));
        }

        public async Task<Response<UserTypeModel>> UpdateAsyc(UserTypeModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
