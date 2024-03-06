using ProductApp.Infrastructure.Logging.Interfaces;

namespace ProductApp.Infrastructure.Logging
{
    public class LoggingService: ILoggingService
    {
        private readonly string _logFilePath;

        public LoggingService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            try
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.UtcNow} - {message}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }
    }
}
