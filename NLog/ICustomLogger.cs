namespace LoggingUsingNLog.NLog;
public interface ICustomLogger
{
    void Log(Log log);
    Task LogAsync(Log log);
}
