using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository repository;
        private readonly ISubMenuRepository subRepository;
        private readonly IResponseService<MenuModel> response;

        public MenuService(
             IResponseService<MenuModel> response,
            IMenuRepository repository,
            ISubMenuRepository subRepository)
        {
            this.repository = repository;
            this.response = response;
            this.subRepository = subRepository;
        }

        public async Task<Response<MenuModel>> AddAsyc(MenuModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model.Url = "/" + model.Caption.ToLower();
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<MenuModel>> DeleteAsyc(int Id)
        {
            MenuModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<MenuModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<MenuModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<MenuModel>> GetAll()
        {
            return response.Results((List<MenuModel>)(await repository.GetAll()));
        }

        public async Task<Response<MenuModel>> UpdateAsyc(MenuModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }

        public async Task<Response<MenuModel>> UpdateSub(MenuModel model)
        {
            foreach(SubMenuModel subMenu in model.Subs)
            {
                subMenu.Menu = model;
                subMenu.Url = model.Url +'/'+ subMenu.Caption.Trim().ToLower();
                await subRepository.Update(subMenu);
            }
            return response.Result(model);
        }
    }
}
