using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Supplies;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Supplies;
using RahyabServices.DataAccess.Repositories.Supplies.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Supplies.Implementations{
    public class RequestSerialIdRepository :IranNaraRepositoryBase<RequestSerialId>,IRequestSerialIdRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public RequestSerialIdRepository(IDataContextFactory dataContextFactory):base(dataContextFactory){
            _dataContextFactory = dataContextFactory;
        }

    }
}