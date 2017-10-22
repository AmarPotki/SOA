using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;

namespace RahyabServices.DataAccess.Core.VipBanking
{
    public class VipBankingDataContext : DbContext
    {
        public VipBankingDataContext(string nameOrConnectionStrin)
            : base(nameOrConnectionStrin)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public VipBankingDataContext()
            : base("Vip")
        {
           // Configuration.AutoDetectChangesEnabled = true;
            //Database.SetInitializer<RahyabServicesDataContext>(new MigrateDatabaseToLatestVersion<RahyabServicesDataContext, Migrations.Configuration>());
        }

        public DbSet<Vip> Vips { get; set; }
        public DbSet<Potential> Potentials { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<VipDelinquent> VipDelinquents { get; set; }
        public DbSet<GeneralReport> GeneralReports { get; set; }
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
            modelBuilder.Entity<Cheque>()
              .Property(e => e.ChequeSumRejCash)
              .HasPrecision(38, 2);

            modelBuilder.Entity<Cheque>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);
            modelBuilder.Entity<GeneralReport>()
                     .Property(e => e.MeanTurnoverVipI)
                     .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverVipII)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverPrivate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.CurrentRemainingCustomerPrivate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverVipIII)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.AllCashVip)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.RatioCashPrivateDivAll)
                .HasPrecision(19, 4);

            //modelBuilder.Entity<GeneralReport>()
            //    .Property(e => e.RatioCashVipIDivAllVip)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<GeneralReport>()
            //    .Property(e => e.RatioCashVipIIDivAllVip)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<GeneralReport>()
            //    .Property(e => e.RatioCashVipIIIDivAllVip)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<GeneralReport>()
            //    .Property(e => e.RatioCashPrivateDivAllVip)
            //    .HasPrecision(19, 4);
            if (modelBuilder == null)
            {
                throw new ArgumentException("modelBuilder");
            }

            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("RahyabServices.DataAccess"));
        }
    }
}
