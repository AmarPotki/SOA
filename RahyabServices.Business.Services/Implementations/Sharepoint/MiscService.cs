using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Misc;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
namespace RahyabServices.Business.Services.Implementations.Sharepoint{
    public class MiscService : IMiscService{
        private readonly int[] _leapYear = {1395, 1399, 1403, 1407, 1411};
        public async Task<double> CalculateProfitAsync(CalculateProfitDtc calculateProfitDtc){
            return await Task.Run(() => CalculateProfit(calculateProfitDtc));
        }
        private double CalculateProfit(CalculateProfitDtc calculateProfitDtc){
            var persianCal = new PersianCalendar();
            var year = persianCal.GetYear(DateTime.Now);
            var totalDays = 365;
            if (_leapYear.Any(x => x == year)) totalDays = 366;
            return Math.Round(calculateProfitDtc.Profit*calculateProfitDtc.Amount*calculateProfitDtc.Days/totalDays,2);
        }
    }
}