using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Kulba.LoadConfig.ConsoleUI
{
    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private IOptions<AppConfigInfo> _appConfigInfo;

        public Application(ILogger<Application> logger, IOptions<AppConfigInfo> appConfigInfo)
        {
            _logger = logger;
            _appConfigInfo = appConfigInfo;
        }

        public void Run()
        {
            _logger.LogInformation("Application started at {dateTime}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            _logger.LogInformation("Successfully loaded options from the appsettings file");
            _logger.LogInformation(_appConfigInfo.Value.Name);
            _logger.LogInformation(_appConfigInfo.Value.Version);
            _logger.LogInformation("Application Ended at {dateTime}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

        }
    }
}
