using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.AbsorbResources;
using RahyabServices.Business.Services.Intefaces.AbsorbResources;
using RahyabServices.Common.Exceptions;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.AbsorbResources{
    public class AbsorbResourcesService: IAbsorbResourcesService{
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly IRequestRepository _requestRepository;
        public AbsorbResourcesService(ICustomerInfoRepository customerInfoRepository, IRequestRepository requestRepository){
            _customerInfoRepository = customerInfoRepository;
            _requestRepository = requestRepository;
        }
        public async Task<CustomerInformationDto> GetBrifCustomerInformation(GetCustomerInformationDtq customerInformationDto){
            var query = $"<View><Query><Where><Eq><FieldRef Name='CustomerNo'  ></FieldRef><Value Type='Text'>" +
            customerInformationDto.CustomerNumber + "</Value></Eq></Where></Query></View>";
            var items = _requestRepository.GetItems(query);
            if (items != null && items.Any()){
                var item = items.FirstOrDefault();
                throw new FaultException($"این شماره حساب قبلا به وسیله {item.FullName} ثبت شده است");
            }
            var customer =
               await
                   _customerInfoRepository.GetByCustomerNumberAsync(customerInformationDto.CustomerNumber);
            return new CustomerInformationDto {FullName = customer.FullNameManaged };
        }
    }
}