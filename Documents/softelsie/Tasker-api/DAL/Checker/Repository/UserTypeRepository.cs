using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly CompanyDbContext context;
        public UserTypeRepository(CompanyDbContext context)
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
                 .Include(c => c.Menues)
                 .Include(u => u.Users)
                 .Where(p => p.Id == Id && p.IsDeleted == false)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserTypeModel>> GetAll()
        {
            return await context
                   .TUserType
                   .Include(c => c.Menues)
                   .Include(u => u.Users)
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();
        }

        public async Task<UserTypeModel> Update(UserTypeModel Item)
        {
            var UpdatedSubject = context.TUserType.Attach(Item);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
