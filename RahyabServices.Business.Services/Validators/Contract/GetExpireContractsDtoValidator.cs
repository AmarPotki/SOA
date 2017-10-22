using RahyabServices.Business.Dtos.Delinquent.Contracts;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Contract
{
    public class GetExpireContractsDtoValidator : AbstractValidator<GetExpireContractsDto>
    {
        public GetExpireContractsDtoValidator()
        {
            RuleFor(d => d.UserName).NotEmpty().WithMessage("نام کاربری نامعتبر است");
        }
    }
}