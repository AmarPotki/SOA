using RahyabServices.Business.Domain.Models.BankPerson;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.BankPerson;
using RahyabServices.DataAccess.Repositories.BankPerson.Interfaces;
namespace RahyabServices.DataAccess.Repositories.BankPerson.Implementations{
    public class OrganizationUnitsRepository : BankPersonRepositoryBase<OrganizationUnits>, IOrganizationUnitsRepository{
        public OrganizationUnitsRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}