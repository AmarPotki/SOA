using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.DataAccess.Core.Sharepoint;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces
{
   public interface IBranchCodeSystemRepository : ISharepointRepository<BranchCodeSystem>{
      
   }
}
