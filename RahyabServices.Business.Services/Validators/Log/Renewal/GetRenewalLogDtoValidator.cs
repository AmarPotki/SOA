using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.Renewal{
    public class GetRenewalLogDtoValidator : AbstractValidator<GetRenewalLogDto>{
        public GetRenewalLogDtoValidator(){
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
        }
    }
}