using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Extensions;
using RahyabServices.Common.Logging;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Sharepoint{
    public class OperationDepartmentService: IOperationDepartmentService{
        private readonly IConvertListRepository _convertListRepository;
        private readonly IQueryListRepository _queryListRepository;
        private readonly IAtmOutputLibRepository _atmOutputLibRepository;
        private readonly ILogger _logger;
        public OperationDepartmentService(IConvertListRepository convertListRepository, IQueryListRepository queryListRepository, IAtmOutputLibRepository atmOutputLibRepository, ILogger logger){
            _convertListRepository = convertListRepository;
            _queryListRepository = queryListRepository;
            _atmOutputLibRepository = atmOutputLibRepository;
            _logger = logger;
        }
        public bool CreateCsvFile(){
            try { 
            var query =
                "<View><Query><OrderBy><FieldRef Name='Title' Ascending='TRUE'></FieldRef>" +
                "</OrderBy><Where><Neq><FieldRef Name='ID'  ></FieldRef><Value Type='Number'>0" +
                 "</Value></Neq></Where></Query></View>";
            var items = _convertListRepository.GetItems(query);
            var item = _queryListRepository.GetLastItem();
            var fileName = item.Title + ".csv";
            var csv = new StringBuilder();
            Directory.CreateDirectory("OperationDepartmentTempFile");
            var tfc = new TempFileCollection("OperationDepartmentTempFile", false);
            var fileNameTfc = tfc.AddExtension("csv");
            foreach (var it in items) {
                csv.AppendLine(it.Convert);
            }
            File.WriteAllText(fileNameTfc, csv.ToString());
            _atmOutputLibRepository.UploadFile(fileName,File.OpenRead(fileNameTfc));
            return true;
            }
            catch (Exception exception) {
                _logger.Error(new FaultDto("OperationDepartmentService", exception.GetMessage(), exception.StackTrace, FaultSource.Endpoint));
                return false;
            }
        }
    }
}