using System;
using System.Collections.Generic;

namespace ML.Utils.Excel
{
    public enum ExcelVersion : short
    {
        CSV = 0,

        Excel2003,

        Excel2007,

        Excel2010
    }

    public class ExcelSheetInfo
    {
        public string Name { get; set; }

        public List<string> Columns { get; set; } = new List<string>();

        public Exception Exception { get; set; }
    }
}
