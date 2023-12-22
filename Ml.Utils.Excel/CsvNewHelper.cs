using ML.Utils.Excel.CsvNew;
using System;
using System.Collections.Generic;
using System.IO;

namespace ML.Utils.Excel
{
    public static class CsvNewHelper
    {
        public static IEnumerable<T> ToEnumerable<T>(string csvContent, Func<string[], T> processData, bool hasHeader = false)
        {
            using (TextReader textReader = new StringReader(csvContent))
            {
                //return ToEnumerable(textReader, processData, hasHeader);
                // trung note: code reader must be here to avoid reader closed error.
                using (var reader = new CsvReader(textReader, hasHeader))
                {
                    reader.SkipEmptyLines = true;

                    //rows
                    //int colsCount = reader.FieldCount;

                    while (reader.ReadNextRecord())
                    {
                        var dataFields = new string[reader.FieldCount];
                        reader.CopyCurrentRecordTo(dataFields);
                        yield return processData(dataFields);
                    }

                    //using var re = reader.GetEnumerator();
                    //while (re.MoveNext())
                    //{
                    //    yield return processData(re.Current);
                    //}
                }
            }
        }

        //public static IEnumerable<T> ToEnumerable<T>(Stream stream, Func<string[], T> processData, bool skipHeaderRow = false)
        //{
        //    using (TextReader textReader = new StreamReader(stream))
        //    {
        //        return ToEnumerable(textReader, processData, skipHeaderRow);
        //    }
        //}

        //public static IEnumerable<T> ToEnumerable<T>(TextReader textReader, Func<string[], T> processData, bool skipHeaderRow = false)
        //{
        //    using (var reader = new CsvReader(textReader, true))
        //    {
        //        if (skipHeaderRow)
        //            reader.GetFieldHeaders();

        //        //rows
        //        //int colsCount = reader.FieldCount;

        //        while (reader.ReadNextRecord())
        //        {
        //            var dataFields = new string[reader.FieldCount];
        //            reader.CopyCurrentRecordTo(dataFields);
        //            yield return processData(dataFields);
        //        }
        //    }
        //}
    }
}
