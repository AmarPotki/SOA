using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Common.Logging;
using RahyabServices.DataAccess.Repositories.BankPerson.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Bank{
    public class ActiveDirectoryService : IActiveDirectoryService{
        private readonly ILogger _logger;
        private readonly string _password = "u36804F@g65041A!r64058Y$";
        private readonly IPersonInfoRepository _personInfoRepository;
        private readonly string _userName = "rahyab.acopt";
        public ActiveDirectoryService(IPersonInfoRepository personInfoRepository, ILogger logger){
            _personInfoRepository = personInfoRepository;
            _logger = logger;
        }
        public async Task UpdateUsersThatMustBeAtive(){
            var bankUsers = await _personInfoRepository.GetAllActive();
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                var qbeUser = new UserPrincipal(pc) {Enabled = false};
                var srch = new PrincipalSearcher(qbeUser);
                var adUsers = srch.FindAll();
                var count = adUsers.Count();
                // Parallel.ForEach(adUsers, adUser =>{
                foreach (var adUser in adUsers){
                    var ad = adUser as UserPrincipal;
                    int number;
                    bool result;
                    if (string.IsNullOrEmpty(ad?.EmployeeId)){
                        if (string.IsNullOrEmpty(ad?.Description)) { continue; }
                        result = int.TryParse(ad.Description, out number);
                    }
                    else{
                        result = int.TryParse(ad.EmployeeId, out number);
                    }
                    // if (string.IsNullOrEmpty(ad?.EmployeeId)) continue;//return;
                    if (!result) continue; //return;
                    var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == number);
                    // var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == int.Parse(ad.EmployeeId));
                    if (user == null) continue; // return;
                    try{
                        ad.Enabled = true;
                        ad.EmployeeId = number.ToString();
                        ad.Save();
                    }
                    catch (Exception e){
                        _logger.Error(new FaultDto
                        {
                            Location = "ActiveDirectoryService",
                            Message = $"{ad.SamAccountName} {user.CodeMeli} {user.Semat} {user.ShomarehPersenely} " +
                                      $"{user.WorkSectionTitle} {user.CodeMahalKhedmat} {user.WorkSectionId} {e.Message}"
                        });
                    }
                }
                //});
            }
        }
        public async Task UpdateActiveDirectoryUsers(){
            var bankUsers = await _personInfoRepository.GetAllActive();
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                var qbeUser = new UserPrincipal(pc) {Enabled = true};
                var srch = new PrincipalSearcher(qbeUser);
                var adUsers = srch.FindAll();
                //  var count = adUsers.Count();
                // Parallel.ForEach(adUsers, adUser =>{
                foreach (var adUser in adUsers){
                    var ad = adUser as UserPrincipal;
                    if (string.IsNullOrEmpty(ad?.EmployeeId)) continue; //return;
                    // if (string.IsNullOrEmpty(ad?.EmployeeId)) continue;//return;
                    int number;
                    var result = int.TryParse(ad.EmployeeId, out number);
                    if (!result) continue; //return;
                    var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == number);
                    // var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == int.Parse(ad.EmployeeId));
                    if (user == null) continue; // return;
                    var directE = ad.GetUnderlyingObject() as DirectoryEntry;
                    if (directE == null) continue; //return;
                    //employeeID employeeNumber employeeType
                    try{
                        if (user.EmploymentStatusId?.ToString() == "8") continue;
                        directE.Properties["title"].Clear();
                        directE.Properties["title"].Add(user.Semat);
                        if (user.EmploymentStatusId != null){
                            directE.Properties["employeeType"].Clear();
                            directE.Properties["employeeType"].Add(user.EmploymentStatusId.Value.ToString());
                        }
                        directE.Properties["employeeID"].Clear();
                        directE.Properties["employeeID"].Add(user.ShomarehPersenely.ToString());
                        directE.Properties["employeeNumber"].Clear();
                        directE.Properties["employeeNumber"].Add(user.CodeMeli);
                        directE.Properties["department"].Clear();
                        directE.Properties["department"].Add(user.WorkSectionTitle);
                        directE.Properties["otherLoginWorkstations"].Clear();
                        directE.Properties["otherLoginWorkstations"].Add(user.CodeMahalKhedmat.Length == 3
                            ? "0" + user.CodeMahalKhedmat
                            : user.CodeMahalKhedmat);
                        if (user.WorkSectionId != null){
                            directE.Properties["postOfficeBox"].Clear();
                            directE.Properties["postOfficeBox"].Add(user.WorkSectionId.ToString());
                        }
                        directE.CommitChanges();
                    }
                    catch (Exception e){
                        _logger.Error(new FaultDto
                        {
                            Location = "ActiveDirectoryService",
                            Message = $"{ad.SamAccountName} {user.CodeMeli} {user.Semat} {user.ShomarehPersenely} " +
                                      $"{user.WorkSectionTitle} {user.CodeMahalKhedmat} {user.WorkSectionId} {e.Message}"
                        });
                    }
                }
                //});
            }
        }
        public async Task UpdateDeActiveUsers(){
            var bankUsers = await _personInfoRepository.GetAllDeActive();
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                var qbeUser = new UserPrincipal(pc) {Enabled = true};
                var srch = new PrincipalSearcher(qbeUser);
                var adUsers = srch.FindAll();
                //  var count = adUsers.Count();
                // Parallel.ForEach(adUsers, adUser =>{

                // Create a file to write to.
                foreach (var adUser in adUsers){
                    var ad = adUser as UserPrincipal;
                    if (string.IsNullOrEmpty(ad?.EmployeeId)) continue; //return;
                    // if (string.IsNullOrEmpty(ad?.EmployeeId)) continue;//return;
                    int number;
                    var result = int.TryParse(ad.EmployeeId, out number);
                    if (!result) continue; //return;
                    var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == number);
                    // var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == int.Parse(ad.EmployeeId));
                    if (user == null) continue; // return;
                    try{
                        _logger.Info(new FaultDto
                        {
                            Location = "ActiveDirectoryService",
                            Message = $"disable User : {ad.Name} {ad.EmployeeId}"
                        });
                        ad.Enabled = false;
                        ad.Save();
                    }
                    catch (Exception e){
                        _logger.Error(new FaultDto
                        {
                            Location = "ActiveDirectoryService",
                            Message = $"{ad.SamAccountName} {user.CodeMeli} {user.Semat} {user.ShomarehPersenely} " +
                                      $"{user.WorkSectionTitle} {user.CodeMahalKhedmat} {user.WorkSectionId} {e.Message}"
                        });
                    }
                }
                //});
            }
        }
        public async Task UpdateActiveDirectoryGroups(){
            var bankUsers = await _personInfoRepository.GetAllActive();
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                var qbeUser = new UserPrincipal(pc) {Enabled = true};
                var srch = new PrincipalSearcher(qbeUser);
                var adUsers = srch.FindAll();
                var resourseManagementCodes = new[]
                {
                    "00105", "0105", "102", "103", "104", "105", "106",
                    "107", "108", "109", "110", "111", "112"
                };
                //  var count = adUsers.Count();
                // Parallel.ForEach(adUsers, adUser =>{
                foreach (var adUser in adUsers){
                    var ad = adUser as UserPrincipal;
                    if (string.IsNullOrEmpty(ad?.EmployeeId)) continue; //return;
                    // if (string.IsNullOrEmpty(ad?.EmployeeId)) continue;//return;
                    int number;
                    var result = int.TryParse(ad.EmployeeId, out number);
                    if (!result) continue; //return;
                    var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == number);
                    // var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == int.Parse(ad.EmployeeId));
                    if (user == null) continue; // return;
                    var directE = ad.GetUnderlyingObject() as DirectoryEntry;
                    if (directE == null) continue; //return;
                    try{
                        //check Setad shoab
                        var codeMahaleKhedmat = directE.Properties["otherLoginWorkstations"][0].ToString();
                        var workSectionId = directE.Properties["postOfficeBox"][0].ToString();
                        var wE = new[] {"2", "3"};
                        //modiriat gharb ya shargh bode rafte setad
                        if (wE.Contains(codeMahaleKhedmat) && user.CodeMahalKhedmat == "12"){
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            RemoveUserFromGroup(pc, ad, groupName);
                            //ezafe kardan be goup setadi
                            AddUserToGroup(pc, ad, "OfficeGroup");
                            //ezafe kardan be group setadi marboote
                            //do here
                            AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                            //ezafe kardan be group modiran setad agar modir bashad
                            var userName = await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                        } //modiriat gharb ya shargh bode rafte shoab
                        else if (wE.Contains(codeMahaleKhedmat) && user.CodeMahalKhedmat != "12" &&
                                 !resourseManagementCodes.Contains(user.CodeMahalKhedmat) &&
                                 codeMahaleKhedmat != user.CodeMahalKhedmat){
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            RemoveUserFromGroup(pc, ad, groupName);
                            //ezafe kardan be group shoab
                            AddUserToGroup(pc, ad, "BranchGroup");
                            //ezafe kardan be goup shobe morede nazar
                            //name group = b + code shobe
                            AddUserToGroup(pc, ad, "g" + (user.CodeMahalKhedmat.Length == 3
                                ? "0" + user.CodeMahalKhedmat
                                : user.CodeMahalKhedmat));
                            //check user is Branch Manager
                            var userName = await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                        } //modiriat gharb ya shargh bode rafte modiriat manabe
                        else if (wE.Contains(codeMahaleKhedmat) &&
                                 resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            RemoveUserFromGroup(pc, ad, groupName);
                            //ezafe kardan be goup modiriat manabe
                            AddUserToGroup(pc, ad, "ResourceManagementGroup");
                            //ezafe kardan be group modiriat manabe marboote
                            // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                            AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                            //ezafe kardan be group modiran modiriat manabe agar modir bashad
                            var userName =
                                await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                        }
                        //shargh o ghab be shargh o gharb
                        else if (wE.Contains(codeMahaleKhedmat) && wE.Contains(user.CodeMahalKhedmat)){
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            RemoveUserFromGroup(pc, ad, groupName);
                            groupName = user.CodeMahalKhedmat == "2" ? "WestBranches" : "EastBranches";
                            AddUserToGroup(pc, ad, groupName);
                            RemoveUserFromGroup(pc, ad, "WS" + workSectionId);
                            AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                        }
                        //ghablan setad boode hala rafte shobe
                        else if (codeMahaleKhedmat == "12" && user.CodeMahalKhedmat != "12" &&
                                 !resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                            //hazf az group setadi
                            RemoveUserFromGroup(pc, ad, "OfficeGroup");

                            //hazf agar raise setad boode
                            RemoveUserFromGroup(pc, ad, "OfficeManagers");
                            //bayad setad ghabli ro peida karde va hazf konad
                            // this code here
                            RemoveUserFromGroup(pc, ad, "WS" + workSectionId);

                            //ezafe kardan be group shoab
                            AddUserToGroup(pc, ad, "BranchGroup");
                            //ezafe kardan be goup shobe morede nazar
                            //name group = b + code shobe
                            AddUserToGroup(pc, ad, "g" + (user.CodeMahalKhedmat.Length == 3
                                ? "0" + user.CodeMahalKhedmat
                                : user.CodeMahalKhedmat));
                            //check user is Branch Manager
                            var userName =
                                await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                        } //ghablan setad boode hala rafte modiriat gharb ya shargh 
                        else if (codeMahaleKhedmat == "12" && wE.Contains(codeMahaleKhedmat)){
                            //hazf az group setadi
                            RemoveUserFromGroup(pc, ad, "OfficeGroup");

                            //hazf agar raise setad boode
                            RemoveUserFromGroup(pc, ad, "OfficeManagers");
                            //bayad setad ghabli ro peida karde va hazf konad
                            // this code here
                            RemoveUserFromGroup(pc, ad, "WS" + workSectionId);
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            AddUserToGroup(pc, ad, groupName);
                        } //ghablan shobe boode hala rafte setad
                        else if (codeMahaleKhedmat != "12" && !resourseManagementCodes.Contains(user.CodeMahalKhedmat) &&
                                 user.CodeMahalKhedmat == "12"){
                            // hazf az group shoab
                            RemoveUserFromGroup(pc, ad, "BranchGroup");
                            //hazf az group modiran shoab
                            RemoveUserFromGroup(pc, ad, "BranchManagers");

                            //hazf az group shobe marboote 
                            //name group = b + code shobe
                            RemoveUserFromGroup(pc, ad, "g" + (user.CodeMahalKhedmat.Length == 3
                                ? "0" + user.CodeMahalKhedmat
                                : user.CodeMahalKhedmat));

                            //ezafe kardan be goup setadi
                            AddUserToGroup(pc, ad, "OfficeGroup");
                            //ezafe kardan be group setadi marboote
                            //do here
                            AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                            //ezafe kardan be group modiran setad agar modir bashad
                            var userName =
                                await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                        } //ghablan shobe boode hala rafte modiriat shargh ya gharb
                        else if (codeMahaleKhedmat != "12" &&
                                 !resourseManagementCodes.Contains(user.CodeMahalKhedmat) &&
                                 wE.Contains(codeMahaleKhedmat)){
                            // hazf az group shoab
                            RemoveUserFromGroup(pc, ad, "BranchGroup");
                            //hazf az group modiran shoab
                            RemoveUserFromGroup(pc, ad, "BranchManagers");

                            //hazf az group shobe marboote 
                            //name group = b + code shobe
                            RemoveUserFromGroup(pc, ad, "g" + (user.CodeMahalKhedmat.Length == 3
                                ? "0" + user.CodeMahalKhedmat
                                : user.CodeMahalKhedmat));
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            AddUserToGroup(pc, ad, groupName);
                        }
                        //az setadi be setad dg rafte
                        else if (codeMahaleKhedmat == user.CodeMahalKhedmat && codeMahaleKhedmat == "12" &&
                                 workSectionId != user.WorkSectionId.ToString()){
                            //bayad setad ghabli ro peida karde va hazf konad
                            // this code here
                            AddUserToGroup(pc, ad, "WS" + workSectionId);
                            //ezafe kardan be group setadi marboote
                            //do here
                            AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                            //check konim bebinim hanooz rais hast ya na
                            var userName =
                                await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                            else RemoveUserFromGroup(pc, ad, "OfficeManagers");
                        }
                        //az shobe be shobe dg
                        else if (codeMahaleKhedmat != user.CodeMahalKhedmat && user.CodeMahalKhedmat != "12" &&
                                 workSectionId != user.WorkSectionId.ToString()){
                            //hazf az group shobe marboote 
                            //name group = b + code shobe
                            // codemahale khedmat dar active 4 raghami hast
                            RemoveUserFromGroup(pc, ad, "g" + codeMahaleKhedmat);
                            // ezafe kardan be group marboote jadid
                            AddUserToGroup(pc, ad, "g" + (user.CodeMahalKhedmat.Length == 3
                                ? "0" + user.CodeMahalKhedmat
                                : user.CodeMahalKhedmat));

                            //check konim bebinim hanooz rais hast ya na
                            var userName =
                                await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                            else RemoveUserFromGroup(pc, ad, "BranchManagers");
                        } //setad boode rafte modiriat manabe
                        else if (codeMahaleKhedmat == "12" &&
                                 resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                            //hazf az group setadi
                            RemoveUserFromGroup(pc, ad, "OfficeGroup");

                            //hazf agar raise setad boode
                            RemoveUserFromGroup(pc, ad, "OfficeManagers");
                            //bayad setad ghabli ro peida karde va hazf konad
                            // this code here
                            RemoveUserFromGroup(pc, ad, "WS" + workSectionId);

                            //ezafe kardan be goup modiriat manabe
                            AddUserToGroup(pc, ad, "ResourceManagementGroup");
                            //ezafe kardan be group modiriat manabe marboote
                            // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                            AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                            //ezafe kardan be group modiran modiriat manabe agar modir bashad
                            var userName =
                                await
                                    _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                        }
                        //shobe boode rafte modiriat manabe
                        else if (codeMahaleKhedmat != "12" && codeMahaleKhedmat != "2" &&
                                 codeMahaleKhedmat != "3"
                                 && resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                            // hazf az group shoab
                            RemoveUserFromGroup(pc, ad, "BranchGroup");
                            //hazf az group modiran shoab
                            RemoveUserFromGroup(pc, ad, "BranchManagers");

                            //hazf az group shobe marboote 
                            //name group = b + code shobe
                            RemoveUserFromGroup(pc, ad, "g" + (user.CodeMahalKhedmat.Length == 3
                                ? "0" + user.CodeMahalKhedmat
                                : user.CodeMahalKhedmat));

                            //ezafe kardan be goup modiriat manabe
                            AddUserToGroup(pc, ad, "ResourceManagementGroup");
                            //ezafe kardan be group modiriat manabe marboote
                            // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                            AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                            //ezafe kardan be group modiran modiriat manabe agar modir bashad
                            var userName =
                                await
                                    _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                        } //modiriat manabe be modiriat manabe
                        else if (resourseManagementCodes.Contains(codeMahaleKhedmat) &&
                                 resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                            RemoveUserFromGroup(pc, ad, "rm" + codeMahaleKhedmat);
                            AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);
                            var userName =
                                await
                                    _personInfoRepository.GetManagerUserName(
                                        user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                            else RemoveUserFromGroup(pc, ad, "ResourceManagementManagers");
                        }
                        //modiriat manabe be shargh gharb
                        else if (resourseManagementCodes.Contains(codeMahaleKhedmat) &&
                                 wE.Contains(user.CodeMahalKhedmat)){
                            RemoveUserFromGroup(pc, ad, "rm" + codeMahaleKhedmat);
                            RemoveUserFromGroup(pc, ad, "ResourceManagementManagers");
                            var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                            AddUserToGroup(pc, ad, groupName);
                        }
                        // kar bar bedoun group hast
                        else{
                            if (resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                                //ezafe kardan be goup modiriat manabe
                                AddUserToGroup(pc, ad, "ResourceManagementGroup");
                                //ezafe kardan be group modiriat manabe marboote
                                // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                                AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                                //ezafe kardan be group modiran modiriat manabe agar modir bashad
                                var userName =
                                    await
                                        _personInfoRepository.GetManagerUserName(
                                            user.WorkSectionId.ToString());
                                if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                            }
                            else if (codeMahaleKhedmat == "3") {
                                AddUserToGroup(pc, ad, "Eastbranches");
                            }
                            else if (codeMahaleKhedmat == "2") {
                                AddUserToGroup(pc, ad, "WestBranches");
                            }
                            //shoab
                            else if (user.CodeMahalKhedmat.Length < 5 &&
                                     codeMahaleKhedmat != "12"){
                                //edare amaliat
                                if (user.CodeMahalKhedmat == "4444") continue;
                                if (user.MahalKhedmat.Contains("ارتباط فردا")) continue;
                                //ezafe kardan be goup shobe morede nazar
                                //name group = b + code shobe
                                AddUserToGroup(pc, ad,
                                    "g" + (user.CodeMahalKhedmat.Length == 3
                                        ? "0" + user.CodeMahalKhedmat
                                        : user.CodeMahalKhedmat));

                                //ezafe kardan be group shoab
                                AddUserToGroup(pc, ad, "BranchGroup");

                                //check user is Branch Manager
                                var userName =
                                    await
                                        _personInfoRepository.GetManagerUserName(
                                            user.WorkSectionId.ToString());
                                if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                            }
                            //setad
                            else if (user.CodeMahalKhedmat.Length < 5 &&
                                     codeMahaleKhedmat == "12"){
                                //ezafe kardan be goup setadi
                                AddUserToGroup(pc, ad, "OfficeGroup");
                                //ezafe kardan be group setadi marboote
                                //do here
                                AddUserToGroup(pc, ad, "WS" + workSectionId);
                                //ezafe kardan be group modiran setad agar modir bashad
                                var userName =
                                    await
                                        _personInfoRepository.GetManagerUserName(
                                            user.WorkSectionId.ToString());
                                if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                            }
                            else{
                                _logger.Warn(new FaultDto
                                {
                                    Location = "ActiveDirectoryService UpdateGroup",
                                    Message =
                                        $"{ad.SamAccountName}  {user.ShomarehPersenely} " +
                                        $"{user.CodeMahalKhedmat} {user.WorkSectionId} UpdateGroup Error"
                                });
                            }
                        }
                    }
                    catch (Exception e){
                        _logger.Error(new FaultDto
                        {
                            Location = "ActiveDirectoryService UpdateGroup",
                            Message = $"{ad.SamAccountName}  {user.ShomarehPersenely} " +
                                      $"{user.CodeMahalKhedmat} {user.WorkSectionId} {e.Message}"
                        });
                    }
                }
            }
        }
        //update a user
        public async Task UpdateActiveUser(string userName){
            var bankUsers = await _personInfoRepository.GetAllActive();
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                var qbeUser = new UserPrincipal(pc) {SamAccountName = userName};
                var srch = new PrincipalSearcher(qbeUser);
                var adUser = srch.FindOne();
                //  var count = adUsers.Count();
                // Parallel.ForEach(adUsers, adUser =>{
                var ad = adUser as UserPrincipal;
                if (string.IsNullOrEmpty(ad?.EmployeeId)) return; //return;
                // if (string.IsNullOrEmpty(ad?.EmployeeId)) continue;//return;
                int number;
                var result = int.TryParse(ad.EmployeeId, out number);
                if (!result) return; //return;
                var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == number);
                // var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == int.Parse(ad.EmployeeId));
                if (user == null) return; // return;
                var directE = ad.GetUnderlyingObject() as DirectoryEntry;
                if (directE == null) return; //return;
                //employeeID employeeNumber employeeType
                try{
                    if (user.EmploymentStatusId?.ToString() == "8") return;
                    directE.Properties["title"].Clear();
                    directE.Properties["title"].Add(user.Semat);
                    if (user.EmploymentStatusId != null){
                        directE.Properties["employeeType"].Clear();
                        directE.Properties["employeeType"].Add(user.EmploymentStatusId.Value.ToString());
                    }
                    directE.Properties["employeeID"].Clear();
                    directE.Properties["employeeID"].Add(user.ShomarehPersenely.ToString());
                    directE.Properties["employeeNumber"].Clear();
                    directE.Properties["employeeNumber"].Add(user.CodeMeli);
                    directE.Properties["department"].Clear();
                    directE.Properties["department"].Add(user.WorkSectionTitle);
                    directE.Properties["otherLoginWorkstations"].Clear();
                    directE.Properties["otherLoginWorkstations"].Add(user.CodeMahalKhedmat.Length == 3
                        ? "0" + user.CodeMahalKhedmat
                        : user.CodeMahalKhedmat);
                    if (user.WorkSectionId != null){
                        directE.Properties["postOfficeBox"].Clear();
                        directE.Properties["postOfficeBox"].Add(user.WorkSectionId.ToString());
                    }
                    directE.CommitChanges();
                }
                catch (Exception e){
                    _logger.Error(new FaultDto
                    {
                        Location = "ActiveDirectoryService",
                        Message = $"{ad.SamAccountName} {user.CodeMeli} {user.Semat} {user.ShomarehPersenely} " +
                                  $"{user.WorkSectionTitle} {user.CodeMahalKhedmat} {user.WorkSectionId} {e.Message}"
                    });
                }
            }
        }
        public async Task UpdateActiveDirectoryGroup(string adUserName){
            var bankUsers = await _personInfoRepository.GetAllActive();
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                var qbeUser = new UserPrincipal(pc) {SamAccountName = adUserName};
                var srch = new PrincipalSearcher(qbeUser);
                var adUser = srch.FindOne();
                var resourseManagementCodes = new[]
                {
                    "00105", "0105", "102", "103", "104", "105", "106",
                    "107", "108", "109", "110", "111", "112"
                };
                var ad = adUser as UserPrincipal;
                if (string.IsNullOrEmpty(ad?.EmployeeId)) return;
                // if (string.IsNullOrEmpty(ad?.EmployeeId)) continue;//return;
                int number;
                var result = int.TryParse(ad.EmployeeId, out number);
                if (!result) return;
                var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == number);
                // var user = bankUsers.FirstOrDefault(x => x.ShomarehPersenely == int.Parse(ad.EmployeeId));
                if (user == null) return;
                var directE = ad.GetUnderlyingObject() as DirectoryEntry;
                if (directE == null) return;
                try{
                    //check Setad shoab
                    var codeMahaleKhedmat = directE.Properties["otherLoginWorkstations"][0].ToString();
                    var workSectionId = directE.Properties["postOfficeBox"][0].ToString();
                    var hrDataCodeMahalKhedmat = user.CodeMahalKhedmat.Length == 3
                        ? "0" + user.CodeMahalKhedmat
                        : user.CodeMahalKhedmat;
                    var wE = new[] {"2", "3"};
                    //modiriat gharb ya shargh bode rafte setad
                    if (wE.Contains(codeMahaleKhedmat) && user.CodeMahalKhedmat == "12"){
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        RemoveUserFromGroup(pc, ad, groupName);
                        //ezafe kardan be goup setadi
                        AddUserToGroup(pc, ad, "OfficeGroup");
                        //ezafe kardan be group setadi marboote
                        //do here
                        AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                        //ezafe kardan be group modiran setad agar modir bashad
                        var userName = await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                    } //modiriat gharb ya shargh bode rafte shoab
                    else if (wE.Contains(codeMahaleKhedmat) && user.CodeMahalKhedmat != "12" &&
                             !resourseManagementCodes.Contains(user.CodeMahalKhedmat) &&
                             codeMahaleKhedmat != hrDataCodeMahalKhedmat){
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        RemoveUserFromGroup(pc, ad, groupName);
                        //ezafe kardan be group shoab
                        AddUserToGroup(pc, ad, "BranchGroup");
                        //ezafe kardan be goup shobe morede nazar
                        //name group = b + code shobe
                        AddUserToGroup(pc, ad, "g" + hrDataCodeMahalKhedmat);
                        //check user is Branch Manager
                        var userName = await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                    } //modiriat gharb ya shargh bode rafte modiriat manabe
                    else if (wE.Contains(codeMahaleKhedmat) &&
                             resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        RemoveUserFromGroup(pc, ad, groupName);
                        //ezafe kardan be goup modiriat manabe
                        AddUserToGroup(pc, ad, "ResourceManagementGroup");
                        //ezafe kardan be group modiriat manabe marboote
                        // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                        AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                        //ezafe kardan be group modiran modiriat manabe agar modir bashad
                        var userName =
                            await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                    }
                    //shargh o ghab be shargh o gharb
                    else if (wE.Contains(codeMahaleKhedmat) && wE.Contains(user.CodeMahalKhedmat)){
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        RemoveUserFromGroup(pc, ad, groupName);
                        groupName = user.CodeMahalKhedmat == "2" ? "WestBranches" : "EastBranches";
                        AddUserToGroup(pc, ad, groupName);
                        RemoveUserFromGroup(pc, ad, "WS" + workSectionId);
                        AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                    }
                    //ghablan setad boode hala rafte shobe
                    else if (codeMahaleKhedmat == "12" && user.CodeMahalKhedmat != "12" &&
                             !resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                        //hazf az group setadi
                        RemoveUserFromGroup(pc, ad, "OfficeGroup");

                        //hazf agar raise setad boode
                        RemoveUserFromGroup(pc, ad, "OfficeManagers");
                        //bayad setad ghabli ro peida karde va hazf konad
                        // this code here
                        RemoveUserFromGroup(pc, ad, "WS" + workSectionId);

                        //ezafe kardan be group shoab
                        AddUserToGroup(pc, ad, "BranchGroup");
                        //ezafe kardan be goup shobe morede nazar
                        //name group = b + code shobe
                        AddUserToGroup(pc, ad, "g" + hrDataCodeMahalKhedmat);
                        //check user is Branch Manager
                        var userName =
                            await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                    } //ghablan setad boode hala rafte modiriat gharb ya shargh 
                    else if (codeMahaleKhedmat == "12" && wE.Contains(codeMahaleKhedmat)){
                        //hazf az group setadi
                        RemoveUserFromGroup(pc, ad, "OfficeGroup");

                        //hazf agar raise setad boode
                        RemoveUserFromGroup(pc, ad, "OfficeManagers");
                        //bayad setad ghabli ro peida karde va hazf konad
                        // this code here
                        RemoveUserFromGroup(pc, ad, "WS" + workSectionId);
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        AddUserToGroup(pc, ad, groupName);
                    } //ghablan shobe boode hala rafte setad
                    else if (codeMahaleKhedmat != "12" && !resourseManagementCodes.Contains(user.CodeMahalKhedmat) &&
                             user.CodeMahalKhedmat == "12"){
                        // hazf az group shoab
                        RemoveUserFromGroup(pc, ad, "BranchGroup");
                        //hazf az group modiran shoab
                        RemoveUserFromGroup(pc, ad, "BranchManagers");

                        //hazf az group shobe marboote 
                        //name group = b + code shobe
                        RemoveUserFromGroup(pc, ad, "g" + hrDataCodeMahalKhedmat);

                        //ezafe kardan be goup setadi
                        AddUserToGroup(pc, ad, "OfficeGroup");
                        //ezafe kardan be group setadi marboote
                        //do here
                        AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                        //ezafe kardan be group modiran setad agar modir bashad
                        var userName =
                            await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                    } //ghablan shobe boode hala rafte modiriat shargh ya gharb
                    else if (codeMahaleKhedmat != "12" &&
                             !resourseManagementCodes.Contains(user.CodeMahalKhedmat) &&
                             wE.Contains(codeMahaleKhedmat)){
                        // hazf az group shoab
                        RemoveUserFromGroup(pc, ad, "BranchGroup");
                        //hazf az group modiran shoab
                        RemoveUserFromGroup(pc, ad, "BranchManagers");

                        //hazf az group shobe marboote 
                        //name group = b + code shobe
                        RemoveUserFromGroup(pc, ad, "g" + hrDataCodeMahalKhedmat);
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        AddUserToGroup(pc, ad, groupName);
                    }
                    //az setadi be setad dg rafte
                    else if (codeMahaleKhedmat == hrDataCodeMahalKhedmat && codeMahaleKhedmat == "12" &&
                             workSectionId != user.WorkSectionId.ToString()){
                        //bayad setad ghabli ro peida karde va hazf konad
                        // this code here
                        AddUserToGroup(pc, ad, "WS" + workSectionId);
                        //ezafe kardan be group setadi marboote
                        //do here
                        AddUserToGroup(pc, ad, "WS" + user.WorkSectionId);
                        //check konim bebinim hanooz rais hast ya na
                        var userName =
                            await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                        else RemoveUserFromGroup(pc, ad, "OfficeManagers");
                    }
                    //az shobe be shobe dg
                    else if (codeMahaleKhedmat != hrDataCodeMahalKhedmat && user.CodeMahalKhedmat != "12" &&
                             workSectionId != user.WorkSectionId.ToString()){
                        //hazf az group shobe marboote 
                        //name group = b + code shobe
                        // codemahale khedmat dar active 4 raghami hast
                        RemoveUserFromGroup(pc, ad, "g" + codeMahaleKhedmat);
                        // ezafe kardan be group marboote jadid
                        AddUserToGroup(pc, ad, "g" + hrDataCodeMahalKhedmat);

                        //check konim bebinim hanooz rais hast ya na
                        var userName =
                            await _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                        else RemoveUserFromGroup(pc, ad, "BranchManagers");
                    } //setad boode rafte modiriat manabe
                    else if (codeMahaleKhedmat == "12" &&
                             resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                        //hazf az group setadi
                        RemoveUserFromGroup(pc, ad, "OfficeGroup");

                        //hazf agar raise setad boode
                        RemoveUserFromGroup(pc, ad, "OfficeManagers");
                        //bayad setad ghabli ro peida karde va hazf konad
                        // this code here
                        RemoveUserFromGroup(pc, ad, "WS" + workSectionId);

                        //ezafe kardan be goup modiriat manabe
                        AddUserToGroup(pc, ad, "ResourceManagementGroup");
                        //ezafe kardan be group modiriat manabe marboote
                        // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                        AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                        //ezafe kardan be group modiran modiriat manabe agar modir bashad
                        var userName =
                            await
                                _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                    }
                    //shobe boode rafte modiriat manabe
                    else if (codeMahaleKhedmat != "12" && codeMahaleKhedmat != "2" &&
                             codeMahaleKhedmat != "3"
                             && resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                        // hazf az group shoab
                        RemoveUserFromGroup(pc, ad, "BranchGroup");
                        //hazf az group modiran shoab
                        RemoveUserFromGroup(pc, ad, "BranchManagers");

                        //hazf az group shobe marboote 
                        //name group = b + code shobe
                        RemoveUserFromGroup(pc, ad, "g" + hrDataCodeMahalKhedmat);

                        //ezafe kardan be goup modiriat manabe
                        AddUserToGroup(pc, ad, "ResourceManagementGroup");
                        //ezafe kardan be group modiriat manabe marboote
                        // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                        AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                        //ezafe kardan be group modiran modiriat manabe agar modir bashad
                        var userName =
                            await
                                _personInfoRepository.GetManagerUserName(user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                    } //modiriat manabe be modiriat manabe
                    else if (resourseManagementCodes.Contains(codeMahaleKhedmat) &&
                             resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                        RemoveUserFromGroup(pc, ad, "rm" + codeMahaleKhedmat);
                        AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);
                        var userName =
                            await
                                _personInfoRepository.GetManagerUserName(
                                    user.WorkSectionId.ToString());
                        if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                        else RemoveUserFromGroup(pc, ad, "ResourceManagementManagers");
                    }
                    //modiriat manabe be shargh gharb
                    else if (resourseManagementCodes.Contains(codeMahaleKhedmat) &&
                             wE.Contains(user.CodeMahalKhedmat)){
                        RemoveUserFromGroup(pc, ad, "rm" + codeMahaleKhedmat);
                        RemoveUserFromGroup(pc, ad, "ResourceManagementManagers");
                        var groupName = codeMahaleKhedmat == "2" ? "WestBranches" : "EastBranches";
                        AddUserToGroup(pc, ad, groupName);
                    }
                    // kar bar bedoun group hast
                    else{
                        if (resourseManagementCodes.Contains(user.CodeMahalKhedmat)){
                            //ezafe kardan be goup modiriat manabe
                            AddUserToGroup(pc, ad, "ResourceManagementGroup");
                            //ezafe kardan be group modiriat manabe marboote
                            // code haye modirat manabe 3 raghami va 4 raghami ya 5 raghami hastan
                            AddUserToGroup(pc, ad, "rm" + user.CodeMahalKhedmat);

                            //ezafe kardan be group modiran modiriat manabe agar modir bashad
                            var userName =
                                await
                                    _personInfoRepository.GetManagerUserName(
                                        user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "ResourceManagementManagers");
                        }
                        else if (codeMahaleKhedmat == "3") {
                            AddUserToGroup(pc, ad, "Eastbranches");
                        }
                        else if (codeMahaleKhedmat == "2") {
                            AddUserToGroup(pc, ad, "WestBranches");
                        }
                        //shoab
                        else if (user.CodeMahalKhedmat.Length < 5 &&
                                 codeMahaleKhedmat != "12"){
                            //edare amaliat
                            if (user.CodeMahalKhedmat == "4444") return;
                            if (user.MahalKhedmat.Contains("ارتباط فردا")) return;
                            //ezafe kardan be goup shobe morede nazar
                            //name group = b + code shobe
                            AddUserToGroup(pc, ad,
                                "g" + hrDataCodeMahalKhedmat);

                            //ezafe kardan be group shoab
                            AddUserToGroup(pc, ad, "BranchGroup");

                            //check user is Branch Manager
                            var userName =
                                await
                                    _personInfoRepository.GetManagerUserName(
                                        user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "BranchManagers");
                        }
                        //setad
                        else if (user.CodeMahalKhedmat.Length < 5 &&
                                 codeMahaleKhedmat == "12"){
                            //ezafe kardan be goup setadi
                            AddUserToGroup(pc, ad, "OfficeGroup");
                            //ezafe kardan be group setadi marboote
                            //do here
                            AddUserToGroup(pc, ad, "WS" + workSectionId);
                            //ezafe kardan be group modiran setad agar modir bashad
                            var userName =
                                await
                                    _personInfoRepository.GetManagerUserName(
                                        user.WorkSectionId.ToString());
                            if (userName == ad.SamAccountName) AddUserToGroup(pc, ad, "OfficeManagers");
                        }
                        else{
                            _logger.Warn(new FaultDto
                            {
                                Location = "ActiveDirectoryService UpdateGroup",
                                Message =
                                    $"{ad.SamAccountName}  {user.ShomarehPersenely} " +
                                    $"{user.CodeMahalKhedmat} {user.WorkSectionId} UpdateGroup Error"
                            });
                        }
                    }
                }
                catch (Exception e){
                    _logger.Error(new FaultDto
                    {
                        Location = "ActiveDirectoryService UpdateGroup",
                        Message = $"{ad.SamAccountName}  {user.ShomarehPersenely} " +
                                  $"{user.CodeMahalKhedmat} {user.WorkSectionId} {e.Message}"
                    });
                }
            }
        }
        public async Task CreateOrUpdateGroupName(){
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", "OU=CEO,OU=BA24,DC=Ab,DC=net",
                ContextOptions.Negotiate, _userName, _password)){
                //UpdateGroup(pc, "OfficeGroup", "اعضای ستاد");
                //UpdateGroup(pc, "OfficeManagers", "مدیران ستاد");
                //UpdateGroup(pc, "BranchGroup", "اعضای شعب");
                //UpdateGroup(pc, "BranchManagers", "مدیران شعب");
                //UpdateGroup(pc, "ResourceManagementGroup", "اعضای مدیریت منابع");
                //UpdateGroup(pc, "ResourceManagementManagers", "مدیران مدیریت منابع");
                //UpdateGroup(pc, "Eastbranches", "اداره امور شعب شرق تهران بزرگ");
                //UpdateGroup(pc, "Westbranches", "اداره امور شعب غرب تهران بزرگ");

                //   var bankUsers = await _personInfoRepository.GetAllActive();
                //shoab          
                //var qbeUser = new UserPrincipal(pc) {Enabled = true};
                //var srch = new PrincipalSearcher(qbeUser);
                //var adUsers = srch.FindAll();
                //var findAllGroups = new GroupPrincipal(pc, "g*");
                //var ps = new PrincipalSearcher(findAllGroups);
                //foreach (var group in ps.FindAll()){
                //    var groupName = group.Name.ToLower().Replace("g", "");
                //    groupName = groupName.StartsWith("0") ? groupName.Remove(0,1) : groupName;
                //    var user = bankUsers.FirstOrDefault(x => x.CodeMahalKhedmat == groupName);
                //    if(user==null)continue;
                // UpdateGroup(pc, group.Name, user.WorkSectionTitle);
                //}
                //modir manabe
                //  var qbeUser = new UserPrincipal(pc) { Enabled = true };
                //  var srch = new PrincipalSearcher(qbeUser);
                //  var adUsers = srch.FindAll();
                //  var resourseManagementCodes = new[]
                //{
                //      "00105", "0105", "102", "103", "104", "105", "106",
                //      "107", "108", "109", "110", "111", "112"
                //  };
                //  foreach (var t in resourseManagementCodes) {
                //      var user = bankUsers.FirstOrDefault(x => x.CodeMahalKhedmat == t);
                //      if (user == null) continue;
                //      UpdateGroup(pc, "rm"+t, user.WorkSectionTitle);
                //  }

                //  var count = adUsers.Count();
                // Parallel.ForEach(adUsers, adUser =>{
                // create WorkSections
                var workSections = await _personInfoRepository.GetWorkSections();
                //foreach (var workSection in workSections)
                //{
                //    //OU=CEO,OU=BA24,DC=Ab,DC=net
                //    PrincipalContext officePc = new PrincipalContext(ContextType.Domain, "ab.net", "OU=Office,OU=CEO,OU=BA24,DC=Ab,DC=net",
                //        ContextOptions.Negotiate, _userName, _password);
                //    CreateNewGroup(officePc, "WS" + workSection.WorkSectionId, workSection.WorkSectionTitle);
                //}
                foreach (var workSection in workSections){
                    //OU=CEO,OU=BA24,DC=Ab,DC=net
                    var officePc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                        ContextOptions.Negotiate, _userName, _password);
                    UpdateGroupSecurity(officePc, "WS" + workSection.WorkSectionId, workSection.WorkSectionTitle);
                }
            }
        }
        private DirectoryEntry GetDirectoryEntry(string path){
            var de = new DirectoryEntry(path, _userName, _password) {AuthenticationType = AuthenticationTypes.Secure};
            return de;
        }
        private void CheckUser(string personnelId){
            var de = GetDirectoryEntry("LDAP://ab.net/DC=ab,DC=net");
            var search = new DirectorySearcher(de) {Filter = "(&(objectClass=user)(description=" + personnelId + "))"};
            var result = search.FindOne();
            if (result == null) return;
            using (var user = GetDirectoryEntry(result.Path)) { var tt = user.Properties["title"][0].ToString(); }
        }
        private void MoveToAnotherOu(string personnelId, string oUPath){
            var de = GetDirectoryEntry("LDAP://ab.net/DC=ab,DC=net");
            var search = new DirectorySearcher(de) {Filter = "(&(objectClass=user)(description=" + personnelId + "))"};
            var result = search.FindOne();
            if (result == null) return;
            var user = GetDirectoryEntry(result.Path);
            var theNewParent = GetDirectoryEntry(oUPath);
            user.MoveTo(theNewParent);
            theNewParent.Close();
            user.Close();
        }
        private void AddJobTitle(string personnelId, string jobTitle){
            var de = GetDirectoryEntry("LDAP://ab.net/DC=ab,DC=net");
            var search = new DirectorySearcher(de) {Filter = "(&(objectClass=user)(description=" + personnelId + "))"};
            var result = search.FindOne();
            if (result == null) return;
            using (var user = GetDirectoryEntry(result.Path)){
                user.Properties["title"].Clear();
                user.Properties["title"].Add(jobTitle);
                user.CommitChanges();
            }
        }
        private void AddPictureToUser(string strFilePath, string fileName){
            // Open file
            // limit size to 100 kb
            var inFile = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);

            // Retrive Data into a byte array variable
            var binaryData = new byte[inFile.Length];
            inFile.Read(binaryData, 0, (int) inFile.Length);
            inFile.Close();
            var de = GetDirectoryEntry("LDAP://ab.net/DC=ab,DC=net");
            var userName = fileName;
            var search = new DirectorySearcher(de) {Filter = "(&(objectClass=user)(description=" + userName + "))"};
            //search.Filter = "(&(objectCategory=Person)(objectClass=User)(&(anr=" + userName + "* )(displayName=*)(sn=*)))";
            var result = search.FindOne();
            if (result != null){
                using (var user = new DirectoryEntry(result.Path, "smo.hosseini", "159753*qaz")){
                    user.Properties["thumbnailPhoto"].Clear();
                    user.Properties["thumbnailPhoto"].Add(binaryData);
                    user.CommitChanges();
                }
            }
            else{
                using (var file = new StreamWriter(@"C:\Project\Coonect To Active Directory\Log.txt", true)) {
                    file.WriteLine(userName);
                }
            }
        }
        private void AddUserToGroup(PrincipalContext pc, UserPrincipal user, string groupName){
            var group = GroupPrincipal.FindByIdentity(pc, groupName);
            if (group == null){
                _logger.Error(new FaultDto
                {
                    Location = "ActiveDirectoryService UpdateGroup",
                    Message = $"group name : {groupName} is not valid"
                });
                return;
            }
            if (@group.Members.Contains(user)) return;
            @group.Members.Add(user);
            @group.Save();
        }
        private void SetAttributValue(string userName, string attributeName, string value){
            using (var pc = new PrincipalContext(ContextType.Domain, "ab.net", null,
                ContextOptions.Negotiate, _userName, _password)){
                using (var user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, userName)){
                    var de = user.GetUnderlyingObject() as DirectoryEntry;
                    de.Properties[attributeName].Clear();
                    de.Properties[attributeName].Add(value);
                    de.CommitChanges();
                }
            }
        }
        private void RemoveUserFromGroup(PrincipalContext pc, UserPrincipal user, string groupName){
            var group = GroupPrincipal.FindByIdentity(pc, groupName);
            if (group == null){
                _logger.Error(new FaultDto
                {
                    Location = "ActiveDirectoryService UpdateGroup",
                    Message = $"group name : {groupName} is not valid"
                });
                return;
            }
            if (!@group.Members.Contains(user)) return;
            @group.Members.Remove(user);
            @group.Save();
        }
        /// <summary>
        ///     Creates a new group in Active Directory
        /// </summary>
        /// <param name="pc">The OU location you want to save your new Group</param>
        /// <param name="sGroupName">The name of the new group</param>
        /// <param name="sDescription">The description of the new group</param>
        /// <param name="oGroupScope">The scope of the new group</param>
        /// <param name="bSecurityGroup">
        ///     True is you want this group to be a security group, false if you want this as a
        ///     distribution group
        /// </param>
        /// <returns>Retruns the GroupPrincipal object</returns>
        private GroupPrincipal CreateNewGroup(PrincipalContext pc, string sGroupName, string sDescription,
            GroupScope oGroupScope = GroupScope.Global, bool bSecurityGroup = false){
            var oGroupPrincipal = new GroupPrincipal(pc, sGroupName)
            {
                Description = sDescription,
                DisplayName = sDescription,
                GroupScope = oGroupScope,
                IsSecurityGroup = bSecurityGroup
            };
            oGroupPrincipal.Save();
            return oGroupPrincipal;
        }
        private void UpdateGroup(PrincipalContext pc, string groupName, string description){
            var group = GroupPrincipal.FindByIdentity(pc, groupName);
            if (group == null) return;
            group.Description = description;
            group.DisplayName = description;
            group.Save();
        }
        private void UpdateGroupSecurity(PrincipalContext pc, string groupName, string description){
            var group = GroupPrincipal.FindByIdentity(pc, groupName);
            if (group == null) return;
            group.IsSecurityGroup = true;
            group.Save();
        }
    }
}