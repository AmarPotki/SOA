namespace RahyabServices.ServiceTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VIPBank : DbContext
    {
        public VIPBank()
            : base("name=VIPBank")
        {
        }

        public virtual DbSet<VIP> VIPs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VIP>()
                .Property(e => e.MeanTurnover)
                .HasPrecision(19, 4);

            modelBuilder.Entity<VIP>()
                .Property(e => e.AccountCounts)
                .HasPrecision(18, 0);

            modelBuilder.Entity<VIP>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<VIP>()
                .Property(e => e.AccountCount_Mojudi_)
                .HasPrecision(5, 0);

            modelBuilder.Entity<VIP>()
                .Property(e => e.InventoryStatus)
                .IsUnicode(false);

            modelBuilder.Entity<VIP>()
                .Property(e => e.CurrentRemainingCustomer)
                .HasPrecision(19, 4);

            modelBuilder.Entity<VIP>()
                .Property(e => e.ID)
                .HasPrecision(18, 0);
        }
    }
}
