using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class SubMenuRepository : ISubMenuRepository
    {
        private readonly CompanyDbContext context;

        public SubMenuRepository(CompanyDbContext context)
        {
            this.context = context;
        }
        public async Task<SubMenuModel> Add(SubMenuModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
           // MenuModel menu = context.TMenu.Find(model.Menu.Id);
            //model.Menu = menu;
            context.TSubMenu.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<SubMenuModel> Get(int Id)
        {
            return await context
                    .TSubMenu
                    .Include(c => c.Menu)
                    .Where(p => p.Id == Id && p.IsDeleted == false)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SubMenuModel>> GetAll()
        {
            return await context
                   .TSubMenu
                   .Include(c => c.Menu)
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();
        }

        public async Task<SubMenuModel> Update(SubMenuModel Item)
        {
            Item.UpdatedAt = DateTime.Now;
            var UpdatedSubject = context.TSubMenu.Attach(Item);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
