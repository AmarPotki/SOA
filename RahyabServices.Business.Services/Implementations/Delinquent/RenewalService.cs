using System;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.State;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class RenewalService : IRenewalService{
        private readonly IBranchPrivilegesService _branchPrivilegesService;
        private readonly ICryptographer _cryptographer;
        private readonly ICustomerDelinquentRepository _customerDelinquentRepository;
        private readonly IHrFacade _hrFacade;
        private readonly IImpunityLogListRepository _impunityLogListRepository;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ILogPrivilegeService _logPrivilegeService;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationRepository _notificationRepository;
        public RenewalService(IBranchPrivilegesService branchPrivilegesService, ICryptographer cryptographer,
            ICustomerDelinquentRepository customerDelinquentRepository, IHrFacade hrFacade,
            IImpunityLogListRepository impunityLogListRepository, ILogBaseRepository logBaseRepository,
            ILogPrivilegeService logPrivilegeService, INotificationFactory notificationFactory,
            INotificationRepository notificationRepository){
            _branchPrivilegesService = branchPrivilegesService;
            _cryptographer = cryptographer;
            _customerDelinquentRepository = customerDelinquentRepository;
            _hrFacade = hrFacade;
            _impunityLogListRepository = impunityLogListRepository;
            _logBaseRepository = logBaseRepository;
            _logPrivilegeService = logPrivilegeService;
            _notificationFactory = notificationFactory;
            _notificationRepository = notificationRepository;
        }
        public async Task AddRenewalLogAsync(AddRenewalLogDto dto){
            var customerDelinquent = await
                _customerDelinquentRepository.GetCustomerDelinquentWithState(dto.CustomerDelinquentId);
            var renewalSate = new RenewalStateHandler(dto);
            customerDelinquent.SetState(renewalSate.Id);
            await _customerDelinquentRepository.SaveAsync(customerDelinquent);
        }
        public async Task<bool> CheckPrivilegeAddRenewalLogAsync(AddRenewalLogDto dto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(dto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, dto);
        }
        public async Task<bool> CheckPrivilegeEditRenewalLogAsync(EditRenewalLogDto dto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(dto.AuthorUserName));
            return await _branchPrivilegesService.IsValidPrivilege(branchCode, dto);
        }       
        public async Task<RenewalLogDto> GetRenewalLogAsync(GetRenewalLogDto dto){
            var renewalAChanceLog = await _logBaseRepository.OneAsync(dto.RequestId);
            var log = (RenewalLog) renewalAChanceLog;
            return new RenewalLogDto
            {
                LegislationDate = log.LegislationDate,
                FacilityNumber = log.FacilityNumber,
                InterestRate = log.InterestRate,
                AuthorUserName = log.Author
            };
        }
        public async Task EditRenewalAsync(EditRenewalLogDto editRenewalLogDto){
            var renewalLog = Mapper.Map<EditRenewalLogDto, RenewalLog>(editRenewalLogDto);
            var personalCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(editRenewalLogDto.AuthorUserName));
            renewalLog.Created = DateTime.Now;
            renewalLog.Author = personalCode;
            renewalLog.CustomerDelinquentId = editRenewalLogDto.CustomerDelinquentId;
            await _logBaseRepository.SaveAsync(renewalLog);
        }
    }
}