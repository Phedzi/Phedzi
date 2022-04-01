using DAL.Common;
using Models.Checker;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Checker
{
    public class TrackActionRepository : ITrackActionRepository
    {
        private readonly CompanyDbContext context;
        public TrackActionRepository(CompanyDbContext context)
        {
            this.context = context;
        }
        public async Task<TrackActionModel> Add(TrackActionModel model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            CheckingModel cheking = (CheckingModel) context.TTask.Find(model.Task.Id);
            model.Task = cheking;
            context.TTrackAction.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<TrackActionModel> Get(int Id)
        {
            return await context
                 .TTrackAction
                 .Include(t => t.Task)
                 .Where(p => p.Id == Id && p.IsDeleted == false)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TrackActionModel>> GetAll()
        {
            return await context
                   .TTrackAction
                   .Where(p => p.IsDeleted == false)
                   .ToListAsync();
        }

        public async Task<TrackActionModel> Update(TrackActionModel Item)
        {
            var UpdatedSubject = context.TTrackAction.Attach(Item);
            UpdatedSubject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return UpdatedSubject.Entity;
        }
    }
}
