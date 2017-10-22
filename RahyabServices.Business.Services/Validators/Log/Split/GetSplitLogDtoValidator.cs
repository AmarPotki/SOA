using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class GetSplitLogDtoValidator : AbstractValidator<GetSplitLogDto>{
        public GetSplitLogDtoValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}