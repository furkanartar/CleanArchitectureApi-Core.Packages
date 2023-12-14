using Core.CrossCuttingConcerns.SeriLog.ConfigurationModels;
using Core.CrossCuttingConcerns.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Core.CrossCuttingConcerns.SeriLog.Loggers;

public class FileLogger : LoggerServiceBase
{
    private readonly IConfiguration _configuration;

    public FileLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        FileLogConfiguration logConfiguration =
            configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage); //SeriLogConfigurations içindeki FileLogConfiguration'ı al FileLogConfiguration'a bind et diyoruz

        string logfilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfiguration.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration().WriteTo.File(
            path: logfilePath, //dosyanın yolu
            rollingInterval: RollingInterval.Day, //gün bazlı log tut
            retainedFileCountLimit: null, //eski logları silme
            fileSizeLimitBytes: 5000000, //5mb'dan büyükse yeni dosya aç
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}" //log formatı
            ).CreateLogger();
    }
}
