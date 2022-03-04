using DAL.Common;
using Models.Budget;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly BudgettaDbContext context;
        public UserTypeRepository(BudgettaDbContext context)
        {
            this.context = context;
        }
        public async Task<UserTypeModel> Add(UserTypeModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TUserType.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<UserTypeModel> Get(int Id)
        {
            return await context
                 .TUserType
                 .Include(u => u.Users)
                 .Where(p => p.Id == Id && p.IsDeleted == false)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserTypeModel>> GetAll()
        {
            return await context
                   .TUserType
                   .Include(u => u.Users)
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();
        }

        public async Task<UserTypeModel> Update(UserTypeModel model)
        {
            model.UpdatedAt = DateTime.Now;
            var UpdatedSubject = context.TUserType.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
