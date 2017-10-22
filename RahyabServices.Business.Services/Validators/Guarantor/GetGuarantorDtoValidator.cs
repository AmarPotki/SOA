using RahyabServices.Business.Dtos.Bank;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Guarantor{
    public class GetGuarantorDtoValidator : AbstractValidator<GetGuarantorsDto>{
        public GetGuarantorDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository){
            RuleFor(d => d.CustomerDelinquentId).NotEmpty().WithMessage("شماره مشتری نباید خالی باشد");
            RuleFor(d => d.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شماره مشتری نامعتبر است");
        }
    }
}