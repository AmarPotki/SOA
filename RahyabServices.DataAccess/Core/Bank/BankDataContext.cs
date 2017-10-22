using System.Data.Entity;
using RahyabServices.Business.Domain.Models.Bank;
namespace RahyabServices.DataAccess.Core.Bank
{
    public class BankDataContext : DbContext
    {
        public BankDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //readonly Context
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //connection timeout
            Database.CommandTimeout = 20*60;
        }

        public BankDataContext()
            : base("TAT_DWBI_ODS")
        {

        }

        public virtual DbSet<RPTFT> RPTFTS { get; set; }
        public virtual DbSet<AccountInfo> AccountInfo { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresss { get; set; }
        public virtual DbSet<CustomerInfo> CustomerInfo { get; set; }
        public virtual DbSet<RptftBond> RptftBonds { get; set; }
        public virtual DbSet<RptftBondDelinquent> RptftBondRahyabServicess { get; set; }
        public virtual DbSet<RptftGuarantor> RptftGuarantors { get; set; }
        public virtual DbSet<AbisDetail> AbisDetails { get; set; }
        public virtual DbSet<BankIranDetail> BankIranDetails { get; set; }
        public virtual DbSet<Monitor> Monitors { get; set; }
        public virtual DbSet<Guarantee> Guarantees { get; set; }
        public virtual DbSet<GuaranteeDetail>  GuaranteeDetails { get; set; }
        public virtual DbSet<DelinquentTr> RahyabServicesTrs { get; set; }
        public virtual DbSet<LastBal> LastBals { get; set; }

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RPTFT>()
                .Property(e => e.MaturityDate)
                .IsUnicode(false);

            modelBuilder.Entity<RPTFT>()
                .Property(e => e.StartDate)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerAddress>()
                .Property(e => e.MobilePhone)
                .IsFixedLength();

            modelBuilder.Entity<RPTFT>()
                .Property(e => e.MaturityDate)
                .IsUnicode(false);

            modelBuilder.Entity<RPTFT>()
                .Property(e => e.StartDate)
                .IsUnicode(false);

            //modelBuilder.Entity<RPTFT>()
            //    .Property(e => e.ApprovedAmount)
            //    .HasPrecision(18, 0);

            //modelBuilder.Entity<CustomerInfo>()
            //    .Property(e => e.CUSTSAL)
            //    .HasPrecision(38, 2);

            //modelBuilder.Configurations.Add(new RPTFTMap());
        }
    }
}
