using System;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
namespace RahyabServices.Business.Domain.Factories.Delinquent.Implementations{
    public class ChangeStatusLogFactory:IChangeStatusLogFactory{
        public ChangeStatusLog Create(int delinquentId, string status){
            var changeStatus = new ChangeStatusLog { Author = "spfarm", Created = DateTime.Now };
            changeStatus.SetCustomerDelinquentId(delinquentId);
            if (status == "0") changeStatus.StatusType = StatusType.Normal;
            if (status == "6") changeStatus.StatusType = StatusType.DueDate;
            if (status == "2") changeStatus.StatusType = StatusType.BadDebt;
            if (status == "5") changeStatus.StatusType = StatusType.BadDebt;
            if (status == "1") changeStatus.StatusType = StatusType.Expire;
            return changeStatus;
        }
    }
}