using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Common;
using Models.Budget;

namespace DAL.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly BudgettaDbContext context;
        public ItemTypeRepository(BudgettaDbContext context)
        {
            this.context = context;
        }

        public async Task<ItemTypeModel> Add(ItemTypeModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TItemType.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<ItemTypeModel> Get(int Id)
        {
            return await context
                  .TItemType
                  .Include(u => u.Items)
                  .Where(p => p.Id == Id && p.IsDeleted == false)
                  .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ItemTypeModel>> GetAll()
        {
            return await context
                       .TItemType
                       .Where(p => p.IsDeleted == false)
                       .ToListAsync();
        }

        public async Task<ItemTypeModel> Update(ItemTypeModel model)
        {
            model.UpdatedAt = DateTime.Now;
            var UpdatedSubject = context.TItemType.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
