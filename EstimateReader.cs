using System;
using System.Text;
using OfficeOpenXml;
using Serilog;

namespace Population.IO
{
    public class EstimateReader : ICommandReader<Estimate>
    {
        private int _rowId = 1;
        private readonly ExcelWorksheet _estimateWorksheet;
        private ILogger Logger { get; }

        public EstimateReader(ExcelWorksheet estimateWorksheet, ILogger logger)
        {
            _estimateWorksheet = estimateWorksheet;
            Logger = logger;
        }

        public Estimate Current { get; private set; }
        public bool MoveNext()
        {
            if (_estimateWorksheet != null)
            {
                try
                {
                    if (_rowId == 1)
                    {
                        ExcelRow headerRow = _estimateWorksheet.Row(_rowId);
                        if (headerRow == null)
                        {
                            Logger.Error("Estimates Worksheet has no rows");
                            return false;
                        }

                        Logger.Information("Reading Estimates ...");
                    }

                    Current = new Estimate();

                    ++_rowId;
                    ExcelRow dataRow = _estimateWorksheet.Row(_rowId);
                    if (dataRow == null)
                    {
                        Logger.Warning("Finished reading Estimates worksheet");
                        return false;
                    }

                    bool sayTrue = false;
                    if (_estimateWorksheet.Cells[_rowId, 1].Value != null)
                    {
                        Current.State = Convert.ToInt32(_estimateWorksheet.Cells[_rowId, 1].Value);
                        sayTrue = true;
                    }

                    if (_estimateWorksheet.Cells[_rowId, 2].Value != null)
                    {
                        Current.Districts = Convert.ToInt32(_estimateWorksheet.Cells[_rowId, 2].Value);
                    }

                    if (_estimateWorksheet.Cells[_rowId, 3].Value != null)
                    {
                        Current.EstimatesPopulation = Convert.ToInt32(_estimateWorksheet.Cells[_rowId, 3].Value);
                    }

                    if (_estimateWorksheet.Cells[_rowId, 4].Value != null)
                    {
                        Current.EstimateHoseholds = Convert.ToInt32(_estimateWorksheet.Cells[_rowId, 4].Value);
                    }
                    return sayTrue;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Logger.Warning("RowId out of range");
                }
            }
            Logger.Warning("Finished reading Estimates");
            return false;
        }
    }
}