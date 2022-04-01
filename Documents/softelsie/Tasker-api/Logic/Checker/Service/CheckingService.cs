

using DAL.Checker;
using Logic.Common;
using Microsoft.AspNetCore.Identity;
using Models.Checker;
using Models.Common;
using Models.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Checker
{
    public class CheckingService : ICheckingService
    {
        private readonly ICheckingRepository repository;
        private readonly IStatusRepository statusRepository;
        private readonly IColorRepository colorRepository;
        private readonly IAgreementRepository agreementRepository;
        private readonly UserManager<UserModel> userManager;
        private readonly IResponseService<CheckingModel> response;
        private int PageSize { get; } = 5;
        public CheckingService(
             IResponseService<CheckingModel> response,
            ICheckingRepository repository,
            IStatusRepository statusRepository,
            IColorRepository colorRepository,
            UserManager<UserModel> userManager,
            IAgreementRepository agreementRepository)
        {
            this.repository = repository;
            this.response = response;
            this.statusRepository = statusRepository;
            this.colorRepository = colorRepository;
            this.userManager = userManager;
            this.agreementRepository = agreementRepository;
        }

        public async Task<Response<CheckingModel>> AddAsyc(CheckingModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model = await repository.Add(model);

            return response.Result(model);
        }
        public async Task<ListResponse<CheckingModel>> AddAsyc(CheckingModel model,string userName)
        {

            //create agreement first
            UserModel owner = await userManager.FindByNameAsync(userName);
            if (owner == null)
                return response.Results(null);
            model.Agreement.Owner = owner;

            AgreementModel agreement = await agreementRepository.Add(model.Agreement);

            if(agreement == null)
                return response.Results(null);

            model.Agreement = agreement;
            var list = new List<CheckingModel>();

            if (agreement.AgreementType.Id == 1)
                list = await DailyTask(model);
            else if (agreement.AgreementType.Id == 2)
                list = await WeeklyTask(model);
            else if (agreement.AgreementType.Id == 3)
                list = await Monthly(model);
            else
            {
                list.Add(await OnceOffTask(model));
            }

            return response.Results(list);
        }
        public async Task<Response<CheckingModel>> DeleteAsyc(int Id)
        {
            CheckingModel model = await repository.Get(Id);
            if (model != null)
            {
                model.IsDeleted = true;
                await repository.Update(model);
            }
            return response.Result(model);
        }

        public async Task<Response<CheckingModel>> CurrentChecking()
        {
            return response.Result(await repository.CurrentChecking());
        }
        public Task<ListResponse<CheckingModel>> Find(string search)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<CheckingModel>> Get(int id)
        {
            return response.Result(await repository.Get(id));
        }

        public async Task<ListResponse<CheckingModel>> GetAll()
        {
            var list = (await repository.GetAll()).GroupBy(c => c.DueDate.Month);
            return response.Results((List<CheckingModel>)list);
        }

        public async Task<List<CheckingTable>> GetAlll()
        {
            var list =(await repository.GetAll())
                .GroupBy(c => new { c.DueDate.Year, c.DueDate.Month })
                .Select(g =>
                new CheckingTable { Month = new CheckingMonth { Month = g.Key.Month, Year = g.Key.Year} , Checkings = g.Select(t => t).ToList()  }
                ).ToList<CheckingTable>();
            return list;
        }

        public async Task<Response<CheckingModel>> PayChecking(CheckingModel entity)
        {
            CheckingModel model = await repository.Get(entity.Id);
            model.AmountDeposited = entity.AmountPaid;
            model.Weight = 99;
           
            return response.Result(await repository.PayChecking(model));
        }

        public async Task<Response<CheckingModel>> UpdateAsyc(CheckingModel model)
        {
            if (model._Color == null)
                return response.Result(null);

            var _current = await repository.Get(model.Id);
            model.UpdatedAt = DateTime.Now;
            model.CreatedAt = _current.CreatedAt;
            model = await repository.Update(model);

            return response.Result(model);
        }

        public async Task<Response<CheckingModel>> ApproveChecking(int taskId)
        {
            CheckingModel model = await repository.Get(taskId);
            model.AmountPaid = model.AmountPaid + model.AmountDeposited;
            model.Weight = 90;

            if ((model.AmountPaid - model.AmountDue) >= 0)
            {
                model.Status = await statusRepository.Get(1); //paid
                model._Color = await colorRepository.Get(2); // css success
            }
            else
            {
                model.Status = await statusRepository.Get(2); //parcialy paid
                model._Color = await colorRepository.Get(3); // css warning
            }
            if(model.AmountDeposited == 0)
            {
                model.Status = await statusRepository.Get(3); //Not paid
                model._Color = await colorRepository.Get(4); // css danger
            }

            return response.Result(await repository.Update(model));
        }

        public async Task<PagedList<CheckingTable>> GetByCheckingTable(string search, string ownerId, int pageNumber)
        {
            var query = (await repository.Find(c => c.IsDeleted == false))
               .GroupBy(c => new { c.DueDate.Year, c.DueDate.Month })
               .Select(g =>
               new CheckingTable 
               { 
                        Month = new CheckingMonth 
                        { 
                            Month = g.Key.Month, 
                            Year = g.Key.Year,
                            MonthTarget = g.Sum(c => c.AmountDue),
                            MonthChecking = g.Sum(c => c.AmountPaid)
                        },  
                        Checkings = g.Select(t => t).ToList() 
               }).ToList<CheckingTable>();

            return new PagedList<CheckingTable>(query.AsQueryable(), pageNumber, PageSize);
        }

        public async Task<ListResponse<CheckingModel>> GetByUser(string userId)
        {
            return response.Results(await repository.GetByUser(userId));
        }

        public async Task<Response<CheckingModel>> OpenChecking(int id)
        {
            CheckingModel model = await repository.Get(id);
            model.Weight = 98;

            return response.Result(await repository.Update(model));
        }


        /***
         *  Private Methods
         * **/

        private async Task<CheckingModel> OnceOffTask(CheckingModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            return await repository.Add(model);
        }
        private async Task<List<CheckingModel>> DailyTask(CheckingModel model)
        {
            List<CheckingModel> tasks = new List<CheckingModel>();

            for (DateTime start = model.Agreement.DateStart; start.Day <= model.Agreement.DateEnd.Day; start = start.AddDays(1))
            {
                var task = model.Clone();

                task.IsDeleted = false;
                task.CreatedAt = DateTime.Now;
                ColorModel _color = await colorRepository.Get(4); // 4 is currentley the default not active status
                task._Color = _color;
                StatusModel status = await statusRepository.Get(4); // 4 is currentley the default not active status
                task.Status = status;
                task.DueDate =start;
                tasks.Add(task);
            }

            repository.AddList(tasks);
            return tasks;
        }
        private async Task<List<CheckingModel>> WeeklyTask(CheckingModel model)
        {
            List<CheckingModel> tasks = new List<CheckingModel>();

            for (DateTime start = model.Agreement.DateStart; start.Day <= model.Agreement.DateEnd.Day; start = start.AddDays(7))
            {
                var task = model.Clone();

                task.IsDeleted = false;
                task.CreatedAt = DateTime.Now;
                ColorModel _color = await colorRepository.Get(4); // 4 is currentley the default not active status
                task._Color = _color;
                StatusModel status = await statusRepository.Get(4); // 4 is currentley the default not active status
                task.Status = status;
                task.DueDate = start;
                tasks.Add(task);
            }
            repository.AddList(tasks);
            return tasks;
        }
        private async Task<List<CheckingModel>> Monthly(CheckingModel model)
        {
            List<CheckingModel> tasks = new List<CheckingModel>();

            for (DateTime start = model.Agreement.DateStart; start <= model.Agreement.DateEnd; start = start.AddMonths(1))
            {
                var task = model.Clone();

                task.IsDeleted = false;
                task.CreatedAt = DateTime.Now;
                ColorModel _color = await colorRepository.Get(4); // 4 is currentley the default not active status
                task._Color = _color;
                StatusModel status = await statusRepository.Get(4); // 4 is currentley the default not active status
                task.Status = status;
                task.DueDate = start;
                tasks.Add(task);
            }
            repository.AddList(tasks);
            return tasks;
        }
    }
}
