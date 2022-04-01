using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository repository;
        private readonly IResponseService<StatusModel> response;

        public StatusService(
             IResponseService<StatusModel> response,
            IStatusRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<StatusModel>> AddAsyc(StatusModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<StatusModel>> DeleteAsyc(int Id)
        {
            StatusModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<StatusModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<StatusModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<StatusModel>> GetAll()
        {
            return response.Results((List<StatusModel>)(await repository.GetAll()));
        }

        public async Task<Response<StatusModel>> UpdateAsyc(StatusModel model)
        {
            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
