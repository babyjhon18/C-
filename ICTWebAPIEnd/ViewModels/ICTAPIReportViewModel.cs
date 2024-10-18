using ictweb5.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace ICTWebAPIEnd.ViewModels
{
    public class ICTAPIReportViewModel
    {
        public List<List<Object>> _Rows = new List<List<Object>>();

        public ICTAPIReportViewModel(ReportViewClass report)
        {
            List<List<Object>> Rows = new List<List<Object>>();
            foreach (DataRow row in report.Data.Rows)
            {
                List<Object> rowData = new List<Object>();
                foreach (DataColumn column in report.Data.Columns)
                {
                    if (column.ColumnName == "ObjectID" || column.ColumnName == "CounterID" || !report.SkipColumns.Contains(column.ColumnName))
                    {

                        rowData.Add(new
                        {
                            Name = column.ColumnName,
                            Value = row[column.ColumnName].ToString(),
                            Description = report.Columns.GetValueOrDefault(column.ColumnName),
                        });
                    }
                }
                Rows.Add(rowData);
            }
            _Rows = Rows;
        }
    }
}
