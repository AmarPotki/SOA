using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log.ClearingLog{
    public class GetClearingLogDtoValidator : AbstractValidator<GetClearingLogDto>{
        public GetClearingLogDtoValidator()
        {
            RuleFor(x => x.ClearingRequestId).NotEmpty().WithMessage("شناسه نامعتبر است.");
            
        }
    }
}