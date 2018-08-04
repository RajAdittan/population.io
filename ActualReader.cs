using System;
using System.Text;
using OfficeOpenXml;
using Serilog;
using Serilog.Formatting.Json;

namespace Population.IO
{
    public class ActualReader : ICommandReader<Actual>
    {
        private int _rowId = 1;
        private ILogger Logger { get; }
        private ExcelWorksheet ActualWorksheet { get; }

        public ActualReader(ExcelWorksheet actualWorksheet, ILogger logger)
        {
            ActualWorksheet = actualWorksheet;
            Logger = logger;
        }

        public Actual Current { get; private set; }

        public bool MoveNext()
        {
            if (ActualWorksheet != null)
            {
                try
                {
                    if (_rowId == 1)
                    {
                        ExcelRow headerRow = ActualWorksheet.Row(_rowId);
                        if (headerRow == null)
                        {
                            Logger.Error("Actuals Worksheet has no rows");
                            return false;
                        }
                        Logger.Information("Reading Actuals ...");
                    }

                    Current = new Actual();

                    ++_rowId;
                    ExcelRow dataRow = ActualWorksheet.Row(_rowId);
                    if (dataRow == null)
                    {
                        Logger.Warning("Finished reading Actuals worksheet");
                        return false;
                    }

                    bool sayTrue = false;
                    if(ActualWorksheet.Cells[_rowId, 1].Value != null)
                    {
                        Current.State = Convert.ToInt32(ActualWorksheet.Cells[_rowId, 1].Value);
                        sayTrue = true;
                    }

                    if (ActualWorksheet.Cells[_rowId, 2].Value != null)
                    {
                        Current.ActualPopulation = Convert.ToDouble(ActualWorksheet.Cells[_rowId, 2].Value);
                    }

                    if (ActualWorksheet.Cells[_rowId, 3].Value != null)
                    {
                        Current.ActualHouseholds = Convert.ToDouble(ActualWorksheet.Cells[_rowId, 3].Value);
                    }

                    return sayTrue;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Logger.Warning("RowId out of range");
                }
            }

            Logger.Warning("Finished reading Actuals worksheet");
            return false;
        }

    }
}