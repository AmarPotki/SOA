using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.Common.Cookies
{
    public interface ICookie
    {
        T GetValue<T>(string key);
        T GetValueForPersiamFullName<T>(string key);
        T GetValue<T>(string key, bool expireOnceRead);

        void Remove(string key);
        void SetValue<T>(string key, T value);
        void SetValue<T>(string key, T value, float expireDuration);
        void SetValueForPersiamFullName<T>(string key, T value, float expireDuration);
        void SetValue<T>(string key, T value, bool httpOnly);
        void SetValue<T>(string key, T value, float expireDuration, bool httpOnly);
        CookieType CookieType { get; set; }
    }

    public enum CookieType
    {
        Day,
        Hour,
        Minute
    }
}
