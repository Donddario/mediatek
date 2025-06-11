using Serilog;
using System;
using System.IO;

namespace MediaTekDocuments.manager
{
    public static class Logs
    {
        /// <summary>
        /// Permet l'initialisation des logs
        /// </summary>
        public static void Initialize()
        {
            string logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MediaTekDocuments", "logs");
            Directory.CreateDirectory(logDir);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(logDir, "log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Logs OK.");
        }
    }
}