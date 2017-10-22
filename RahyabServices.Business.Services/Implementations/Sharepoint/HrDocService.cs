using System;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Logging;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Sharepoint{
    public class HrDocService : IHrDocService{
        private readonly IHrDocPermissionListRepository _docPermissionListRepository;
        private readonly ILogger _logger;
        private readonly IPersonelDocListRepository _personelDocListRepository;
        public HrDocService(IHrDocPermissionListRepository docPermissionListRepository,
            IPersonelDocListRepository personelDocListRepository, ILogger logger){
            _docPermissionListRepository = docPermissionListRepository;
            _personelDocListRepository = personelDocListRepository;
            _logger = logger;
        }
        public bool CheckPermissionStates(){
            var notStartedItems = _docPermissionListRepository.GetNotStartedItems();
            var mustBeDoneItems = _docPermissionListRepository.GetMustBeDoneItems();

            // set permission
            foreach (var item in notStartedItems){
                try{
                    _personelDocListRepository.SetItemPermission(item.Users.Values, item.PersonalNo);
                    item.State = 1;
                    _docPermissionListRepository.Update(item);
                }
                catch (Exception ex){
                    _logger.Error(new FaultDto
                    {
                        Location = "HrDocService",
                        Message = $"Item Id: {item.Id}  Document Code {item.PersonalNo} {ex.Message}"
                    });
                }
            }

            // reset permission
            foreach (var item in mustBeDoneItems){
                try{
                    item.State = 2;
                    _personelDocListRepository.ResetItemPermission(item.Users.Values, item.PersonalNo);
                    _docPermissionListRepository.Update(item);
                }
                catch (Exception ex){
                    _logger.Error(new FaultDto
                    {
                        Location = "HrDocService",
                        Message = $"Item Id: {item.Id}  Document Code {item.PersonalNo} {ex.Message}"
                    });
                }
            }
            return true;
        }
    }
}