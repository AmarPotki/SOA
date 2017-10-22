using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.Customer;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Extensions;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Bank{
    public class CustomerAccountService : ICustomerAccountService{
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly ICustomerInfoRepository _customerInfoRepository;

        public CustomerAccountService(ICustomerInfoRepository customerInfoRepository,
            ICustomerAddressRepository customerAddressRepository,
            ICustomerDelinquentRepository customerDelinquentRepository){
            _customerInfoRepository = customerInfoRepository;
            _customerAddressRepository = customerAddressRepository;
            _customerDelinquentRepository = customerDelinquentRepository;;

        }
        public string GetCustomerMobileNumber(string customerNumber){
            return Task.Run(async () => await GetCustomerMobileNumberAsync(customerNumber)).Result;
        }
        public async Task<string> GetCustomerMobileNumberAsync(string customerNumber){
            try{
                var customerInfo = await _customerInfoRepository.GetByCustomerNumberAsync(customerNumber);
                var customerAddress = await _customerAddressRepository.GetByCustomerNumberAsync(customerNumber);
                var mobileNumber = customerInfo.Telephone.Trim().IsCellPhone()
                    ? customerInfo.Telephone.Trim()
                    : customerAddress.HomePhone.Trim().IsCellPhone()
                        ? customerAddress.HomePhone.Trim()
                        : customerAddress.BusinessPhone.Trim().IsCellPhone()
                            ? customerAddress.BusinessPhone.Trim()
                            : customerAddress.MobilePhone.Trim().IsCellPhone()
                                ? customerAddress.MobilePhone.Trim()
                                : string.Empty;
                return mobileNumber;
            }
            catch {
                return string.Empty;
            }
        }
        public async Task<CustomerInformationDto> GetCustomerInformationAsync(
            GetCustomerInformationDto customerInformationDto){
            var customerDelinquent =
                await
                    _customerDelinquentRepository.GetContractByCustomerNumberAsync(customerInformationDto.CustomerNumber);
            ////await GetCustomerMobileNumberAsync(customerInformationDto.CustomerNumber);// await _customerAddressRepository.GetCellPhoneAsync(customerInformationDto.CustomerNumber);
            return await ConvertToCustomerInformationAsync(customerDelinquent);
        }
        public string GetSheba(string accountNumber){
            string ibn;
            try {
                ibn = IbanConvertor.ConvertAccountToIban(long.Parse(accountNumber), AccountType.Sepordeh);
            }
            catch (Exception ex) {
                ibn = ex.Message;
            }
            return ibn;
        }
        protected async Task<CustomerInformationDto> ConvertToCustomerInformationAsync(
            CustomerDelinquent customerDelinquent){
            var customer = await _customerInfoRepository.GetByCustomerNumberAsync(customerDelinquent.CustomerNumber);
            if (customer == null){
                return new CustomerInformationDto
                {
                    CellPhone = customerDelinquent.MobileNumber,
                    CustomerCode = customerDelinquent.CustomerNumber,
                    FullName = "",
                    NationalCode = ""
                };
            }
            var fullNameStr = customer.FullNameManaged;
                //string.Format("{0} {1}", customer.FirstName, customer.LastName);
            return new CustomerInformationDto
            {
                CellPhone = customerDelinquent.MobileNumber,
                CustomerCode = customerDelinquent.CustomerNumber,
                FullName = fullNameStr,
                NationalCode = customer.EconomicCode.Trim()
            };
        }
    }
}