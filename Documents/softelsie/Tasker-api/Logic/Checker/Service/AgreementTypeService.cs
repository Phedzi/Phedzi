using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class AgreementTypeService : IAgreementTypeService
    {
        private readonly IAgreementTypeRepository repository;
        private readonly IResponseService<AgreementTypeModel> response;

        public AgreementTypeService(
             IResponseService<AgreementTypeModel> response,
            IAgreementTypeRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<AgreementTypeModel>> AddAsyc(AgreementTypeModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<AgreementTypeModel>> DeleteAsyc(int Id)
        {
            AgreementTypeModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<AgreementTypeModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<AgreementTypeModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<AgreementTypeModel>> GetAll()
        {
            return response.Results((List<AgreementTypeModel>)(await repository.GetAll()));
        }

        public async Task<Response<AgreementTypeModel>> UpdateAsyc(AgreementTypeModel model)
        {

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }
    }
}
