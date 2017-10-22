using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.Business.Domain.Models.BranchMarketing
{
   public class CommunicationCustomerData
    {
        public string Branch { get; set; }
        //واحد بررسی کننده
        public string CheckingUnit { get; set; }
        public string CustomerID { get; set; }
        //پتانسیل مشتری-M
        public string Rank { get; set; }

        public string CustType { get; set; }
        public  string CustomerName { get; set; }
        public string CustomerTell { get; set; }
        //مانده حساب در زمان تعریف مشتری
        public  double? AccountBalance { get; set; }
        //آخرین مانده حساب مشتری
        public double? Latestaccountbalance { get; set; }
        //صندوق
        public double? cashdesk { get; set; }
        //تسهیلات
        public double? Facility { get; set; }
        //کمپین
         public  string CampainId { get; set; }
        //تعداد هفته هایی که از اوج مشتری گذشته-R
        public double? RECENCY { get; set; }
        //تعداد هفته های فعالیت مشتری-F
        public  double? FREQUENCY { get; set; }
       public MainRevertCusts Main
        { get; set; }
   }
}
