using System.Data.Entity.Migrations;
using RahyabServices.DataAccess.Core.Delinquent;
namespace RahyabServices.DataAccess.Migrations{
    internal sealed class Configuration : DbMigrationsConfiguration<DelinquentDataContext>{
        public Configuration(){
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }
        protected override void Seed(DelinquentDataContext context){
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}