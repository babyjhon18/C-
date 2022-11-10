using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ictweb5.ViewModels;
using ICTWebAPIEnd.ProxyDataRepository;
using ICTWebAPIEND.Models.CounterArchiveTypeModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPICounterItemCurrentDataRepository : BaseICTDataRepositoryClass, IAPIItemCurrentDataRepository
    {
        public ICTAPICounterItemCurrentDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            counter.ID = Convert.ToInt32(Params["counterID"]);
            if (repository.User.HasAccess(user, counter))
            {
                var currentData = repository.Counter.Current.View(counter) as ReportViewClass;
                ListDataPoint dataPoint = new ListDataPoint();
                DataPointClass timePoint = new DataPointClass();
                foreach (DataRow row in currentData.Data.Rows)
                {
                    foreach (DataColumn column in currentData.Data.Columns)
                    {
                        if (column.ColumnName == "TimeStamp")
                            timePoint.TimeStamp = (DateTime)row[column.ColumnName];
                        if (column.ColumnName == "ArchiveType")
                            dataPoint.ArchiveType = (ArchiveType)row[column.ColumnName];
                        if (!currentData.SkipColumns.Contains(column.ColumnName) &&
                            column.ColumnName != "TimeStamp")
                        {
                            FieldsClass field = new FieldsClass();
                            field.Name = column.ColumnName;
                            field.Value = row[column.ColumnName].ToString();
                            timePoint.Fields.Add(field);
                        }
                    }

                }
                dataPoint.Points.Add(timePoint);
                return dataPoint;
            }
            return null;
        }

        public object View<T>(T DataItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }
    }
}
