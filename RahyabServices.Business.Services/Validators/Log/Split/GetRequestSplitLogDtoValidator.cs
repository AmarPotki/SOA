using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class GetRequestSplitLogDtoValidator : AbstractValidator<GetRequestSplitLogDto>{
        public GetRequestSplitLogDtoValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}