namespace LoggingUsingNLog.NLog
{
    using System;

    public class Log
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Exception { get; set; }
        public string FnParameter { get; set; }
        public string Level { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
