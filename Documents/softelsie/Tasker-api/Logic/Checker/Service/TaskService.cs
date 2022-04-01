using DAL.Checker;
using Logic.Common;
using Models.Checker;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository repository;
        private readonly IResponseService<TaskModel> response;

        public TaskService(
             IResponseService<TaskModel> response,
            ITaskRepository repository)
        {
            this.repository = repository;
            this.response = response;
        }

        public async Task<Response<TaskModel>> AddAsyc(TaskModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }

        public async Task<Response<TaskModel>> DeleteAsyc(int Id)
        {
            TaskModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public Task<ListResponse<TaskModel>> Find(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TaskModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<TaskModel>> GetAll()
        {
            return response.Results((List<TaskModel>)(await repository.GetAll()));
        }

        public async Task<Response<TaskModel>> UpdateAsyc(TaskModel model)
        {
            if (model._Color != null)
                return response.Result(null);

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }

        public void Viewed(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
