using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Models.State{
    public class RegisterState:DelinquentState{
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new System.NotImplementedException();
        }
    }
}