using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ictweb5.ViewModels;
using ICTWebAPIEnd.Models.CounterLastArchiveTypeModel;
using ICTWebAPIEnd.ProxyDataRepository;
using ICTWebAPIEND.Models.CounterArchiveTypeModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPIObjectItemLastDataRepository : BaseICTDataRepositoryClass, IAPIItemCurrentDataRepository
    {
        public ICTAPIObjectItemLastDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }
        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            BaseObjectClass obj = dataItem as BaseObjectClass;
            obj.ID = Convert.ToInt32(Params["objectID"]);
            if (repository.User.HasAccess(user, obj))
            {
                var lastData = repository.Object.Current.View(obj)
                    as ReportViewClass;
                LastDataByObject lastDataByObject = new LastDataByObject()
                {
                    ObjectID = Convert.ToInt32(Params["objectID"]),
                    ListOfCounters = new List<CounterListDataPoint>()
                };
                CounterListDataPoint lastDataByCounter = new CounterListDataPoint();
                ListDataPoint listDataPoint = new ListDataPoint();
                DataPointClass timePoint = new DataPointClass();
                FieldsClass fields = new FieldsClass();
                foreach (DataRow Row in lastData.Data.AsEnumerable().Reverse())
                {
                    if (Row[0].ToString() == "CounterID")
                    {
                        lastDataByCounter.CounterID = Convert.ToInt32(Row[1]);
                        lastDataByObject.ListOfCounters.Add(lastDataByCounter);
                        lastDataByObject.ListOfCounters.Reverse();
                        lastDataByCounter = new CounterListDataPoint();
                    }
                    if (Row[0].ToString() == "Дата и время показаний")
                    {
                        timePoint.TimeStamp = Convert.ToDateTime(Row[1]);
                        timePoint.Fields.Reverse();
                        listDataPoint.Points.Add(timePoint);
                        listDataPoint.Points.Reverse();
                        timePoint = new DataPointClass();
                    }
                    if (Row[0].ToString() == "ArchiveType")
                    {
                        listDataPoint.ArchiveType = (ArchiveType)Convert.ToInt32(Row[1]);
                        lastDataByCounter.Data.Add(listDataPoint);
                        lastDataByCounter.Data = lastDataByCounter.Data.OrderBy(a => a.ArchiveType).ToList();

                        listDataPoint = new ListDataPoint();
                    }
                    else if (!lastData.SkipColumns.Contains(Row[0].ToString()) &&
                        !lastData.Columns.ContainsValue(Row[0].ToString()))
                    {
                        fields = new FieldsClass();
                        fields.Name = Row[0].ToString();
                        fields.Value = Row[1].ToString();
                        if (fields.Value != "")
                            timePoint.Fields.Add(fields);
                    }
                }
                if (Params.ContainsKey("counterID"))
                {
                    lastDataByCounter =
                        lastDataByObject.ListOfCounters.Where(c => c.CounterID == Convert.ToInt32(Params["counterID"])).FirstOrDefault();
                    lastDataByObject.ListOfCounters.Clear();
                    lastDataByObject.ListOfCounters.Add(lastDataByCounter);
                    if (lastDataByCounter == null)
                        return null;
                }
                return lastDataByObject;
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
