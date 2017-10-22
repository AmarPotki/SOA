using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BankPerson;
using RahyabServices.Business.Domain.Models.VipBanking;

namespace RahyabServices.DataAccess.Core.BankPerson
{
    public class BankPersonDataContext : DbContext
    {
        public BankPersonDataContext(string nameOrConnectionStrin)
            : base(nameOrConnectionStrin)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public BankPersonDataContext()
            : base("Hamyar")
        {
           // Configuration.AutoDetectChangesEnabled = true;
            //Database.SetInitializer<RahyabServicesDataContext>(new MigrateDatabaseToLatestVersion<RahyabServicesDataContext, Migrations.Configuration>());
        }
        public DbSet<PersonAbUser> PersonAbUsers { get; set; }
        public DbSet<OrganizationUnits> OrganizationUnitses { get; set; }

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public virtual void MarkAsModified<TEntity>(TEntity instance) where TEntity : class
        {
            Entry(instance).State = EntityState.Modified;
        }

        public virtual void MarkAsModified<TEntity>(TEntity instance, params string[] parameters) where TEntity : class
        {
            Entry(instance).State = EntityState.Modified;

            foreach (var param in parameters)
            {
                Entry(instance).Property(param).IsModified = true;
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Potential>()
             .Property(e => e.ID)
             .HasPrecision(18, 0);

            modelBuilder.Entity<Potential>()
                .Property(e => e.CurrentRemaining)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Potential>()
                .Property(e => e.AccountCountMojudi)
                .HasPrecision(5, 0);

            modelBuilder.Entity<Potential>()
                .Property(e => e.LookupInventoryStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Potential>()
                .Property(e => e.InputCodeMojudy)
                .HasPrecision(2, 0);

            modelBuilder.Entity<Vip>()
                .Property(e => e.MeanTurnover)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Vip>()
                .Property(e => e.AccountCounts)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Vip>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.AccountCountMojudi)
                .HasPrecision(5, 0);

            modelBuilder.Entity<Vip>()
                .Property(e => e.InventoryStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.CurrentRemainingCustomer)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Vip>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);
            if (modelBuilder == null)
            {
                throw new ArgumentException("modelBuilder");
            }

            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("RahyabServices.DataAccess"));
        }
    }
}
