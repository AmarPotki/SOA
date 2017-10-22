using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Dto;
using RahyabServices.Common.Exceptions;
using RahyabServices.Common.Logging;
using FluentValidation;
using FluentValidation.Results;
using RahyabServices.Common.Extensions;
using FaultException = RahyabServices.Common.Exceptions.FaultException;

namespace RahyabServices.Business.Contracts
{
    public abstract class ContractBase
    {
        private readonly IValidatorFactory _validatorFactory;
        // private readonly IAuthorizationService _authorizationService;
        private readonly ICryptographer _cryptographer;
        private readonly ILogger _logger;
        private readonly ISharepointAuthorizationService _sharepointAuthorizationService;

        protected ContractBase(IValidatorFactory validatorFactory,
            ICryptographer cryptographer, ILogger logger, ISharepointAuthorizationService sharepointAuthorizationService)
        {
            _validatorFactory = validatorFactory;
            //_authorizationService = authorizationService;
            _cryptographer = cryptographer;
            _logger = logger;
            _sharepointAuthorizationService = sharepointAuthorizationService;
        }

        protected async Task<T> ExecuteFaultHandledOperation<T>(Func<Task<T>> codetoExecute)
        {
            try
            {
                return await codetoExecute();
            }
            catch (FaultException faultException)
            {
                _logger.Error(new FaultDto("ContractBase", faultException.GetMessage(), faultException.StackTrace, FaultSource.Endpoint));
                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(new FaultDto("ContractBase", exception.GetMessage(), exception.StackTrace, FaultSource.Endpoint));
                //throw new FaultException(exception.Message);
                throw new FaultException("مشکلی پیش امده با مدیر سیستم تماس بگیرید");
            }
        }

        protected async Task ExecuteFaultHandledOperation(Func<Task> codetoExecute)
        {
            try
            {
                await codetoExecute();
            }
            catch (FaultException faultException)
            {
                _logger.Error(new FaultDto("ContractBase", faultException.GetMessage(), faultException.StackTrace, FaultSource.Endpoint));
                throw;
            }
            catch (Exception exception)
            {
                _logger.Error(new FaultDto("ContractBase", exception.GetMessage(), exception.StackTrace, FaultSource.Endpoint));
                throw new FaultException("مشکلی پیش امده با مدیر سیستم تماس بگیرید");
            }
        }

        protected async Task<T> ValidateThenExecuteFaultHandledOperation<T, TDto>(Func<Task<T>> codetoExecute,
            object objectToValidate)
        {
            await Validate<TDto>(objectToValidate);
            return await ExecuteFaultHandledOperation(codetoExecute);
        }

        protected async Task ValidateThenExecuteFaultHandledOperation<TDto>(Func<Task> codetoExecute,
            object objectToValidate)
        {
            await Validate<TDto>(objectToValidate);
            await ExecuteFaultHandledOperation(codetoExecute);
        }

        protected async Task Validate<TDto>(object objectToValidate)
        {
            //   _logger.Info(new FaultDto("ContractBase", "HI", FaultSource.Endpoint));
            var validate = objectToValidate as ManagerRequestDto;
            if (validate != null)
            {
                var userNameDecrypt = _cryptographer.Decrypt(validate.UserName);
                try
                {
                    if (!_sharepointAuthorizationService.CheckUserInAdminGroup(userNameDecrypt) && !_sharepointAuthorizationService.CheckUserInBranchLevelGroup(userNameDecrypt))
                        throw new FaultException("نام کابری نامعتبر است");
                }
                catch (Exception ex){

                    throw new FaultException(ex.Message);
                } 
            }
            else{
                var dto = objectToValidate as SharepointRequestDto;
                if(dto != null)
                {
                    var encryptableDto = dto;
                    try
                    {
                        encryptableDto.Info = _cryptographer.DecryptCryptoInfo(encryptableDto.Key);
                        if (encryptableDto.Info.Ticks < DateTime.Now.Ticks) throw new FaultException<AuthenticationFault>(new AuthenticationFault("the Key has been expired"), "the Key has been expired");
                        //if (! _sharepointAuthorizationService.IsValidUserName(encryptableDto.Info.UserName,encryptableDto.SiteCollection))
                        //{
                        //    throw new FaultException<AuthenticationFault>(new AuthenticationFault("the Key has been expired"), "the Key has been expired");
                        //}
                        encryptableDto.UserName = encryptableDto.Info.UserName.Replace(@"i:0#.w|ab\","");
                    }
                    catch (Exception e)
                    {
                        _logger.Warn(new FaultDto {Location = "ContractBase",Message =e.GetMessage() ,StackTrace = e.StackTrace});
                        throw new FaultException<AuthenticationFault>(new AuthenticationFault("the Key is Not valid"), "the Key is Not valid");
                    }
                }
                else{ 
                    // if (!_hrFacade.IsValidUserName(userNameDecrypt))
                    //throw new FaultException("نام کابری نامعتبر است");
                    //Check tehran Branch 
                    // var ouId = _hrFacade.GetBranchRegionId(userNameDecrypt);
                    //if (ouId != "9" && ouId != "10")  throw new FaultException("نام کابری نامعتبر است"); 
                
                }
            }
            var result = new ValidationResult();
            try
            {
                var validator = _validatorFactory.GetValidator<TDto>();
                if (validator == null) return;

                result = validator.Validate(objectToValidate);
                if (result.IsValid) return;
            }
            catch (Exception ex)
            {
                _logger.Error(new FaultDto("ContractBase - Validate", ex.Message, FaultSource.Endpoint));
                throw new FaultException("مشکلی به وجود آمده اندکی بعد دوباره تلاش کنید");

            }

            var validationException = new ValidationFault(result.Errors);
            var faultException = new FaultException<ValidationFault>(validationException, validationException.Message);

            throw faultException;
        }
    }
}
