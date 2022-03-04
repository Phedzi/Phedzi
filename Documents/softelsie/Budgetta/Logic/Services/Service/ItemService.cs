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
    public class ItemService : IItemService
    {
        private readonly IMapper mapper;
        private readonly IItemRepository repository;
        private readonly IItemTypeRepository typeRepository;
        private readonly IResponseService<Item> response;

        public ItemService(
            IMapper mapper,
            IItemRepository repository,
            IResponseService<Item> response,
            IItemTypeRepository typeRepository)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.response = response;
            this.typeRepository = typeRepository;
        }

        public async Task<Response<Item>> Add(Item model)
        {
            
            model.ItemType = mapper.Map<ItemTypeModel,ItemType>(await typeRepository.Get(model.ItemType.Id));

            var _request = await repository.Add(mapper.Map<Item, ItemModel>(model));
            var _response = mapper.Map<ItemModel, Item>(_request);

            return response.Result(_response);
        }

        public async Task<Response<Item>> Delete(int Id)
        {
            var request = await repository.Get(Id);

            if (request != null)
            {
                request.IsDeleted = true;
                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<ItemModel, Item>(request), "Ok");
        }

        public async Task<Response<Item>> Get(int Id)
        {
            var _response = mapper.Map<ItemModel, Item> (await repository.Get(Id));
            return response.Result(_response);
        }

        public async Task<ListResponse<Item>> GetAll(int id)
        {
            var _request = (List<ItemModel>)await repository.GetAll();
            var _response = mapper.Map<List<ItemModel>, List<Item>>(_request);

            return response.Results(_response);
        }

        public async Task<Response<Item>> Update(Item model)
        {
            var request = await repository.Get(model.Id);

            if (request != null)
            {
                request.Name = model.Name;
                request.Description = model.Description;
                request.PaidAmount = model.PaidAmount;
                request.PaymentMaid = model.PaymentMaid;
                request.Amount = request.Amount - model.PaidAmount;

                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<ItemModel, Item>(request), "Ok");
        }
    }
}
