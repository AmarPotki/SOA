using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IMonitorRepository : IBankRepository<Monitor>{
        Task<bool> IsFinishAbisTask(string date);
        Task<bool> IsFinishBankIranTask(string date);
        Task<bool> IsFinishAbisAndBankIranTask(string date, string abisDate);
    }
}