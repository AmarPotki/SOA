using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.VipBanking;

namespace RahyabServices.Business.Contracts.Interfaces
{
    [ServiceContract]
    public interface IVipBankingRestContract
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetAllVips/{key}/?skip={skip}&take={take}")]
        Task<AllVipDto> GetAllVip(string key, string skip, string take);
        [OperationContract]
        [WebGet(UriTemplate = "GetAllPotential/{key}/?skip={skip}&take={take}")]
        Task<AllPotentialDto> GetAllPotential(string key, string skip, string take);
        [OperationContract]
        [WebGet(UriTemplate = "GetAllVipDelinquent/{key}/?skip={skip}&take={take}")]
        Task<AllVipDelinquentDto> GetAllDelinquent(string key, string skip, string take);
        [OperationContract]
        [WebGet(UriTemplate = "GetVipDelinquents/{key}/?customerNumber={customerNumber}&skip={skip}&take={take}")]
        Task<AllVipDelinquentDto> GetDelinquents(string key,string customerNumber, string skip, string take);
        [OperationContract]
        [WebGet(UriTemplate = "GetAllCheque/{key}/?skip={skip}&take={take}")]
        Task<AllChequeDto> GetAllCheque(string key, string skip, string take);
        [OperationContract]
        [WebGet(UriTemplate = "GetCheques/{key}/?customerNumber={customerNumber}&skip={skip}&take={take}")]
        Task<AllChequeDto> GetCheques(string key,string customerNumber, string skip, string take);
        [OperationContract]
        [WebGet(UriTemplate = "GetMaxGeneralReport/{key}")]
        Task<GeneralReportDto> GetMaxGeneralReport(string key);
        [OperationContract]
        [WebGet(UriTemplate = "GetThirtyLastBal/{key}/{customerNumber}")]
        Task<IEnumerable<LastBalDetailDto>> GetThirtyLastBal(string key,string customerNumber);
        [OperationContract]
        [WebGet(UriTemplate = "GetVipByCustomerNumber/{key}/{customerNumber}")]
        Task<VipDto> GetVipByCustomerNumber(string key, string customerNumber);
        [OperationContract]
        [WebGet(UriTemplate = "GetPotentialByCustomerNumber/{key}/{customerNumber}")]
        Task<PotentialDto> GetPotentialByCustomerNumber(string key, string customerNumber);

    }
}