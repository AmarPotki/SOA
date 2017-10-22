using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
namespace RahyabServices.Business.Domain.Models.State{
    public class RequestSplitState:DelinquentState{
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new System.NotImplementedException();
        }
       
    }
}