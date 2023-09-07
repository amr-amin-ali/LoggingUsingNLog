#pragma warning disable CS8604 // Possible null reference argument.
namespace LoggingUsingNLog.NLog;
using System;
using System.Text;
using System.Threading.Tasks;

using global::NLog;

using Microsoft.Extensions.Configuration;

public class CustomLogger : ICustomLogger
{
    private readonly Logger _logger;
    private LoggerConfigurations LoggerConfigurations { get; set; } = new LoggerConfigurations();

    public CustomLogger(IConfiguration configuration)
    {
        _logger = LogManager.GetCurrentClassLogger();

        configuration.GetSection("LoggerConfigurations").Bind(LoggerConfigurations);

    }

    public void Log(Log log)
    {
        if ( LoggerConfigurations.AllowLogging )
        {
            var message = BuildFileLoggingMessage(log);

            if ( LoggerConfigurations.AllowedLevels.Contains(CustomLogLevel.Trace.ToString()) && log.Level == CustomLogLevel.Trace.ToString() )
            {
                _logger.Trace(message);
            }

            if ( LoggerConfigurations.AllowedLevels.Contains(CustomLogLevel.Debug.ToString()) && log.Level == CustomLogLevel.Debug.ToString() )
            {
                _logger.Debug(message);
            }

            if ( LoggerConfigurations.AllowedLevels.Contains(CustomLogLevel.Info.ToString()) && log.Level == CustomLogLevel.Info.ToString() )
            {
                _logger.Info(message);
            }

            if ( LoggerConfigurations.AllowedLevels.Contains(CustomLogLevel.Error.ToString()) && log.Level == CustomLogLevel.Error.ToString() )
            {
                LogEventInfo logEventWithNoException = BuildDatabaseLoggingMessage(log: log);
                _logger.Error(logEventWithNoException);
                try
                {
                    throw new Exception("Exception was thrown");
                }
                catch ( Exception e )
                {
                    LogEventInfo logEvent = BuildDatabaseLoggingMessage(log: log, exception: e);
                    _logger.Error(logEvent);
                }
            }

            if ( LoggerConfigurations.AllowedLevels.Contains(CustomLogLevel.Warn.ToString()) && log.Level == CustomLogLevel.Warn.ToString() )
            {
                _logger.Warn(message);
            }

            if ( LoggerConfigurations.AllowedLevels.Contains(CustomLogLevel.Fatal.ToString()) && log.Level == CustomLogLevel.Fatal.ToString() )
            {
                _logger.Fatal(message);
            }
        }
    }
    public Task LogAsync(Log log)
    {
        throw new NotImplementedException();
    }

    private LogEventInfo BuildDatabaseLoggingMessage(Log log, Exception? exception = null)
    {
        LogEventInfo logEvent = new LogEventInfo(LogLevel.Error, "dataBase", "Test_message");
        logEvent.Properties["UserId"] = log.UserId;
        logEvent.Properties["Controller"] = log.Controller;
        logEvent.Properties["Action"] = log.Action;
        logEvent.Properties["Message"] = log.Message;
        logEvent.Properties["FnParameter"] = log.FnParameter;
        logEvent.Properties["CreatedAt"] = log.CreatedAt;
        if ( exception != null )
        {
            logEvent.Properties["Exception"] = exception.Message;
        }

        return logEvent;
    }
    private string BuildFileLoggingMessage(Log log)
    {
        var message = new StringBuilder();

        message.Append(log.Logger?.Trim().Length > 0 ? $"Logger: {log.Logger}, " : null);

        message.Append(log.FnParameter?.Trim().Length > 0 ? $"FnParameter: {log.FnParameter}, " : null);

        message.Append(log.Message?.Trim().Length > 0 ? $"Message: {log.Message}, " : null);

        return message.ToString();

    }
}
