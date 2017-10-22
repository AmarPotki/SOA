using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.DataAccess.Core
{
    public class NullDatabaseInitializer<TContext> :
        IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context) { }
    }
}
