using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Repositories;
using Logic.Common;
using Models.Budget;
using Models.Common;
using Models.Model;

namespace Logic.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IMapper mapper;
        private readonly IBudgetRepository repository;
        private readonly IResponseService<Budget> response;

        public BudgetService(
            IMapper mapper,
            IBudgetRepository repository,
            IResponseService<Budget> response)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<Budget>> Add(Budget model)
        {
            var _request = await repository.Add(mapper.Map<Budget, BudgetModel>(model));
            var _response = mapper.Map<BudgetModel, Budget>(_request);

            return response.Result(_response);
        }

        public async Task<Response<Budget>> Delete(int Id)
        {
            var request = await repository.Get(Id);

            if (request != null)
            {
                request.IsDeleted = true;
                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<BudgetModel, Budget>(request), "Ok");
        }

        public async Task<Response<Budget>> Get(int Id)
        {
            var _response = mapper.Map<BudgetModel, Budget>(await repository.Get(Id));
            return response.Result(_response);
        }

        public async Task<ListResponse<Budget>> GetAll(int id)
        {
            var _request = (List<BudgetModel>)await repository.GetAll();
            var _response = mapper.Map<List<BudgetModel>, List<Budget>>(_request);

            return response.Results(_response);
        }

        public async Task<Response<Budget>> Update(Budget model)
        {
            var request = await repository.Get(model.Id);

            if (request != null)
            {
                request.Name = model.Name;
                request.Description = model.Description;
                request.IsMonthly = model.IsMonthly;
                request.Loss = model.Loss;
                request.SaveCurrent = request.SaveCurrent;
                request.SaveTarget = request.SaveTarget;

                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<BudgetModel, Budget>(request), "Ok");
        }
    }
}
