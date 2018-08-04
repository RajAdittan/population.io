using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OfficeOpenXml;
using Population.IO.Config;
using Serilog;

namespace Population.IO
{
    public class ExcelReader : IInputReader
    {
        private ExcelPackage _excelPackage;
        private ExcelWorksheet _estimateWorksheet;
        private ExcelWorksheet _actualWorksheet;
        private ExcelWorkbook _excelWorkbook;

        private ILogger Logger { get; }
        private string ExcelFile { get; }

        public ExcelReader(InOutConfig cfg, ILogger log)
        {
            ExcelFile = cfg.InputExcelFile;
            Logger = log;
        }


        public void BeginRead()
        {
            Logger.Information("BeginRead {0}", ExcelFile);
            if (File.Exists(ExcelFile))
            {
                Logger.Information("ExcelFile exist");
                _excelPackage = new ExcelPackage(new FileInfo(ExcelFile));
                Logger.Information("Package created");
                if (_excelPackage.Workbook == null)
                {
                    Logger.Error("No workbook found in the specified excel");
                    Environment.Exit(-2);
                }

                _excelWorkbook = _excelPackage.Workbook;

                if (_excelWorkbook.Worksheets == null || _excelWorkbook.Worksheets.Count <= 0)
                {
                    Logger.Error("No worksheets found in the specified excel");
                    Environment.Exit(-3);
                }

                _estimateWorksheet = _excelPackage.Workbook.Worksheets["Estimates"];
                _actualWorksheet = _excelPackage.Workbook.Worksheets["Actuals"];
                if (_estimateWorksheet == null)
                {
                    Logger.Warning("Estimates worksheet is not found!");
                }

                if (_actualWorksheet == null)
                {
                    Logger.Warning("Actuals worksheet is not found");
                }
                Logger.Information("Estimates, Actuals worksheets found, proceed to create command readers");
                return;
            }
            Logger.Error("Specified excel file not found");
            Environment.Exit(-1);
        }

        public void EndRead()
        {
            _estimateWorksheet.Dispose();
            _actualWorksheet.Dispose();
            _excelWorkbook.Dispose();
            _excelPackage.Dispose();
        }

        public ICommandReader<Estimate> GetEstimateReader()
        {   
            return new EstimateReader(_estimateWorksheet, Logger);
        }

        public ICommandReader<Actual> GetActualReader()
        {
            return new ActualReader(_actualWorksheet, Logger);
        }
    }
}
