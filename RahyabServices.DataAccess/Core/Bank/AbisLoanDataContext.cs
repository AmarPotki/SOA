using System.Data.Entity;
namespace RahyabServices.DataAccess.Core.Bank{
    public class AbisLoanDataContext : DbContext
    {
        public AbisLoanDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            //readonly Context
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //connection timeout
            Database.CommandTimeout = 20 * 60;
        }
        public AbisLoanDataContext()
            : base("AbisLoan")
        {

        }
        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}