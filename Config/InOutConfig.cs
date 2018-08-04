using System;
using System.Collections.Generic;
using System.Text;

namespace Population.IO.Config
{
    public class InOutConfig
    {
        public string InputExcelFile { get; set; }
        public string OutputSqlLiteDbFile { get; set; }

        public override string ToString()
        {
            return String.Format("InOutConfig: {{" + Environment.NewLine +
                                 "\"InputExcelFile\":" + "\""+InputExcelFile + "\"" + Environment.NewLine +
                                 "\"OutputSqlLiteDbFile\":" + "\"" + OutputSqlLiteDbFile + "\"" + Environment.NewLine +
                                 "}}");
        }
    }
}
