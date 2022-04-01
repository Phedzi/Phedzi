using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class ColorRepository : IColorRepository
    {
        private readonly CompanyDbContext context;

        public ColorRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public async Task<ColorModel> Add(ColorModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            context.TColor.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<ColorModel> Get(int Id)
        {
            return await context
                .TColor
                .Where(p => p.Id == Id && p.IsDeleted == false)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ColorModel>> GetAll()
        {
            var list = await context
                   .TColor
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();

            return list;
        }

        public async Task<ColorModel> Update(ColorModel model)
        {
            var UpdatedSubject = context.TColor.Attach(model);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
