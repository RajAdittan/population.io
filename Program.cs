using System;
using Serilog;

namespace Population.IO
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger @log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Config.InOutConfig @cfg = new Config.InOutConfig
            {
                InputExcelFile = @"D:\usr\rajamohan\dotnet\population.io\TestData.xlsx",
                OutputSqlLiteDbFile = @"D:\usr\rajamohan\dotnet\population\population.db"
            };

            @log.Information("population.io");
            @log.Information(@cfg.ToString());
            @log.Information("reading specified excel");
            IInputReader inputReader = new ExcelReader(@cfg, @log);
            inputReader.BeginRead();

            PopulationContext context = new PopulationContext(@cfg, @log);
            @log.Information("read estimates");
            ICommandReader<Estimate> estimatesReader = inputReader.GetEstimateReader();
            while (estimatesReader.MoveNext())
            {
                @log.Information(estimatesReader.Current.ToString());
                context.Estimates.Add(estimatesReader.Current);
            }
            @log.Information("read actuals");
            ICommandReader<Actual> actualReader = inputReader.GetActualReader();
            while (actualReader.MoveNext())
            {
                @log.Information(actualReader.Current.ToString());
                context.Actuals.Add(actualReader.Current);
            }
            @log.Information("end reading");
            inputReader.EndRead();

            @log.Information("save estimates and actuals");
            context.SaveChanges();
            @log.Information("end.");
        }
    }

}

