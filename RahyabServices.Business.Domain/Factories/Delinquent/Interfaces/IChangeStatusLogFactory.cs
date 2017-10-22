using RahyabServices.Business.Domain.Models.Delinquent.Log;
namespace RahyabServices.Business.Domain.Factories.Delinquent.Interfaces{
    public interface IChangeStatusLogFactory{
        ChangeStatusLog Create(int delinquentId, string status);
    }
}