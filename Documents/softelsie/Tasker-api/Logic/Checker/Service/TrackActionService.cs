using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class TrackActionService : ITrackActionService
    {
        private readonly ITrackActionRepository repository;
        private readonly IResponseService<TrackActionModel> response;

        public TrackActionService(
             IResponseService<TrackActionModel> response,
            ITrackActionRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<TrackActionModel>> AddAsyc(TrackActionModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<TrackActionModel>> DeleteAsyc(int Id)
        {
            TrackActionModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<TrackActionModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TrackActionModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<TrackActionModel>> GetAll()
        {
            return response.Results((List<TrackActionModel>)(await repository.GetAll()));
        }

        public async Task<Response<TrackActionModel>> UpdateAsyc(TrackActionModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
