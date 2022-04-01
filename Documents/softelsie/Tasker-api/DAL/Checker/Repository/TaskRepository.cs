using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class TaskRepository : ITaskRepository
    {
        private readonly CompanyDbContext context;

        public TaskRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskModel> Add(TaskModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            ColorModel _color = context.TColor.Find(model._Color.Id);
            model._Color = _color;
            context.TTask.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<TaskModel> Get(int Id)
        {
            return await context
                    .TTask
                    .Include(c => c._Color)
                    .Where(p => p.Id == Id && p.IsDeleted == false)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            return await context
                   .TTask
                   .Include(c => c._Color)
                   .Where(p => p.IsDeleted == false)
                   .OrderBy(p => p.DueDate)
                   .ToListAsync();
        }

        public async Task<TaskModel> Update(TaskModel Item)
        {
            var UpdatedSubject = context.TTask.Attach(Item);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
