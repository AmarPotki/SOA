using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.Impunity{
    public class GetImpunityLogDtoValidator : AbstractValidator<GetImpunityLogDto>{
        public GetImpunityLogDtoValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}