﻿using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;
namespace RahyabServices.Business.Services.Validators.Log{
    public class AddGivingAChanceLogDtoValidator : AbstractValidator<AddGivingAChanceLogDto>{
        public AddGivingAChanceLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository){
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("تعداد نباید خالی باشد");
            RuleFor(x => x.DocumentUrl).NotEmpty().WithMessage("آدرس فایل نباید خالی باشد");
            RuleFor(x => x.LegislationDate).NotNull().WithMessage("تاریخ مصوبه نباید خالی باشد");
            RuleFor(x => x.ExpireDate).NotNull().WithMessage("تاریخ انقضا نباید خالی باشد");
        }
    }
}