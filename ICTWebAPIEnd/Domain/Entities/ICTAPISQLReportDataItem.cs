using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ictweb5.ViewModels;
using ICTWebAPIEnd.ProxyDataRepository;
using ICTWebAPIEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPISQLReportDataItem : BaseICTDataRepositoryClass, IAPIItemReportDataRepository
    {
        public ICTAPISQLReportDataItem(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public object View(String ReportClassName, IQueryCollection Params, UserAccountClass user)
        {
            List<RegionClass> regions;
            List<LocationClass> locations;
            List<BaseObjectClass> objects;
            if (Params["regionsID"].ToString() != String.Empty)
            {
                regions = (from el in Params["regionsID"].ToString().Split(',')
                           select new RegionClass() { ID = Convert.ToInt32(el) }).ToList();
            }
            else regions = new List<RegionClass>();

            if (Params["locationsID"].ToString() != String.Empty)
            {
                locations = (from el in Params["locationsID"].ToString().Split(',')
                             select new LocationClass() { ID = Convert.ToInt32(el) }).ToList();
            }
            else locations = new List<LocationClass>();

            if (Params["objectsID"].ToString() != String.Empty)
            {
                objects = (from el in Params["objectsID"].ToString().Split(',')
                           select new BaseObjectClass() { ID = Convert.ToInt32(el) }).ToList();
            }
            else objects = new List<BaseObjectClass>();

            try
            {
                ICTAPIReportViewModel reportViewModel = new ICTAPIReportViewModel(repository.ReportEngine
                        .Reports[ReportClassName]
                        .View(user, regions, locations, objects
                        , new ConsumerClass(), (ArchiveType)Convert.ToInt32(Params["archiveType"]) != ArchiveType.atCurrent,
                        (ArchiveType)Convert.ToInt32(Params["archiveType"]), (AccountingType)Convert.ToInt32(Params["accountingType"]),
                         DateTime.ParseExact(Params["dateFrom"], "yyyy-MM-dd'T'HH:mm:ss", null),
                         DateTime.ParseExact(Params["toDate"], "yyyy-MM-dd'T'HH:mm:ss", null)) as ReportViewClass);
                return reportViewModel._Rows;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }
    }
}
