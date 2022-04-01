using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class SubMenuService : ISubMenuService
    {
        private readonly ISubMenuRepository repository;
        private readonly IResponseService<SubMenuModel> response;

        public SubMenuService(
             IResponseService<SubMenuModel> response,
            ISubMenuRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<SubMenuModel>> AddAsyc(SubMenuModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<SubMenuModel>> DeleteAsyc(int Id)
        {
            SubMenuModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<SubMenuModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<SubMenuModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<SubMenuModel>> GetAll()
        {
            return response.Results((List<SubMenuModel>)(await repository.GetAll()));
        }

        public async Task<Response<SubMenuModel>> UpdateAsyc(SubMenuModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
