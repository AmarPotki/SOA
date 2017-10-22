using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.BankPerson;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Common.Extensions;
using RahyabServices.DataAccess.Repositories.BankPerson.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Bank{
    public class HrService : IHrService{
        private readonly IOrganizationUnitsRepository _organizationUnitsRepository;
        private readonly IPersonInfoRepository _personInfoRepository;
        public HrService(IPersonInfoRepository personInfoRepository,
            IOrganizationUnitsRepository organizationUnitsRepository){
            _personInfoRepository = personInfoRepository;
            _organizationUnitsRepository = organizationUnitsRepository;
        }
        public async Task<PersonInfoDto> GetUserInfo(GetUserInfoDtq getUserInfoDtq){
            if (getUserInfoDtq.UserName == "SHAREPOINT\\system")
                return new PersonInfoDto
                {
                    CodeMahalKhedmat = "0201",
                    MahalKhedmat = "rahyab",
                    ShomarehPersenely = 1,
                    FName = "system",
                    LName = "Account"
                };
            var user = await _personInfoRepository.GetPersonAbUser(getUserInfoDtq.UserName);
            var person = await _personInfoRepository.GetPersonInfo(int.Parse(user.PersonelId));
            return Mapper.Map<PersonInfo, PersonInfoDto>(person);
        }
        public async Task<PersonInfoDto> GetUserInfo(GetUserInfoByPersonnelNoDtq getUserInfoDtq)
        {
            if (getUserInfoDtq.UserName == "SHAREPOINT\\system")
                return new PersonInfoDto
                {
                    CodeMahalKhedmat = "0201",
                    MahalKhedmat = "rahyab",
                    ShomarehPersenely = 1,
                    FName = "system",
                    LName = "Account"
                };
            
            var person = await _personInfoRepository.GetPersonInfo(int.Parse(getUserInfoDtq.PersonnalNumber));
            return Mapper.Map<PersonInfo, PersonInfoDto>(person);
        }
        public async Task<BranchManagerDto> IsBranchManager(IsValidBranchManagerDtq isManager){
            var user = await _personInfoRepository.GetPersonAbUser(isManager.UserName);
            var person = await _personInfoRepository.GetPersonInfo(int.Parse(user.PersonelId));
            var userName = await _personInfoRepository.GetBranchManagerUserName(person.CodeMahalKhedmat);
            return isManager.UserName == userName
                ? new BranchManagerDto
                {
                    ShomarehPersenely = person.ShomarehPersenely,
                    CodeMahalKhedmat = person.CodeMahalKhedmat,
                    IsBranchManager = true
                }
                : new BranchManagerDto
                {
                    IsBranchManager = false
                };
        }
        public async Task<IEnumerable<WorkSectionDto>> GetWorkSections(){
            var worksections = await _personInfoRepository.GetWorkSections();
            return Mapper.Map<IEnumerable<WorkSection>, IEnumerable<WorkSectionDto>>(worksections);
        }
        public async Task<IEnumerable<BranchDto>> GetBranches(){
            var orgs =
                await
                    _organizationUnitsRepository.QueryAsync(
                        async f =>
                            await
                                f.Where(x => x.IsActive == "Y" && (x.UnitType == "B" ))
                                    .ToListAsync());
            //! || x.UnitType == "V" شعبه مجازی ها و ...
            return orgs.Select(x => new BranchDto {Code = x.Code, Name = x.Name.SafeFarsiStr()});
        }
    }
}