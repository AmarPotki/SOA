using System;
using System.Globalization;
using System.Threading.Tasks;

namespace RahyabServices.Common.Convertors
{
    public class DateTimeConvertor : IDateTimeConvertor
    {
        private readonly PersianCalendar _persianCalendar;

        public DateTimeConvertor()
        {
            _persianCalendar = new PersianCalendar();
        }

        public bool IsFriday => DateTime.Now.DayOfWeek == DayOfWeek.Friday;
        public string GetHistoryDateFormat(DateTime dateTime)
        {
            return
                $"{_persianCalendar.GetYear(dateTime).ToString(CultureInfo.InvariantCulture).Substring(2)}{_persianCalendar.GetMonth(dateTime):D2}{_persianCalendar.GetDayOfMonth(dateTime):D2}";
        }

        public async Task<string> GetHistoryDateFormatAsync(DateTime dateTime)
        {
            return
                await
                    Task.Run(
                        () =>
                            GetHistoryDateFormat(dateTime));
        }

        public string GetPersianDate(DateTime dateTime)
        {
            return
                $"{_persianCalendar.GetYear(dateTime):D4}/{_persianCalendar.GetMonth(dateTime):D2}/{_persianCalendar.GetDayOfMonth(dateTime):D2}";
        }

        public async Task<string> GetPersianDateAsync(DateTime dateTime)
        {
            return
                await
                    Task.Run(
                        () =>
                            GetPersianDate(dateTime));
        }

        public DateTime GetGregorianFromPersian(string dateTime)
        {
            var parts = dateTime.Split('/');
            return new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), _persianCalendar);
        }

        public async Task<DateTime> GetGregorianFromPersianAsync(string dateTime)
        {
            return
                await
                    Task.Run(
                        () =>
                            GetGregorianFromPersian(dateTime));
        }
        public string GetPersianDateWithOutSlashAndYear(string persianDate){
            return  persianDate.Replace("/", "").Remove(0, 2);
        }
        public string GetPersianDateWithOutSlash(DateTime dateTime){

            return GetPersianDate(dateTime).Replace("/", "");
        }
        public string GetPersianDateWithOutSlashAndYear(DateTime dateTime)
        {

            return GetPersianDate(dateTime).Replace("/", "").Remove(0, 2);
        }
        public DateTime GetGregorianFromPersianWithOutSlash(string persianDate){

            return new DateTime(int.Parse(persianDate.Substring(0, 4)), int.Parse(persianDate.Substring(4, 2)), int.Parse(persianDate.Substring(6, 2)), _persianCalendar);
        }
        public string InserSlashIntoStrPersianDate(string persianDate){
            return $"{persianDate.Substring(0, 4)}/{persianDate.Substring(4, 2)}/{persianDate.Substring(6, 2)}";
        }
    }
}