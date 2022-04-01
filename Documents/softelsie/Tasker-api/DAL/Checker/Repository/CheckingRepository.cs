using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace DAL.Checker.Repository
{
    public class CheckingRepository : ICheckingRepository
    {
        private readonly CompanyDbContext context;
        public CheckingRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public async Task<CheckingModel> Add(CheckingModel model)
        {
                model.IsDeleted = false;
                model.CreatedAt = DateTime.Now;
                ColorModel _color = context.TColor.Find(4); // 4 is currentley the default not active status
                model._Color = _color;
                StatusModel status = context.TStatus.Find(4); // 4 is currentley the default not active status
                model.Status = status;
                context.TChecking.Add(model);
                await context.SaveChangesAsync();

            return model;
        }


        public async Task<CheckingModel> Get(int Id)
        {
            return await context
                    .TChecking
                    .Include(c => c._Color)
                    .Include(c => c.Status)
                    .Where(p => p.Id == Id && p.IsDeleted == false)
                    .FirstOrDefaultAsync();
        }
        public async Task<CheckingModel> CurrentChecking(string ownerId=null)
        {

            return string.IsNullOrEmpty(ownerId)? await context
                    .TChecking
                    .Include(c => c._Color)
                    .Include(c => c.Status)
                    .Where(p => p.IsDeleted == false && p.DueDate >= DateTime.Now) 
                    .FirstOrDefaultAsync() : 
                    await context
                    .TChecking
                    .Include(c => c._Color)
                    .Include(c => c.Status)
                    .Where(p => p.IsDeleted == false && p.DueDate >= DateTime.Now)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CheckingModel>> GetAll()
        {
            var currentChecking = await this.CurrentChecking();
            return await context
                    .TChecking
                    .Include(c => c._Color)
                    .Include(c => c.Status)
                    .Where(p => p.IsDeleted == false && p.Id != currentChecking.Id)
                    .OrderBy(p => p.DueDate)
                    .ToListAsync();
        }

            public async Task<CheckingModel> PayChecking(CheckingModel model)
            {
                return await this.Update(model);
            }

            public async Task<CheckingModel> Update(CheckingModel Item)
        {
            var UpdatedSubject = context.TChecking.Attach(Item);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }

        public async Task<CheckingModel> CheckingByWeight(string userId,int weight)
        {
            return await context
                 .TChecking
                 .Include(c => c._Color)
                 .Include(c => c.Status)
                 .Where(p => 
                 p.Weight == weight && 
                 p.IsDeleted == false)
                 .FirstOrDefaultAsync();
        }

        public async Task<List<CheckingModel>> GetByUser(string userId)
        {
            var currentChecking = await this.CurrentChecking(userId);
            return await context
                    .TChecking
                    .Include(c => c._Color)
                    .Include(c => c.Status)
                    .Where(p =>p.IsDeleted == false && p.Id != currentChecking.Id)
                    .OrderBy(p => p.DueDate)
                    .ToListAsync();
        }

        public async Task<IEnumerable<CheckingModel>> Find(Expression<Func<CheckingModel, bool>> predicte)
        {
           return await context.TChecking
                        .Include(c => c._Color)
                        .Include(c => c.Status)
                        .Where(predicte)
                        .ToListAsync();
        }

        public void AddList(List<CheckingModel> Items)
        {

            context.TChecking.AddRange(Items);
            context.SaveChanges();
        }
    }
}
