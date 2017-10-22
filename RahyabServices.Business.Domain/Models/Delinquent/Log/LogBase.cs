using System;

namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public abstract class LogBase : IDelinquentEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        //personelCode
        public string Author { get; set; }
        public CustomerDelinquent CustomerDelinquent { get; set; }
        public int CustomerDelinquentId { get; set; }

        public LogBase SetCustomerDelinquentId(int id)
        {
            CustomerDelinquentId = id;
            return this;
        }
    }
}