using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Common;
using Models.Budget;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BudgettaDbContext context;
        public CategoryRepository(BudgettaDbContext context)
        {
            this.context = context;
        }

        public async Task<CategoryModel> Add(CategoryModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TCategory.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<CategoryModel> Get(int Id)
        {
            return await context
                     .TCategory
                     .Include(u => u.Budget)
                     .Where(p => p.Id == Id && p.IsDeleted == false)
                     .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetAll()
        {
            return await context
                       .TCategory
                       .Where(p => p.IsDeleted == false)
                       .ToListAsync();
        }

        public async Task<CategoryModel> Update(CategoryModel model)
        {
            model.UpdatedAt = DateTime.Now;
            var UpdatedSubject = context.TCategory.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
