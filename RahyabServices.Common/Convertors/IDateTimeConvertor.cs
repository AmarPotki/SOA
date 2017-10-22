using System;
using System.Threading.Tasks;

namespace RahyabServices.Common.Convertors
{
    public interface IDateTimeConvertor
    {
        string GetHistoryDateFormat(System.DateTime dateTime);
        Task<string> GetHistoryDateFormatAsync(System.DateTime dateTime);

        string GetPersianDate(DateTime dateTime);
        Task<string> GetPersianDateAsync(DateTime dateTime);
        DateTime GetGregorianFromPersian(string dateTime);
        Task<DateTime> GetGregorianFromPersianAsync(string dateTime);
        string GetPersianDateWithOutSlashAndYear(string persianDate);
        string GetPersianDateWithOutSlashAndYear(DateTime dateTime);
        string GetPersianDateWithOutSlash(DateTime dateTime);
        
            DateTime GetGregorianFromPersianWithOutSlash(string persianDate);
        string InserSlashIntoStrPersianDate(string persianDate);
    }
}