using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Common.Extensions;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;

namespace RahyabServices.DataAccess.Repositories.Bank.Implementations
{
    public class CustomerAddressRepository : BankRepositoryBase<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public CustomerAddressRepository(IDataContextFactory databaseFactory)
            : base(databaseFactory)
        {
            _dataContextFactory = databaseFactory;
        }

        public CustomerAddress GetByCustomerNumber(string customerNumber)
        {
            return Task.Run(async () => await GetByCustomerNumberAsync(customerNumber)).Result;
        }

        //public string GetCellPhone(string customerId)
        //{
        //    var customerAddress = Query(q => (q.FirstOrDefault(x => x.CustomerNumber == customerId)));

        //    customerInfo.Telephone.Trim().IsCellPhone() ? customerInfo.Telephone.Trim() : customerAddress.HomePhone.Trim().IsCellPhone() ?
        //            customerAddress.HomePhone.Trim() : customerAddress.BusinessPhone.Trim().IsCellPhone() ? customerAddress.BusinessPhone.Trim() :
        //            customerAddress.MobilePhone.Trim().IsCellPhone() ? customerAddress.MobilePhone.Trim() : string.Empty;

        //    if (address == null) return "";
        //    if (address.MobilePhone.IsCellPhone()) return address.MobilePhone;
        //    if (address.HomePhone.IsCellPhone()) return address.HomePhone;
        //    return address.BusinessPhone.IsCellPhone() ? address.BusinessPhone : "";
        //}

        //public async Task<string> GetCellPhoneAsync(string customerId)
        //{
        //    var address = await QueryAsync(async q => (await q.FirstOrDefaultAsync(x => x.CustomerNumber == customerId)));
        //    if (address == null) return "";
        //    if (address.MobilePhone.IsCellPhone()) return address.MobilePhone;
        //    if (address.HomePhone.IsCellPhone()) return address.HomePhone;
        //    return address.BusinessPhone.IsCellPhone() ? address.BusinessPhone : "";
        //}

        public async Task<CustomerAddress> GetByCustomerNumberAsync(string customerNumber)
        {
            return
                await
                    QueryAsync(async q => await q.SingleOrDefaultAsync(w => w.CustomerNumber == customerNumber));
        }
    }
}
