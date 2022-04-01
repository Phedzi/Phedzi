using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository repository;
        private readonly IMenuRepository menuRepository;
        private readonly IResponseService<UserTypeModel> response;

        public UserTypeService(
             IResponseService<UserTypeModel> response,
            IUserTypeRepository repository,
            IMenuRepository menuRepository)
        {
            this.repository = repository;
            this.response = response;
            this.menuRepository = menuRepository;
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

        public async Task<Response<UserTypeModel>> UpdateMenu(UserTypeModel model)
        {
            foreach (MenuModel menu in model.Menues)
            {
                menu.UserType = model;
                await menuRepository.Update(menu);
            }
            return response.Result(model);
        }
    }
}
