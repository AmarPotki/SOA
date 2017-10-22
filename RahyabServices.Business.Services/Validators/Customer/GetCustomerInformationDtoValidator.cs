using RahyabServices.Business.Dtos.Delinquent.Customer;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Customer
{
    public class GetCustomerInformationDtoValidator : AbstractValidator<GetCustomerInformationDto>
    {
        public GetCustomerInformationDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository)
        {
            RuleFor(d => d.CustomerNumber).NotEmpty().WithMessage("شماره مشتری نباید خالی باشد");
            RuleFor(d => d.CustomerNumber)
                .MustAsync(async (o, customerNumber, cancel) => await customerDelinquentRepository.IsExistAsync(customerNumber))
                .WithMessage("شماره مشتری نامعتبر است");
        }
    }
}