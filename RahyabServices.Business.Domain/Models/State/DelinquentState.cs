using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Models.State{
    public abstract class DelinquentState : IState
    {
        public  DateTime? ExpireDate { get; set; }
        public abstract Task Handler(CustomerDelinquent customerDelinquent);
        public int HistoryCustomerDelinquentId { get; set; }
        public int Id { get; set; }
    }
}