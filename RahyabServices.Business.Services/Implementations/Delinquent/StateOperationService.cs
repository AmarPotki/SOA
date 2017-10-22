using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;

namespace RahyabServices.Business.Services.Implementations.Delinquent
{
	public class StateOperationService : IStateOperationService
	{
	    private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
		public StateOperationService( ICustomerDelinquentRepository customerDelinquentRepository){
		    
		    _customerDelinquentRepository = customerDelinquentRepository;
		}
	    public async Task<bool> CheckExpireDateAndHandleOperationAsync(DateTime date){
            var customerDelinquents = await _customerDelinquentRepository.GetExpiredEvents(date.Date);
	        foreach (var cd in customerDelinquents){
                DelinquentState ds;
                switch (cd.CurrentState.GetType().Name)
                {
                    case "RegisterState":
                        ds = new RegisterStateHandler();
                   break;
                    case "FirstAnnounceState":
                        ds = new FirstAnnounceStateHandler();
                        break;
                    case "SecondAnnounceState":
                           ds = new SecondAnnounceStateHandler ();
                        break;
                    case "ThirdAnnounceState":
                        ds = new ThirdAnnounceStateHandler();
                        break;
                    case "FirstWrittenNoticeState":
                        ds = new FirstWrittenNoticeDtoStateHandler();
                        break;
                    case "SecondWrittenNoticeState":
                        ds = new SecondWrittenNoticeStateHandler();
                        break;
                    case "ThirdWrittenNoticeState":
                        ds = new ThirdWrittenNoticeStateHandler();
                        break;
                    case "SplitState":
                        ds = new SplitStateHandler();
                        break;
                    case "GivingAChanceState":
                        ds = new GivingAChanceStateHandler(cd.CurrentState.Id);
                        break;
                    case "ImpunityForCrimesState":
                        ds = new ImpunityForCrimesStateHandler();
                        break;
                    //case "RenewalState":
                    //    ds = new RenewalStateHandler();
                    //    break;
                    //baghie handler ha badan ezafe shavad
                    default :
                        continue;
                }
                await ds.Handler(cd);
			}
	        return true;
	    }
	}
}