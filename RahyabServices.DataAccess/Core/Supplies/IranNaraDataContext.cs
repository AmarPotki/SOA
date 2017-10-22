using System;
using System.Data;
using System.Data.Entity;
using System.Reflection;
using RahyabServices.Business.Domain.Models.Supplies;
namespace RahyabServices.DataAccess.Core.Supplies{
    public class IranNaraDataContext : DbContext{
        public IranNaraDataContext(string nameOrConnectionStrin)
            : base(nameOrConnectionStrin)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
           Configuration.ValidateOnSaveEnabled = false;

        }

        public IranNaraDataContext()
            : base("IranNara")
        {
        
        }
        public DbSet<IranNaraChequeRequest> IranNaraChequeRequests { get; set; }
        public DbSet<RequestSerialId> RequestSerialIds { get; set; }
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder){
            if (modelBuilder == null)
            {
                throw new ArgumentException("modelBuilder");
            }
            modelBuilder.Entity<RequestSerialId>()
            .Property(e => e.IranNaraChequeRequestId)
            .HasColumnName("ABChequeRequest_Id");
            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("RahyabServices.DataAccess"));
        }
    }
}