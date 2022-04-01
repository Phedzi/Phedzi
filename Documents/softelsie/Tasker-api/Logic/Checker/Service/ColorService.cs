using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository repository;
        private readonly IResponseService<ColorModel> response;

        public ColorService(
             IResponseService<ColorModel> response,
            IColorRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<ColorModel>> AddAsyc(ColorModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<ColorModel>> DeleteAsyc(int Id)
        {
            ColorModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<ColorModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ColorModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<ColorModel>> GetAll()
        {
            return response.Results((List<ColorModel>)(await repository.GetAll()));
        }

        public async Task<Response<ColorModel>> UpdateAsyc(ColorModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
