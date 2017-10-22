using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.GivingAChance{
    public class GetRequestGivingAChanceLogDtoValidator : AbstractValidator<GetRequestGivingAChanceLogDto>{
        public GetRequestGivingAChanceLogDtoValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}