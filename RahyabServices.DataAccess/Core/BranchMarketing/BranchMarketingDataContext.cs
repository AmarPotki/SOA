using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BranchMarketing;
using RahyabServices.Business.Domain.Models.VipBanking;
namespace RahyabServices.DataAccess.Core.BranchMarketing
{
   public class BranchMarketingDataContext : DbContext
    {
        public BranchMarketingDataContext(string nameOrConnectionStrin)
            : base(nameOrConnectionStrin)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public BranchMarketingDataContext()
            : base("BranchMarketing")
        {
            // Configuration.AutoDetectChangesEnabled = true;
            //Database.SetInitializer<RahyabServicesDataContext>(new MigrateDatabaseToLatestVersion<RahyabServicesDataContext, Migrations.Configuration>());
        }
       public DbSet<MainRevertCusts> MainRevertCustses { get; set; }
       public DbSet<DailyRevertCustomers> DailyRevertCustomerses { get; set; }
       
       
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

      

    }
}
