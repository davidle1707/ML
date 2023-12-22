using System;
using System.Linq.Expressions;

namespace ML.Utils.Excel.Csv.Export
{
    /// <summary>
    /// Represents custom exportable column with a expression for the property name
    /// and a custom format string
    /// </summary>
    public class ExportableColumn<T>
    {
        public Expression<Func<T, Object>> Func { get; private set; }
        public String HeaderString { get; private set; }
        public String CustomFormatString { get; private set; }

        public ExportableColumn(
            Expression<Func<T, Object>> func,
            String headerString = "",
            String customFormatString = "")
        {
            this.Func = func;
            this.HeaderString = headerString;
            this.CustomFormatString = customFormatString;
        }
    }
}
