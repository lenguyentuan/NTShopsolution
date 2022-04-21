using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;

namespace NTShopSolution.ActivityLog
{
    public struct File
    {

    }
    public class FileActivityLog : IActivityLog<File>
    {
        private Logger _log;
        private readonly string _d;
        public FileActivityLog(IConfiguration config)
        {
            _d = config.GetValue<string>("ActivityLog.File.Directory");
            _log = new LoggerConfiguration().WriteTo.File($"{_d}\\log_", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, shared: true).CreateLogger();
        }
        public IActivityLog<File> SetFileName(string fileName)
        {
            _log = new LoggerConfiguration().WriteTo.File($"{_d}\\{fileName}", rollOnFileSizeLimit: false, shared: true).CreateLogger();
            return this;
        }

        public void Error(string messageTemplate)
        {
            _log.Error(messageTemplate);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _log.Error(messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate)
        {
            _log.Fatal(messageTemplate);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            _log.Fatal(messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate)
        {
            _log.Information(messageTemplate);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            _log.Information(messageTemplate, propertyValues);
        }

        public void Verbose(string messageTemplate)
        {
            _log.Verbose(messageTemplate);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            _log.Verbose(messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate)
        {
            _log.Warning(messageTemplate);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _log.Warning(messageTemplate, propertyValues);
        }

        public void Exception(Exception exception,string messageTemplate)
        {
            _log.Error(exception, messageTemplate);
        }

        public void Exception(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Error(exception, messageTemplate,propertyValues);
        }
    }
}
