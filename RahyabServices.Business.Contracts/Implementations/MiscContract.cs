using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class MiscContract : ContractBase, IMiscContract{
        private readonly IOperationDepartmentService _operationDepartment;
        public MiscContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IOperationDepartmentService operationDepartment)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _operationDepartment = operationDepartment;
        }
        public bool OperattionCreateCsvFile(string key){
            return key == "theS3crectC0de" && _operationDepartment.CreateCsvFile();
        }
    }
}