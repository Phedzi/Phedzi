using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Common;
using Models.Budget;

namespace DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly BudgettaDbContext context;
        public ItemRepository(BudgettaDbContext context)
        {
            this.context = context;
        }

        public async Task<ItemModel> Add(ItemModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TItem.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<ItemModel> Get(int Id)
        {
            return await context
                      .TItem
                      .Include(u => u.ItemType)
                      .Where(p => p.Id == Id && p.IsDeleted == false)
                      .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ItemModel>> GetAll()
        {
            return await context
                       .TItem
                       .Where(p => p.IsDeleted == false)
                       .ToListAsync();
        }

        public async Task<ItemModel> Update(ItemModel model)
        {
            model.UpdatedAt = DateTime.Now;
            var UpdatedSubject = context.TItem.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
