using FluentValidation;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.Business.Services.Validators.Supplies{
    public class GetAccountInformationDtqValidator : AbstractValidator<GetAccountInformationDtq>{
        public GetAccountInformationDtqValidator(IAccountInfoRepository accountInfoRepository){
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("شماره حساب را وارد کنید");
            When(x => x.BranchCode != "0000" && x.BranchCode !="4444", () =>{
                RuleFor(x => x.AccountNumber)
                    .MustAsync(async (dtq, accountNumber, cancel) =>
                        await
                            accountInfoRepository.IsvalidByBranchCodeAsync(dtq.AccountNumber,
                                dtq.BranchCode.StartsWith("0") ? dtq.BranchCode.Remove(0, 1) : dtq.BranchCode))
                    .WithMessage(" شماره حساب اشتباه است /شماره حساب مربوط به این شعبه نمی باشد ");
            });
            When(x => x.BranchCode == "4444", () => {
                RuleFor(x => x.AccountNumber)
                    .MustAsync(async (dtq, accountNumber, cancel) =>
                        await
                            accountInfoRepository.IsvalidAsync(dtq.AccountNumber))
                    .WithMessage(" شماره حساب اشتباه است /شماره حساب مربوط به این شعبه نمی باشد ");
            });
        }
    }
}