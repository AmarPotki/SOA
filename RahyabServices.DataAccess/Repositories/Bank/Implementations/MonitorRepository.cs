

using System;
using System.Data.Entity;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class MonitorRepository : BankRepositoryBase<Monitor>, IMonitorRepository{
        public MonitorRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<bool> IsFinishAbisTask(string date){
            return await QueryAsync(f => f.AnyAsync(x => x.FDate == date && x.PckName == "28X"));
        }
        public async Task<bool> IsFinishBankIranTask(string date)
        {
            return await QueryAsync(f => f.AnyAsync(x => x.FDate == date && x.PckName == "RPTFT"));
        }
        public async Task<bool> IsFinishAbisAndBankIranTask(string date,string abisDate){
            if (!(await QueryAsync(f => f.AnyAsync(x => x.FDate == abisDate && x.PckName == "28X")))) return false;
            return await QueryAsync(f => f.AnyAsync(x => x.FDate == date && x.PckName == "RPTFT"));
        }
    }
}