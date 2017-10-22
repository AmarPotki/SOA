using NLog;
using NLog.Config;
using NLog.Targets;

namespace RahyabServices.Business.Bootstrapper.BootstrapTask
{
    public class ConfigureNLog : IBootstrapTask
    {
        public int Priority
        {
            get { return 4; }
        }

        public void Execute()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();

            config.AddTarget("file", fileTarget);

            fileTarget.FileName = @"C:\Log\Service Host - ${shortdate}.txt";
            fileTarget.Layout = "${longdate}    ${message}";
            fileTarget.Encoding = System.Text.Encoding.UTF8;
            fileTarget.ArchiveAboveSize = 10485760;
            fileTarget.MaxArchiveFiles = 30;
            fileTarget.ArchiveEvery = FileArchivePeriod.Hour;

            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }
    }
}
