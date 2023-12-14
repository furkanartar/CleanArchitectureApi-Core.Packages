using Core.CrossCuttingConcerns.SeriLog.ConfigurationModels;
using Core.CrossCuttingConcerns.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.SeriLog.Loggers;

public class MsSqlLogger : LoggerServiceBase
{
    public MsSqlLogger(IConfiguration configuration)
    {
        MsSqlConfiguration logConfiguration =
            configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage); //SeriLogConfigurations içindeki MsSqlConfiguration'ı al MsSqlConfiguration'a bind et diyoruz

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = logConfiguration.TableName,
            AutoCreateSqlTable = logConfiguration.AutoCreateSqlTable,
        };

        ColumnOptions columnOptions = new(); // Ms sql'de naming convention'a zaten uyduğumuz için column options kullanmıyoruz. Eğer postgre, oracle gibi bir db kullanıyorsak column options kullanırdık.

        global::Serilog.Core.Logger seriLogConfig = new LoggerConfiguration().WriteTo
            .MSSqlServer(connectionString: logConfiguration.ConnectionString, sinkOptions: sinkOptions, columnOptions: columnOptions)
            .CreateLogger();

        Logger = seriLogConfig;
    }
}
