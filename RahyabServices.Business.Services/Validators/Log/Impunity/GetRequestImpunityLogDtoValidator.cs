using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.Impunity{
    public class GetRequestImpunityLogDtoValidator : AbstractValidator<GetRequestImpunityLogDto>{
        public GetRequestImpunityLogDtoValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}