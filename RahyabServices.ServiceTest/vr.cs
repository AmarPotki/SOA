namespace RahyabServices.ServiceTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class vr : DbContext
    {
        public vr()
            : base("name=vr")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
        }
    }
}
