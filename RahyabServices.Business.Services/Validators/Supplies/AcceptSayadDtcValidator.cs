using FluentValidation;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Validators.Supplies{
    public class AcceptSayadDtcValidator : AbstractValidator<AcceptSayadDtc>{
        public AcceptSayadDtcValidator(ISuppliesRequestRepository suppliesRequest){
            RuleFor(x => x.ItemId).NotEmpty().WithMessage("شناسه خالی است");
            //RuleFor(x => x.Key).NotEmpty();
            RuleFor(x => x.ItemId).Must((o, itemId, cancel) => suppliesRequest.IsValid(itemId,"6")).WithMessage("شناسه نامعتبر است");


        }
    }
}