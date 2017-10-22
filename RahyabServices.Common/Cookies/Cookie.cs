using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RahyabServices.Common.Cryptography;

namespace RahyabServices.Common.Cookies
{
    public class Cookie : ICookie
    {
        private readonly ICryptographer _cryptographer;
        private static float _defaultExpireDuration = 30;
        private static bool _defaultHttpOnly = true;
        public CookieType CookieType { get; set; }

        public Cookie(ICryptographer cryptographer)
        {
            _cryptographer = cryptographer;
        }

        public static float DefaultExpireDuration
        {
            [DebuggerStepThrough]
            get { return _defaultExpireDuration; }

            [DebuggerStepThrough]
            set { _defaultExpireDuration = value; }
        }

        public static bool DefaultHttpOnly
        {
            [DebuggerStepThrough]
            get { return _defaultHttpOnly; }

            [DebuggerStepThrough]
            set { _defaultHttpOnly = value; }
        }

        public T GetValue<T>(string key)
        {
            return GetValue<T>(key, false);
        }

        public T GetValueForPersiamFullName<T>(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            var value = default(T);

            if (cookie == null) return value;
            if (string.IsNullOrWhiteSpace(cookie.Value)) return value;

            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                value = (T)converter.ConvertFromString(cookie.Value);
            }
            catch (NotSupportedException)
            {
                if (converter.CanConvertFrom(typeof(string)))
                {
                    value = (T)converter.ConvertFrom(cookie.Value);
                }
            }

            return value;
        }
        public T GetValue<T>(string key, bool expireOnceRead)
        {
            var cookie = HttpContext.Current.Request.Cookies[key];
            var value = default(T);

            if (cookie == null) return value;

            if (!string.IsNullOrWhiteSpace(cookie.Value))
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                try
                {
                    value = (T)converter.ConvertFromString(_cryptographer.Decrypt(cookie.Value));
                }
                catch (NotSupportedException)
                {
                    if (converter.CanConvertFrom(typeof(string)))
                    {
                        value = (T)converter.ConvertFrom(_cryptographer.Decrypt(cookie.Value));
                    }
                }
            }

            if (!expireOnceRead) return value;

            cookie = HttpContext.Current.Response.Cookies[key];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-100d);
            }

            return value;
        }

        public void Remove(string key)
        {
            var cookie = new HttpCookie(key, String.Empty) { Expires = DateTime.Now.AddDays(-1), HttpOnly = false };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SetValue<T>(string key, T value)
        {
            SetValue(key, value, DefaultExpireDuration, DefaultHttpOnly);
        }

        public void SetValue<T>(string key, T value, float expireDuration)
        {
            SetValue(key, value, expireDuration, DefaultHttpOnly);
        }

        public void SetValueForPersiamFullName<T>(string key, T value, float expireDuration)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var cookieValue = string.Empty;

            try
            {
                cookieValue = converter.ConvertToString(value);
            }
            catch (NotSupportedException)
            {
                if (converter.CanConvertTo(typeof(string)))
                {
                    cookieValue = (string)converter.ConvertTo(value, typeof(string));
                }
            }

            if (string.IsNullOrWhiteSpace(cookieValue)) return;

            var cookieExpireDuration = DateTime.Now;

            switch (CookieType)
            {
                case CookieType.Day:
                    cookieExpireDuration = cookieExpireDuration.AddDays(expireDuration);
                    break;
                case CookieType.Hour:
                    cookieExpireDuration = cookieExpireDuration.AddHours(expireDuration);
                    break;
                default:
                    cookieExpireDuration = cookieExpireDuration.AddMinutes(expireDuration);
                    break;
            }

            var cookie = new HttpCookie(key, cookieValue)
            {
                Expires = cookieExpireDuration,
                HttpOnly = DefaultHttpOnly
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SetValue<T>(string key, T value, bool httpOnly)
        {
            SetValue(key, value, DefaultExpireDuration, httpOnly);
        }

        public void SetValue<T>(string key, T value, float expireDuration, bool httpOnly)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            string cookieValue = string.Empty;

            try
            {
                cookieValue = converter.ConvertToString(value);
            }
            catch (NotSupportedException)
            {
                if (converter.CanConvertTo(typeof(string)))
                {
                    cookieValue = (string)converter.ConvertTo(value, typeof(string));
                }
            }

            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                var cookieExpireDuration = DateTime.Now;

                switch (CookieType)
                {
                    case CookieType.Day:
                        cookieExpireDuration = cookieExpireDuration.AddDays(expireDuration);
                        break;
                    case CookieType.Hour:
                        cookieExpireDuration = cookieExpireDuration.AddHours(expireDuration);
                        break;
                    default:
                        cookieExpireDuration = cookieExpireDuration.AddMinutes(expireDuration);
                        break;
                }

                var cookie = new HttpCookie(key, _cryptographer.Encrypt(cookieValue))
                {
                    Expires = cookieExpireDuration,
                    HttpOnly = httpOnly
                };

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
