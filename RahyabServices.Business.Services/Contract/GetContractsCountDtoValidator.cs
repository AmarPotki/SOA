using RahyabServices.Business.Dtos.Delinquent.Contracts;
using FluentValidation;
namespace RahyabServices.Business.Services.Contract
{
    public class GetContractsCountDtoValidator : AbstractValidator<GetContractsCountDto>
    {
        public GetContractsCountDtoValidator()
        {
            RuleFor(d => d.UserName).NotEmpty();
        }
    }
}