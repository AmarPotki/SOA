using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log.Split{
    public class CancelRequestSplitDtoValidator : AbstractValidator<CancelRequestSplitDto>{
        public CancelRequestSplitDtoValidator(ILogBaseRepository logBaseRepository){
            RuleFor(x => x.RequestLogId)
               .MustAsync(async (o, requestLogId, cancel) => await logBaseRepository.IsExistAllowEditRequestSplitLog(requestLogId, o.CustomerDelinquentId))
               .WithMessage("درخواست نامعتبر است");

        }
    }
}