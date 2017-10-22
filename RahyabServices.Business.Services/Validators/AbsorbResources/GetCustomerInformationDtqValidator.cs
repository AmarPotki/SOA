using FluentValidation;
using RahyabServices.Business.Dtos.AbsorbResources;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.Business.Services.Validators.AbsorbResources{
    public class GetCustomerInformationDtqValidator : AbstractValidator<GetCustomerInformationDtq>{
        public GetCustomerInformationDtqValidator(ICustomerInfoRepository customerInfoRepository){
            RuleFor(x => x.CustomerNumber)
                .MustAsync((o, customerNumber, cancle) => customerInfoRepository.IsExistAsync(customerNumber))
                .WithMessage("شماره حساب نادرست است");
            
        }
    }
}