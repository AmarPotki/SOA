﻿using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.WrittenNotice;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using FluentValidation;

namespace RahyabServices.Business.Services.Validators.Log
{
    public class AddWrittenNoticeLogDtoValidator : AbstractValidator<AddWrittenNoticeLogDto>
    {
        public AddWrittenNoticeLogDtoValidator(ICustomerDelinquentRepository customerDelinquentRepository)
        {
            RuleFor(x => x.AuthorUserName).NotEmpty().WithMessage("نام کاربری را پر کنید");
            RuleFor(x => x.CustomerDelinquentId).GreaterThan(0).WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.CustomerDelinquentId)
                .MustAsync(async (o, customerDelinquentId, cancel) => await customerDelinquentRepository.IsExistAsync(customerDelinquentId))
                .WithMessage("شناسه نامعتبر است");
            RuleFor(x => x.Description).NotEmpty().WithMessage("توضیحات نباید خالی باشد");
        }
    }
}