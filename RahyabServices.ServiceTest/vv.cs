namespace RahyabServices.ServiceTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VV : DbContext
    {
        public VV()
            : base("name=VV")
        {
        }

        public virtual DbSet<GeneralReport> GeneralReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.CurrentRemainingCustomerVipI)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverVipI)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.CurrentRemainingCustomerVipII)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverVipII)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverPrivate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.CurrentRemainingCustomerVipIII)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.CurrentRemainingCustomerPrivate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.MeanTurnoverVipIII)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.RatioCashVipIDivAllVip)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.RatioCashVipIIDivAllVip)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.RatioCashVipIIIDivAllVip)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.RatioCashPrivateDivAllVip)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GeneralReport>()
                .Property(e => e.AllCashVip)
                .HasPrecision(19, 4);
        }
    }
}
