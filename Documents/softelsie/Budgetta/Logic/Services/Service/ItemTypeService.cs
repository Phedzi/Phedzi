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
    public class ItemTypeService : IItemTypeService
    {
        private readonly IMapper mapper;
        private readonly IItemTypeRepository repository;
        private readonly IResponseService<ItemType> response;

        public ItemTypeService(
            IMapper mapper,
            IItemTypeRepository repository,
            IResponseService<ItemType> response)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<ItemType>> Add(ItemType model)
        {

            var _request = await repository.Add(mapper.Map<ItemType,ItemTypeModel>(model));
            var _response = mapper.Map<ItemTypeModel,ItemType>(_request);

            return response.Result(_response);
        }

        public async Task<Response<ItemType>> Delete(int Id)
        {
            var request = await repository.Get(Id);
            
            if (request != null)
            {
                request.IsDeleted = true;
                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<ItemTypeModel, ItemType>(request),"Ok");
        }

        public async Task<Response<ItemType>> Get(int Id)
        {
            var _response = mapper.Map<ItemTypeModel,ItemType>(await repository.Get(Id));
            return response.Result(_response);
        }

        public async Task<ListResponse<ItemType>> GetAll(int id)
        {
            var _request = (List<ItemTypeModel>)await repository.GetAll();
            var _response = mapper.Map<List<ItemTypeModel>, List<ItemType>>(_request);

            return response.Results(_response);
        }

        public async Task<Response<ItemType>> Update(ItemType model)
        {
            var request = await repository.Get(model.Id);

            if (request != null)
            {
                request.Name = model.Name;
                request.Description = model.Description;
                
                await repository.Update(request);
            }
            return response.ResultMessage(mapper.Map<ItemTypeModel, ItemType>(request), "Ok");
        }
    }
}
