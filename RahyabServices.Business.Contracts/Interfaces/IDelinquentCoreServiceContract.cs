using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Common.Logging;

namespace RahyabServices.Business.Contracts.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IDelinquentCoreServiceCallback))]
    public interface IDelinquentCoreServiceContract
    {
        [OperationContract(IsOneWay = true)]
        Task UpdateWorkingCopyOfDatabaseAsync();

        [OperationContract(IsOneWay = true)]
        Task UpdateWorkingCopyOfAbisDatabaseAsync();

        [OperationContract(IsOneWay = true)]
        Task UpdateWorkingCopyOfBankIranDatabaseAsync();

        [OperationContract(IsOneWay = true)]
        Task UpdateWorkingCopyOfGuaranteeDatabaseAsync();

        [OperationContract(IsOneWay = true)]
        Task UpdateBranchClaim();

        // [OperationContract(IsOneWay = true)]
        //Task SendSmsAsync(string number, string message);

        [OperationContract(IsOneWay = true)]
        Task SendDayRemainSmsAsync();

        [OperationContract(IsOneWay = true)]
        Task SendWeekRemainSmsAsync();

        [OperationContract(IsOneWay = true)]
        Task SendMonthRemainSmsAsync();

        [OperationContract(IsOneWay = true)]
        Task CheckAndUpdateSmsDeliveryStatusAsync();

        [OperationContract(IsOneWay = true)]
        Task CheckExpireDateAndHandleOperationAsync(DateTime date, BankType bankType);
    }
}
