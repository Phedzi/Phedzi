using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class MenuRepository : IMenuRepository
    {
        private readonly CompanyDbContext context;

        public MenuRepository(CompanyDbContext context)
        {
            this.context = context;
        }
        public async Task<MenuModel> Add(MenuModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TMenu.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<MenuModel> Get(int Id)
        {
            return await context
                    .TMenu
                    .Include(c => c.Subs)
                    .Include(c => c.UserType)
                    .Where(p => p.Id == Id && p.IsDeleted == false)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MenuModel>> GetAll()
        {
            return await context
                   .TMenu
                   .Include(c => c.Subs)
                   .Include(c => c.UserType)
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();
            
        }

        public async Task<MenuModel> Update(MenuModel Item)
        {
            var UpdatedSubject = context.TMenu.Attach(Item);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
