using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Sharepoint.DailyliquidityReport;
using RahyabServices.Business.Dtos.Finance;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;

namespace RahyabServices.Business.Services.Implementations.Sharepoint{
    public class FinanceService:IFinanceService{
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly IDailyliquidityReportRepository _dailyliquidityRepository;
        private readonly IReportFacilityDetailRepository _facilityDetailRepository;
        public FinanceService(IDateTimeConvertor dateTimeConvertor, IDailyliquidityReportRepository dailyliquidityRepository, IReportFacilityDetailRepository facilityDetailRepository){
            _dateTimeConvertor = dateTimeConvertor;
            _dailyliquidityRepository = dailyliquidityRepository;
            _facilityDetailRepository = facilityDetailRepository;
        }
        public async Task<string> GetDailyReport(GetDailyliquidityReportDtq dtq){
          
            var fromDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(dtq.FromPersianDate);
            var toDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(dtq.ToPersianDate);
            //2017-07-23T21:45:20Z
            var query = $"<View><Query><Where><And><Geq><FieldRef Name='Date' />" +
                        $"<Value Type='DateTime'>{fromDate.ToString("yyyy-MM-ddT00:00:00Z")}</Value>" +
                        $"</Geq><Leq><FieldRef Name='Date' />" +
                        $"<Value  Type='DateTime'>{toDate.ToString("yyyy-MM-ddT23:59:59Z")}</Value></Leq>" +
                        $"</And></Where></Query></View>";
            var items = _dailyliquidityRepository.GetItems(query);
            if (!items.Any()) return "";
            var dailies = items.GroupBy(x => x.Date.Date);
            var lst = items.Select(dr => new DailyliquidityReportDto
            {
                Description = dr.Description, Amount = dr.Value, PersianDate = _dateTimeConvertor.GetPersianDate(dr.Date)
            }).ToList();

            var tempDate = fromDate.Date;
            while (toDate.Date > tempDate){
                if(!dailies.Any(x => x.Key == tempDate))
                {
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "مانده ابتدای وقت",
                        Priority = 1,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "شاپرک",
                        Priority = 2,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "پایا",
                        Priority = 3,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "شتاب",
                        Priority = 4,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "چکاوک عادی",
                        Priority = 5,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "چکاوک رمزدار",
                        Priority = 6,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "ساتنای دریافتی",
                        Priority = 7,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "ساتنای پرداختی",
                        Priority = 8,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    }); lst.Add(new DailyliquidityReportDto
                    {
                        Description = "تسهیلات دریافتی",
                        Priority = 9,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "استرداد تسهیلات دریافتی",
                        Priority = 10,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    }); lst.Add(new DailyliquidityReportDto
                    {
                        Description = "تسهیلات اعطایی",
                        Priority = 11,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "استرداد تسهیلات اعطایی",
                        Priority = 12,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "سپرده قانونی",
                        Priority = 13,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "مانده انتهای وقت",
                        Priority = 14,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new DailyliquidityReportDto
                    {
                        Description = "مقایسه مانده ابتدا و انتهای وقت",
                        Priority = 15,
                        Amount = 0,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });

                }
                tempDate=tempDate.AddDays(1);
            }
            var grs = lst.OrderBy(x => x.Priority)
                   .GroupBy(x => x.Description)
                   .Select(g => new {
                       Description = g.Key,
                       Values = g.Select(t => new {
                           t.PersianDate,
                           t.Amount
                       }) });
            var str = "{\"data\":[";
            foreach (var dr in grs){
                
                str += "{\"شرح\":\"" + dr.Description + "\",";
                var values = dr.Values.OrderBy(f => f.PersianDate);
                str = values.Aggregate(str, (current, value) => current + ("\"" + value.PersianDate + "\":" + value.Amount + ","));
                str = str.Remove(str.Length-1);
                str += "},";
               
            }
            str = str.Remove(str.Length - 1);
            str += "]}";
            return str;
        }
        public async Task<string> GetReportFacility(GetReportFacilityDetailDtq getDaily){
            var fromDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(getDaily.FromPersianDate);
            var toDate = _dateTimeConvertor.GetGregorianFromPersianWithOutSlash(getDaily.ToPersianDate);
            //2017-07-23T21:45:20Z
            var query = $"<View><Query><Where><And><Geq><FieldRef Name='Date' />" +
                        $"<Value Type='DateTime'>{fromDate.ToString("yyyy-MM-ddT00:00:00Z")}</Value>" +
                        $"</Geq><Leq><FieldRef Name='Date' />" +
                        $"<Value  Type='DateTime'>{toDate.ToString("yyyy-MM-ddT23:59:59Z")}</Value></Leq>" +
                        $"</And></Where></Query></View>";
            var items = _facilityDetailRepository.GetItems(query);
            if (!items.Any()) return "";
            var dailies = items.GroupBy(x => x.Date.Date);
            var lst = items.Select(dr => new ReportFacilityDetailDto
            {
                Description = dr.Description,
                Amount = dr.Value,
                PersianDate = _dateTimeConvertor.GetPersianDate(dr.Date)
            }).ToList();

            var tempDate = fromDate.Date;
            while (toDate.Date > tempDate)
            {
                if (!dailies.Any(x => x.Key == tempDate))
                {
                    lst.Add(new ReportFacilityDetailDto
                    {
                        Description = "مانده سپرده بانکی",
                        Amount = 0,
                        Priority = 1,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new ReportFacilityDetailDto
                    {
                        Description = "مانده تسهیلات دریافتی از بانکها",
                        Amount = 0,
                        Priority = 2,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new ReportFacilityDetailDto
                    {
                        Description = "جمع کل سپرده دریافتی از بانکها",
                        Amount = 0,
                        Priority = 3,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new ReportFacilityDetailDto
                    {
                        Description = "مانده تسهیلات اعطایی به بانکها",
                        Amount = 0,
                        Priority = 4,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });
                    lst.Add(new ReportFacilityDetailDto
                    {
                        Description = "خالص بدهی / طلب",
                        Amount = 0,
                        Priority = 5,
                        PersianDate = _dateTimeConvertor.GetPersianDate(tempDate),
                    });              

                }
                tempDate = tempDate.AddDays(1);
            }
            var grs = lst.OrderBy(x => x.Priority)
                   .GroupBy(x => x.Description)
                   .Select(g => new {
                       Description = g.Key,
                       Values = g.Select(t => new {
                           t.PersianDate,
                           t.Amount
                       })
                   });
            var str = "{\"data\":[";
            foreach (var dr in grs)
            {

                str += "{\"شرح\":\"" + dr.Description + "\",";
                var values = dr.Values.OrderBy(f => f.PersianDate);
                str = values.Aggregate(str, (current, value) => current + ("\"" + value.PersianDate + "\":" + value.Amount + ","));
                str = str.Remove(str.Length - 1);
                str += "},";

            }
            str = str.Remove(str.Length - 1);
            str += "]}";
            return str;
        }
    }
}