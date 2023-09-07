namespace LoggingUsingNLog.Controllers;
using LoggingUsingNLog.NLog;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TestNLog : ControllerBase
{
    private ICustomLogger _customLogger;

    public TestNLog(ICustomLogger customLogger) => _customLogger = customLogger;

    [HttpGet]
    public IActionResult Log()
    {
        var log = new Log
        {
            Id = 0,
            Logger = nameof(Log),
            CreatedAt = DateTime.Now,
            FnParameter = "param 123",
            Level = CustomLogLevel.Error.ToString(),
            Message = "Log message",
            Exception = "Exception message",
            StackTrace = "Stack trace here",
            Url = "HTTP://djnbvlsdnvlws.go",
            Action = nameof(Log),
            Controller = nameof(TestNLog),
            UserId = "ID=79"
        };
        _customLogger.Log(log);
        return Ok();
    }
}
