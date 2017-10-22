﻿using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Notice;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log
{
    public class AddNoticeLogDtoValidator : AbstractValidator<AddNoticeLogDto>
    {
        public AddNoticeLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository)
        {
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد");
            RuleFor(x => x.DocumentUrl).NotEmpty().WithMessage("آدرس فایل نباید خالی باشد");
            RuleFor(x => x.LetterDate).NotNull().WithMessage("تاریخ نامه نباید خالی باشد");
            RuleFor(x => x.LetterNumber).NotNull().WithMessage("شماره نامه نباید خالی باشد");
        }
    }
}