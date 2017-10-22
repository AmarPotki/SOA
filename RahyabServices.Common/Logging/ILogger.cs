namespace RahyabServices.Common.Logging
{
    public interface ILogger
    {
        void Error(FaultDto faultDto);
        void Trace(FaultDto faultDto);
        void Info(FaultDto faultDto);
        void Fatal(FaultDto faultDto);
        void Warn(FaultDto faultDto);
    }
}