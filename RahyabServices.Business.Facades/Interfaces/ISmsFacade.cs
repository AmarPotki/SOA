using System;

namespace RahyabServices.Business.Facades.Interfaces
{
    public interface ISmsFacade
    {
        long Send(string number, string message);
        string CheckSms(long smsId);
        bool IsDelivered(long smsId);
        long Send(string cellPhone, string message, DateTime dateTime);
    }
}