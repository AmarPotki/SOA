using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.BranchClaim;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class BranchClaimService : IBranchClaimService{
        private readonly IBranchClaimRepository _branchClaimRepository;
        public BranchClaimService(IBranchClaimRepository branchClaimRepository){
            _branchClaimRepository = branchClaimRepository;
        }
        public async Task<BranchClaimDto> GetBranchClaim(GetBranchClaimDto branchClaimDto){
            var branchClaims =
                await _branchClaimRepository.GetBranchClaim(branchClaimDto.BranchId, branchClaimDto.DateOnly.Date);
            return Mapper.Map<BranchClaim, BranchClaimDto>(branchClaims);
        }
        public async Task<IEnumerable<BranchClaimDto>> GetLastSevenDaysBranchClaims(
            GetLastSevenDaysBranchClaimsDto branchClaimsDto){
            var branchClaims =
                await
                    _branchClaimRepository.GetDaysBranchClaims(branchClaimsDto.BranchId, DateTime.Now.Date.AddDays(-7));
            return Mapper.Map<IEnumerable<BranchClaim>, IEnumerable<BranchClaimDto>>(branchClaims);
        }
    }
}