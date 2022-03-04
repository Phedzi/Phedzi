using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Budget;

namespace DAL.Common
{
    public class BudgettaDbContext : IdentityDbContext<UserModel>
    {
        public BudgettaDbContext(DbContextOptions<BudgettaDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserTypeModel> TUserType { get; set; }
        public DbSet<BudgetModel> TBudget { get; set; }
        public DbSet<CategoryModel> TCategory { get; set; }
        public DbSet<ItemModel> TItem { get; set; }
        public DbSet<ItemTypeModel> TItemType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedRoles(modelBuilder);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<UserTypeModel>().HasData(
                new UserTypeModel()
                {
                    Id = 1,
                    Name = "ApiUser",
                    Weight = 1,
                    DefaultUrl = "User",
                    Descriprion = "Basic User Account",
                    CreatedAt = DateTime.Now
                },

                new UserTypeModel()
                {
                    Id = 2,
                    Name = "SAdmin",
                    Weight = 1000,
                    DefaultUrl = "SAdmin url",
                    Descriprion = "Administrator Account",
                    CreatedAt = DateTime.Now
                }
                );
        }

    }
}
