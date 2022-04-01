using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class StatusRepository : IStatusRepository
    {
        private readonly CompanyDbContext context;

        public StatusRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public async Task<StatusModel> Add(StatusModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TStatus.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<StatusModel> Get(int Id)
        {
            return await context
                        .TStatus
                        .Where(p => p.Id == Id && p.IsDeleted == false)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StatusModel>> GetAll()
        {
            var list = await context
                    .TStatus
                    .Where(p => p.IsDeleted == false)
                    .ToListAsync();

            return list;
        }

        public async Task<StatusModel> Update(StatusModel model)
        {
            var UpdatedSubject = context.TStatus.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
