using FluentValidation;
using RahyabServices.Business.Dtos.VipBanking;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.Business.Services.Validators.VipBanking{
    public class GetVipByCustomerNumberDtqValidator : AbstractValidator<GetVipByCustomerNumberDtq>{
        public GetVipByCustomerNumberDtqValidator(ICustomerInfoRepository customerInfoRepository){
            RuleFor(x => x.CustomerNumber).NotEmpty().WithMessage("customernumber is not valid");
            RuleFor(d => d.CustomerNumber)
          .MustAsync(async (o, customerNumber, cancel) => await customerInfoRepository.IsExistAsync(customerNumber))
          .WithMessage("شماره مشتری نامعتبر است");
        }
    }
}