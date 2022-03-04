using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Repositories;
using Logic.Common;
using Models.Budget;
using Models.Common;
using Models.Model;

namespace Logic.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository repository;
        private readonly IBudgetRepository budgetRepository;
        private readonly IResponseService<Category> response;

        public CategoryService(
            IMapper mapper,
            ICategoryRepository repository,
            IResponseService<Category> response,
            IBudgetRepository budgetRepository)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.response = response;
            this.budgetRepository = budgetRepository;
        }

        public async Task<Response<Category>> Add(Category model)
        {
            model.Budget = mapper.Map<BudgetModel, Budget>(await budgetRepository.Get(model.Budget.Id));

            var _request = await repository.Add(mapper.Map<Category, CategoryModel>(model));
            var _response = mapper.Map<CategoryModel, Category>(_request);

            return response.Result(_response);
        }

        public async Task<Response<Category>> Delete(int Id)
        {
            var request = await repository.Get(Id);

            if (request != null)
            {
                request.IsDeleted = true;
                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<CategoryModel, Category>(request), "Ok");
        }

        public async Task<Response<Category>> Get(int Id)
        {
            var _response = mapper.Map<CategoryModel, Category>(await repository.Get(Id));
            return response.Result(_response);
        }

        public async Task<ListResponse<Category>> GetAll(int id)
        {
            var _request = (List<CategoryModel>)await repository.GetAll();
            var _response = mapper.Map<List<CategoryModel>, List<Category>>(_request);

            return response.Results(_response);
        }

        public async Task<Response<Category>> Update(Category model)
        {
            var request = await repository.Get(model.Id);

            if (request != null)
            {
                request.Name = model.Name;
                request.Description = model.Description;
                request.HasTarget = model.HasTarget;
                request.TotalAmount = model.TotalAmount;
                request.TotalTarget = model.TotalTarget;
                request.TotalLoss = model.TotalLoss;

                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<CategoryModel, Category>(request), "Ok");
        }
    }
}
