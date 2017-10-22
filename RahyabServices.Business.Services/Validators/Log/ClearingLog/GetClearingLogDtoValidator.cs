using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.ClearingLog{
    public class GetRequestClearingLogDtoValidator : AbstractValidator<GetRequestClearingLogDto>{
        public GetRequestClearingLogDtoValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}