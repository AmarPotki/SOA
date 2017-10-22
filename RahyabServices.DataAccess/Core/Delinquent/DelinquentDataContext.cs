using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.DataAccess.Core.Delinquent
{
    public class DelinquentDataContext : DbContext
    {
        public DelinquentDataContext(string nameOrConnectionStrin)
            : base(nameOrConnectionStrin)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DelinquentDataContext()
            : base("Delinquent")
        {
            Configuration.AutoDetectChangesEnabled = true;
            //Database.SetInitializer<RahyabServicesDataContext>(new MigrateDatabaseToLatestVersion<RahyabServicesDataContext, Migrations.Configuration>());
        }

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
            if (modelBuilder == null)
            {
                throw new ArgumentException("modelBuilder");
            }

            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("RahyabServices.DataAccess"));
        }
    }
}
