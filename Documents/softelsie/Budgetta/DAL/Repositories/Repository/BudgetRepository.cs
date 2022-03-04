using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Common;
using Models.Budget;

namespace DAL.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly BudgettaDbContext context;
        public BudgetRepository(BudgettaDbContext context)
        {
            this.context = context;
        }

        public async Task<BudgetModel> Add(BudgetModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TBudget.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<BudgetModel> Get(int Id)
        {
            return await context
                     .TBudget
                     .Include(c => c.Categories)
                     .Where(p => p.Id == Id && p.IsDeleted == false)
                     .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BudgetModel>> GetAll()
        {
            return await context
                       .TBudget
                       .Where(p => p.IsDeleted == false)
                       .ToListAsync();
        }

        public async Task<BudgetModel> Update(BudgetModel model)
        {
            model.UpdatedAt = DateTime.Now;
            var UpdatedSubject = context.TBudget.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
