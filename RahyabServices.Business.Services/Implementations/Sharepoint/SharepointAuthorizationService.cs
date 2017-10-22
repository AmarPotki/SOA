using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Sharepoint{
    public class SharepointAuthorizationService:ISharepointAuthorizationService{
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUserInformationRepository _userInformationRepository;
        public SharepointAuthorizationService(IAuthorizationRepository authorizationRepository, IUserInformationRepository userInformationRepository)
        {
            _authorizationRepository = authorizationRepository;
            _userInformationRepository = userInformationRepository;
        }

        public bool CheckUserInAdminGroup(string userName){
            return _authorizationRepository.IsExistInManagerGroup(userName);
        }
        public bool CheckUserInBranchLevelGroup(string userName)
        {
            return _authorizationRepository.IsExistInBranchLevel(userName);
        }
        public bool IsExistInBranchService(string userName){
            return _authorizationRepository.IsExistInBranchService(userName);
        }
        public bool IsValidUserName(string userName, string siteCollection)
        {
            return _userInformationRepository.IsValid(userName, siteCollection);
        }
    }
}