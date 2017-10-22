using System.Runtime.Remoting.Messaging;
using System.Web;
using NLog;

namespace RahyabServices.Common.Logging
{
    public class NLogger : ILogger
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void Error(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            Logger.Error(faultDto.ToJsonString());
        }

        public void Trace(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            Logger.Trace(faultDto.ToJsonString());
        }

        public void Info(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            Logger.Info(faultDto.ToJsonString());
        }

        public void Fatal(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            Logger.Fatal(faultDto.ToJsonString());
        }

        public void Warn(FaultDto faultDto)
        {
            AddCallContextData(faultDto);
            Logger.Warn(faultDto.ToJsonString());
        }

        private void AddCallContextData(FaultDto faultDto)
        {
            if (faultDto.FaultSource == FaultSource.Endpoint)
            {
                var hostContext = CallContext.HostContext as HttpContext;

                if (hostContext != null)
                {
                    faultDto.Endpoint = new EndpointCallContextData
                                            {
                                                Machine = hostContext.Server.MachineName,
                                                Url = hostContext.Request.Url.AbsoluteUri
                                            };
                }
            }
        }
    }
}