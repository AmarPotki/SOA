using System;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NLog;
namespace RahyabServices.Common.Logging
{
    public class Logger
    {
        public Logger()
        {
        }

        public void Error(string location, string message)
        {
            Log(location, message);
        }

        public void Trace(string location, string message)
        {
            Log(location, message);
        }

        public void Info(string location, string message)
        {
            Log(location, message);
        }

        public void Fatal(string location, string message)
        {
            Log(location, message);
        }

        public void Warn(string location, string message)
        {
            Log(location, message);
        }

        public void Error(string location, string message, string stackTrace)
        {
            Log(location, message, stackTrace);
        }

        public void Trace(string location, string message, string stackTrace)
        {
            Log(location, message, stackTrace);
        }

        public void Info(string location, string message, string stackTrace)
        {
            Log(location, message, stackTrace);
        }

        public void Fatal(string location, string message, string stackTrace)
        {
            Log(location, message, stackTrace);
        }

        public void Warn(string location, string message, string stackTrace)
        {
            Log(location, message, stackTrace);
        }

        private void Log(string location, string message)
        {
            try
            {
                var fault = new FaultDto(location, message, FaultSource.Web);
                AddCallContextData(fault);

            }
            catch { }
        }

        private void Log(string location, string message, string stackTrace)
        {
            try
            {
                var fault = new FaultDto(location, message, stackTrace, FaultSource.Web);
                AddCallContextData(fault);

            }
            catch { }
        }

        private void AddCallContextData(FaultDto faultDto)
        {
            try
            {
                var hostContext = (HttpContext)CallContext.HostContext;

                faultDto.WebCallContextData = new WebCallContextData
                                                  {
                                                      Machine = hostContext.Server.MachineName,
                                                      Url = hostContext.Request.Url.AbsoluteUri,
                                                      UrlReferrer =
                                                          hostContext.Request.UrlReferrer != null
                                                              ? hostContext.Request.UrlReferrer
                                                                    .AbsoluteUri
                                                              : string.Empty,
                                                      BrowserType = hostContext.Request.Browser.Type,
                                                      BrowserVersion =
                                                          hostContext.Request.Browser.Version,
                                                      UserAgent = hostContext.Request.UserAgent,
                                                      ClientId = hostContext.Request.UserHostAddress,
                                                      AuthHash =
                                                          hostContext.Request.Cookies[
                                                              Invariants.CookieKeys.UserHash] != null
                                                              ? hostContext.Request.Cookies[
                                                                  Invariants.CookieKeys.UserHash].Value
                                                              : string.Empty
                                                  };
            }
            catch
            {
                faultDto.WebCallContextData = new WebCallContextData();
            }
        }

        public void Error(FaultDto faultDto)
        {
            throw new NotImplementedException();
        }

        public void Trace(FaultDto faultDto)
        {
            throw new NotImplementedException();
        }

        public void Info(FaultDto faultDto)
        {
            throw new NotImplementedException();
        }

        public void Fatal(FaultDto faultDto)
        {
            throw new NotImplementedException();
        }

        public void Warn(FaultDto faultDto)
        {
            throw new NotImplementedException();
        }
    }
}