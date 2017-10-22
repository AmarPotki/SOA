using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Models.State{
    public interface IState : IDelinquentEntity
    {
        DateTime? ExpireDate { get; set; }
        Task Handler(CustomerDelinquent customerDelinquent);
        int HistoryCustomerDelinquentId { get; set; }
    }
}