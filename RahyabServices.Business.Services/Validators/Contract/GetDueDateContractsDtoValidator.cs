using RahyabServices.Business.Dtos.Delinquent.Contracts;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Contract
{
    public class GetDueDateContractsDtoValidator : AbstractValidator<GetDueDateContractsDto>
    {
        public GetDueDateContractsDtoValidator()
        {
            RuleFor(d => d.UserName).NotEmpty();
        }
    }
}