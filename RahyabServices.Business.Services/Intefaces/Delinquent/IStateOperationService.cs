using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
	public interface IStateOperationService
	{
		Task<bool> CheckExpireDateAndHandleOperationAsync(DateTime date);
	}
}
