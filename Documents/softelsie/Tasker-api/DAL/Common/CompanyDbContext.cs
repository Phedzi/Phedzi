using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Checker;

namespace DAL.Common
{
    public class CompanyDbContext : IdentityDbContext<UserModel>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {

        }

        public DbSet<AgreementModel> TAgreement { get; set; }
        public DbSet<AgreementTypeModel> TAgreementType { get; set; }
        public DbSet<TaskModel> TTask { get; set; }
        public DbSet<StatusModel> TStatus { get; set; }
        public DbSet<ColorModel> TColor { get; set; }
        public DbSet<CheckingModel> TChecking { get; set; }
        public DbSet<UserTypeModel> TUserType { get; set; }
        public DbSet<MenuModel> TMenu { get; set; }
        public DbSet<SubMenuModel> TSubMenu { get; set; }
        public DbSet<TrackActionModel> TTrackAction { get; set; }

    }
}
