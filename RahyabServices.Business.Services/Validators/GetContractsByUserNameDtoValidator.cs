using RahyabServices.Business.Dtos.Delinquent.Contracts;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators
{
    public class GetContractsByUserNameDtoValidator : AbstractValidator<GetContractsByUserNameDto>
    {
        public GetContractsByUserNameDtoValidator()
        {
            RuleFor(d => d.UserName).NotEmpty();
        }
    }
}