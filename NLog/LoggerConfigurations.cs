namespace LoggingUsingNLog.NLog;
public class LoggerConfigurations
{
    public bool AllowLogging { get; set; }
    public List<string> AllowedLevels { get; set; } = new List<string>();
}
