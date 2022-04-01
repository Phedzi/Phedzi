using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class AgreementTypeRepository : IAgreementTypeRepository
    {
        private readonly CompanyDbContext context;

        public AgreementTypeRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public async Task<AgreementTypeModel> Add(AgreementTypeModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TAgreementType.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<AgreementTypeModel> Get(int Id)
        {
            return await context
                .TAgreementType
                .Where(p => p.Id == Id && p.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AgreementTypeModel>> GetAll()
        {
            var list = await context
                   .TAgreementType
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();

            return list;
        }

        public async Task<AgreementTypeModel> Update(AgreementTypeModel model)
        {
            var UpdatedSubject = context.TAgreementType.Attach(model);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
