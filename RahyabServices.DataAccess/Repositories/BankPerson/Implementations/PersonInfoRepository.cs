using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BankPerson;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Repositories.BankPerson.Interfaces;
namespace RahyabServices.DataAccess.Repositories.BankPerson.Implementations{
    public class PersonInfoRepository : IPersonInfoRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public PersonInfoRepository(IDataContextFactory dataContextFactory){
            _dataContextFactory = dataContextFactory;
        }
        public async Task<PersonInfo> GetPersonInfo(int personelId){
            using (var db = _dataContextFactory.GetBankPersonDataContext()){
                var query =
                    "SELECT * FROM[View_PersonelInfo] Where ShomarehPersenely = @PERSONEL_ID And EmploymentStatusID IN(1, 4, 8, 9, 10, 11)";
                var nn =
                    await
                        db.Database.SqlQuery<PersonInfo>(query, new SqlParameter("PERSONEL_ID", personelId))
                            .ToListAsync();
                return nn.First();
            }
        }
        public async Task<IEnumerable<PersonInfo>> GetAllActive()
        {
            using (var db = _dataContextFactory.GetBankPersonDataContext())
            {
                var query =
                    "SELECT * FROM[View_PersonelInfo] where EmploymentStatusID IN(1,8, 9, 10, 11)";
               return 
                    await
                        db.Database.SqlQuery<PersonInfo>(query)
                            .ToListAsync();
           
            }
        }
        public async Task<IEnumerable<PersonInfo>> GetAllDeActive()
        {
            using (var db = _dataContextFactory.GetBankPersonDataContext())
            {
                var query =
                    "SELECT * FROM [View_PersonelInfo] where EmploymentStatusID IN (2,3, 5,6 ,7)";
                return
                     await
                         db.Database.SqlQuery<PersonInfo>(query)
                             .ToListAsync();

            }
        }
        public async Task<IEnumerable<WorkSection>> GetWorkSections(){

           
            using (var db = _dataContextFactory.GetBankPersonDataContext())
            {
                var query = @"SELECT distinct
max([WorkSectionID]) as WorkSectionId
,max([WorkSectionTitle]) as WorkSectionTitle
FROM[TAT_DWBI_ODS].[dbo].[View_PersonelInfo]
where EmploymentStatusID IN(1, 4, 8, 9, 10, 11)  and WorkSectionTitle is not null  and WorkSectionTitle != '' and WorkSectionID != '1'and WorkSectionID != '9'
and MahalKhedmat not like '%فردا%' and CodeMahalKhedmat in('12','2','3')
group by[WorkSectionID]";
               return 
                    await
                        db.Database.SqlQuery<WorkSection>(query) .ToListAsync();
               
            }
        }
        public async Task<string> GetBranchManagerUserName(string branchCode)
        {
            using (var db = _dataContextFactory.GetBankPersonDataContext()){
                var managers =
                    await
                        db.Database.SqlQuery<GET_MASTER_BY_CODEResult>("GET_MASTER_BY_CODE @codeMahalKhedmat , @pROCESS_CODE", new SqlParameter("codeMahalKhedmat", branchCode), new SqlParameter("pROCESS_CODE", "02"))
                            .ToListAsync();
                var getMasterByCodeResult
                    = managers?.FirstOrDefault();
                if (getMasterByCodeResult != null) return getMasterByCodeResult?.USER_NAME.Trim() ?? "";
                return "";
            }
        }
        public async Task<string> GetManagerUserName(string workStationId){
            using (var db = _dataContextFactory.GetBankPersonDataContext()){
                var managers =
                  await
                      db.Database.SqlQuery<GET_MASTER_BY_CODEResult>("GET_MASTER_OF_ORG @WORKSECTION , @PROCESS_CODE", new SqlParameter("WORKSECTION", workStationId), new SqlParameter("PROCESS_CODE", "02"))
                          .ToListAsync();
                var getMasterByCodeResult
                    = managers?.FirstOrDefault();
                if (getMasterByCodeResult != null) return getMasterByCodeResult?.USER_NAME.Trim() ?? "";
                return "";
            }
        }
        public
            async Task<bool> IsValidUserName(string userName){
            using (var db = _dataContextFactory.GetBankPersonDataContext())
            {
                return await db.PersonAbUsers.AnyAsync(x => x.UserName == userName);
            }
        }
        public async Task<PersonAbUser> GetPersonAbUser(string userName){
            using (var db = _dataContextFactory.GetBankPersonDataContext()) {
                return await db.PersonAbUsers.FirstOrDefaultAsync(x => x.UserName == userName);
            }
        }
    }
}