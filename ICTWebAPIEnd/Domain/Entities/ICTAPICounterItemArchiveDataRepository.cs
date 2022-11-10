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
    public class ICTAPICounterItemArchiveDataRepository : BaseICTDataRepositoryClass, IAPIItemArchiveDataRepository
    {
        public ICTAPICounterItemArchiveDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public bool Delete(string UIDs)
        {
            throw new NotImplementedException();
        }

        public object View<T>(T DataItem, ArchiveType ArchiveType, DateTime DateFrom, DateTime DateTo)
        {
            throw new NotImplementedException();
        }


        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            counter.ID = Convert.ToInt32(Params["counterID"]);
            if (repository.User.HasAccess(user, counter))
            {
                try
                {
                    var archiveData = repository.Counter.Archive.View(counter, (ArchiveType)Convert.ToInt32(Params["archiveType"]),
                        DateTime.ParseExact(Params["dateFrom"], "yyyy-MM-dd'T'HH:mm:ss", null),
                        DateTime.ParseExact(Params["toDate"], "yyyy-MM-dd'T'HH:mm:ss", null)) as ReportViewClass;
                    CounterListDataPoint counterDataList = new CounterListDataPoint();
                    counterDataList.CounterID = counter.ID;
                    foreach (DataRow row in archiveData.Data.Rows)
                    {
                        DataPointClass timePoint = new DataPointClass();
                        ListDataPoint dataPoint = new ListDataPoint();
                        foreach (DataColumn column in archiveData.Data.Columns)
                        {
                            if (column.ColumnName == "TimeStamp")
                                timePoint.TimeStamp = (DateTime)row[column.ColumnName];
                            if (column.ColumnName == "ArchiveType")
                                dataPoint.ArchiveType = (ArchiveType)row[column.ColumnName];
                            if (!archiveData.SkipColumns.Contains(column.ColumnName) &&
                                column.ColumnName != "TimeStamp")
                            {
                                FieldsClass field = new FieldsClass();
                                field.Name = column.ColumnName;
                                field.Value = row[column.ColumnName].ToString();
                                timePoint.Fields.Add(field);
                            }

                        }
                        dataPoint.Points.Add(timePoint);
                        counterDataList.Data.Add(dataPoint);
                    }
                    return counterDataList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return default(Object);
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }
    }
}
