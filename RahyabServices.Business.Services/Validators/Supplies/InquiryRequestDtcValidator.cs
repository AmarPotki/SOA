using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Validators.Supplies{
    public class InquiryRequestDtcValidator : AbstractValidator<InquiryRequestDtc>{
        private readonly ISuppliesRequestRepository _repository;
        public InquiryRequestDtcValidator(ISuppliesRequestRepository repository){
            _repository = repository;
            RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("شناسه ایتم نامعتبر است");
            RuleFor(x => x.ItemId)
                .Must((o, itemId, cancel) => repository.IsValid(itemId,"0"))
                .WithMessage("شناسه ایتم نامعتبر است");
        }
    }

}