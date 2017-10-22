using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RahyabServices.Business.Dtos.ParsLogic;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Facades.ParsLogic;
using RahyabServices.Business.Facades.Proxy.wPMI;
namespace RahyabServices.Business.Facades.Implementations{
    public class ParsLogicFacade : IParsLogicFacade{
        private const string WebServiceAddress = "";
        private static readonly string PmiUser = "";
        private static readonly string PmiPass = "";
        private readonly PMICase _pmiCase;
        public ParsLogicFacade(){
            _pmiCase = new PMICase(WebServiceAddress);
            _pmiCase.Login(PmiUser, PmiPass);
        }

        public async Task<IEnumerable<ResultGetRowsActivityIdDto>> GetRows(TaslListFilterDtc dtc)
        {

            var tskLstFilter = new TaskListFilter
            {
                activityId = dtc.ActivityId,
                activityType = dtc.ActivityType,
                
            };
            var lrItems = _pmiCase.client.taskGetListRows(1000,1,null, tskLstFilter);
            List<ResultGetRowsActivityIdDto> lstRowsActivity = new List<ResultGetRowsActivityIdDto>();
           
            for (int i = 0; i < lrItems.totalRows; i++) {
                lstRowsActivity.Add(new ResultGetRowsActivityIdDto
                {
                    RequestType = ((IdString)lrItems.rows[i].data[1]).str,
                    Status = ((StringData)lrItems.rows[i].data[2]).value,
                    Branch = ((IdString)lrItems.rows[i].data[5]).str,
                    Description = ((StringData)lrItems.rows[i].data[19]).value,
                    ActionerFN = ((User)lrItems.rows[i].data[9]).firstName,
                    ActionerLN = ((User)lrItems.rows[i].data[9]).lastName
                });
               
               
            }
            return lstRowsActivity;
        }
        public async Task<int> CreateNewCase(BranchInformationDtc dtc){
            var customerSort = new Sort[0];
            var custLstFilter = new CustomerListFilter();
            var customerfrmVal = new PlvListFilter.FormValue[1];
            //search User
            customerfrmVal[0] = new PlvListFilter.FormValue
            {
                elementIdent = "custcode1",
                value = dtc.BranchCode
            };
            custLstFilter.type = 1;
            custLstFilter.formValues = customerfrmVal;
            var custLr = _pmiCase.client.customerGetListRows(30, 1, customerSort, custLstFilter);
            //  var customer = custLr.rows.First().data[0] as Customer;
            var domainindex = 0;
            for (var i = 0; i < custLr.columns.Length; i++){
                if (custLr.columns[i].ident != "domain") continue;
                domainindex = i;
                break;
            }
            var custid = (from item in custLr.rows where ((StringData) item.data[domainindex]).value == "23" select Convert.ToInt32(item.id)).FirstOrDefault();
            var caseDef = new CaseDefinition
            {
                customer = custid,
                domain = 23,
                subjectIdents = new[] {dtc.RequestType},
                description = dtc.Description
            };
            var pmiUser = _pmiCase.client.getCurrentUser();
            caseDef.creatorGroup = pmiUser.groups[0].id;
            var taskDef = new TaskDefinition
            {
                startDate = DateTime.Now,
                operationIdent = "erja"
            };
            //taskDef.d
            var assignedTo = new UserGroupDefinition {@group = 230034};
            //    assignedTo.user = 0;
            taskDef.assignedTo = new[] {assignedTo};
            var newCase = new NewCase
            {
                activity = caseDef,
                task = taskDef
            };
            var result = _pmiCase.client.caseCreate(newCase);
            return result.activityId;
        }
    }
}