using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Exceptions;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class BranchService:IBranchService{
        private readonly IBranchRepository _branchRepository;
        private readonly IHrFacade _hrFacade;
        private readonly ICryptographer _cryptographer;
        private readonly ISharepointAuthorizationService _sharepointAuthorizationService;
        public BranchService(IBranchRepository branchRepository, IHrFacade hrFacade, ICryptographer cryptographer, ISharepointAuthorizationService sharepointAuthorizationService){
            _branchRepository = branchRepository;
            _hrFacade = hrFacade;
            _cryptographer = cryptographer;
            _sharepointAuthorizationService = sharepointAuthorizationService;
        }
        public async Task<BranchDto> GetBranchInformationAsync(GetBranchDto getBranchDto){
            var branchCode = _hrFacade.GetBranchCode(_cryptographer.Decrypt(getBranchDto.UserName));
            var branch = await _branchRepository.GetBranchByCode(branchCode);
            return Mapper.Map<Branch,BranchDto>(branch);
        }
        public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync(GetAllBranchDto allBranchDto){
            IEnumerable<Branch> branches = new List<Branch>();
            var decryptedUser = _cryptographer.Decrypt(allBranchDto.UserName);

            if (_sharepointAuthorizationService.CheckUserInAdminGroup(decryptedUser))
            {
                 branches= await _branchRepository.GetAllAsNoTracking();
            }
            else if (_sharepointAuthorizationService.CheckUserInBranchLevelGroup(decryptedUser)){
                var branchCode = _hrFacade.GetBranchCode(decryptedUser);
                var branch = await _branchRepository.GetBranchByCode(branchCode);
                 branches = await _branchRepository.GetBranchChildrenForBranchLevel(branch.Id);
                
            }
            else{
                 throw new FaultException("نام کابری نامعتبر است");
            }
               return Mapper.Map<IEnumerable<Branch>,IEnumerable<BranchDto>>(branches);
        }
    }
}