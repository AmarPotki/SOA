using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.Sharepoint.BranchMarketing
{
    [SharepointListId("{52D36598-817A-4CF1-BF90-092F2F5B132E}")]
    public class CommunicationCustomer: IEntitySharepointMapper
    {
        public string Title { get; set; }

        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        //شعبه هدف
        public string Branch { get; set; }
         
        public double State { get; set; }

        public string CustomerID { get; set; }

        public string Description { get; set; }
        public string Rank { get; set; }
        public string CustType { get; set; }
        public string CustomerName { get; set; }
       // public DateTime ActionDate { get; set; }
       // //دلایل عدم رویگردانی
        //[SharepointFieldName("ItemCallLater")]
        //public LookupFieldMapper ItemCallLater { get; set; }
        //دلایل رویگردانی   
        //[SharepointFieldName("C_ReasonsRejection")]
       // public LookupFieldMapper CReasonsRejection { get; set; }

        public string Waycalling { get; set; }
        public string CustomerTell { get; set; }
        //مانده حساب در زمان تعریف مشتری
        public double AccountBalance { get; set; }
        //آخرین مانده حساب مشتری
        public double Latestaccountbalance { get; set; }
        //حذف توسط اداره بازاریابی
        public bool Isdeleted { get; set; }
        //صندوق
        public double cashdesk { get; set; }
        //تسهیلات
        public double Facility { get; set; }
        //نام کمپین
       [SharepointFieldName("CampainName")]
        public LookupFieldMapper CampainName { get; set; }

        //تعداد هفته هایی که از اوج مشتری گذشته
        public double RECENCY { get; set; }

        //تعداد هفته های فعالیت مشتری
        public double FREQUENCY { get; set; }

        [SharepointFieldName("Status")]
        public LookupFieldMapper Status { get; set; }

        public string CheckingUnit { get; set; }
        public double RowDBID { get; set; }
        //مبلغ اظهار شده توسط شعبه در وضعیت موفق
        [SharepointFieldName("Amount")]
        public double Amount { get; set; }
        //مبلغ حساب مشتری یک روز بعد از موفق اعلام شدن شعبه
        public double NextDayAmount { get; set; }
        //وضعیت موفق حقیقی 
        public bool IsSuccess { get; set; }
        public DateTime? ActionDate { get; set; }



    }
}
