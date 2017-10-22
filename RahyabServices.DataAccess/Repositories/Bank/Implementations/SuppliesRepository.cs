using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class SuppliesRepository : ISuppliesRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public SuppliesRepository(IDataContextFactory dataContextFactory){
            _dataContextFactory = dataContextFactory;
        }
        public async Task<DataSet> GetAccountOwners(string account){
            return await Task.Run(() =>{
                using (var db = _dataContextFactory.GetBankDataContext("Hamyar")){
                    var con = new SqlConnection(db.Database.Connection.ConnectionString);
                    var com = new SqlCommand("SP_GET_ACCOUNTS_OBJECTS", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    com.Parameters.AddWithValue("@AccountNumber", account);
                    var da = new SqlDataAdapter {SelectCommand = com};
                    var ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            });
        }
    }
}