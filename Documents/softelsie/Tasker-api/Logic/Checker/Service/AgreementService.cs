using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using Models.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class AgreementService : IAgreementService
    {
        private readonly IAgreementRepository repository;
        private readonly IResponseService<AgreementModel> response;

        public AgreementService(
             IResponseService<AgreementModel> response,
            IAgreementRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<AgreementModel>> AddAsyc(AgreementModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;

            //var userID = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            //var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<AgreementModel>> DeleteAsyc(int Id)
        {
            AgreementModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public async  Task<PagedList<AgreementModel>> Filter(int page)
        {
            var query = (await repository.Find(c => c.IsDeleted == false)).ToList<AgreementModel>();

            return new PagedList<AgreementModel>(query.AsQueryable(), page, 10);
        }

        public Task<ListResponse<AgreementModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<AgreementModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<AgreementModel>> GetAll()
        {
            return response.Results((List<AgreementModel>)(await repository.GetAll()));
        }

        public async Task<Response<AgreementModel>> UpdateAsyc(AgreementModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
