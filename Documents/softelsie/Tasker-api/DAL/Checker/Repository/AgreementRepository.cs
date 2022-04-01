using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace DAL.Checker
{
    public class AgreementRepository : IAgreementRepository
    {
        private readonly CompanyDbContext context;
        private readonly UserManager<UserModel> userManager;
        public AgreementRepository(CompanyDbContext context, UserManager<UserModel> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<AgreementModel> Add(AgreementModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            AgreementTypeModel type = context.TAgreementType.Find(model.AgreementType.Id);
            model.AgreementType = type;
            UserModel owner = await userManager.FindByIdAsync(model.Owner.Id);
            model.Owner = owner;

            if(type.Id == 1)
            {
                model.possibleTaskCount = (int)(model.DateEnd - model.DateStart).TotalDays;
            }
            else if(type.Id == 2)
            {
                model.possibleTaskCount = (int)((model.DateEnd - model.DateStart).TotalDays/7);
            }
            else if (type.Id == 3)
            {
                model.possibleTaskCount = (int)((model.DateEnd - model.DateStart).TotalDays/30);
            }
            else
            {
                model.possibleTaskCount = 1;
            }

            context.TAgreement.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<IEnumerable<AgreementModel>> Find(Expression<Func<AgreementModel, bool>> predicte)
        {
            return await context.TAgreement
             .Include(a => a.AgreementType)
             .Where(predicte)
             .ToListAsync();
        }

        public async Task<AgreementModel> Get(int Id)
        {
            return await context
                .TAgreement
                .Include(a => a.taks)
                .Where(p => p.Id == Id && p.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AgreementModel>> GetAll()
        {
            var list = await context
                   .TAgreement
                   .Include(a => a.AgreementType)
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<AgreementModel>> GetByAssignee(UserModel assignee)
        {
            var list = await context
                   .TAgreement
                   .Where(p => p.Assignee.Id == assignee.Id && p.IsDeleted == false)
                   .ToListAsync();

            return list;
        }

        public async Task<AgreementModel> Update(AgreementModel model)
        {
            var UpdatedSubject = context.TAgreement.Attach(model);
            UpdatedSubject.State = EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
