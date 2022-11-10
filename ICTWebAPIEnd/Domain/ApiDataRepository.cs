using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.Domain.Entities;
using ICTWebAPIEnd.ProxyDataRepository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ICTWebAPIEnd.ProxyDataRepository
{
    public class ApiDataRepository : IAPIDataRepository
    {
        protected IICTDataRepository Repository;
        public ApiDataRepository(IConfiguration configuration)
        {
            this.Repository = new SQLICTDataRepository(configuration["ictdb"].ToString(), null);
            this.Common = new CommonItemSQLDataRepositoryClass(configuration["ictdb"].ToString(), null);
            this.System = new ICTAPISystemRoutinesItemDataRepository(Repository);
            this.Region = new ICTAPIRegionItemDataRepository(Repository);
            this.User = new ICTAPIUserItemDataRepository(Repository);
            this.Object = new ICTAPIObjectItemDataRepository(Repository);
            this.Location = new ICTAPILocationItemDataRepository(Repository);
            this.Counter = new ICTAPICounterItemDataRepository(Repository);
            this.Device = new ICTAPIDeviceDataRepository(Repository);
            this.Report = new ICTAPISQLReportDataItem(Repository);
            this.Consumer = new ICTAPIConsumerItemDataRepository(Repository);
            this.ObjectTree = new ICTAPIObjectTreeDataRepository(Repository);
            this.ScheduleEngine = new ICTAPIScheduleDataEngineDataRepository(Repository);
            this.Tele = new ICTAPITeleItemDataRepository(Repository);
        }
        public IAPIScheduleDataEngine ScheduleEngine { get; }
        public IAPIDataRepositoryItem Region { get; }
        public IAPIBaseDataRepositoryItem ObjectTree { get; }
        public IAuthItemDataRepository User { get; }
        public IAPIObjectItemDataRepository Object { get; }
        public IAPIDataRepositoryItem Location { get; }
        public IAPIITemplatesItemDataRepository Counter { get; }
        public ICommonItemDataRepository Common { get; }
        public IAPIDataRepositoryItem Device { get; }
        public IAPIItemReportDataRepository Report { get; }
        public IAPIBaseDataRepositoryItem Consumer { get; }
        public IAPISystemRoutines System { get; }
        public IAPITele Tele { get; }
    }

    public class APICRUDDataRepositoryItem : IAPIDataRepositoryItem
    {
        protected IICTDataRepository repository;
        public APICRUDDataRepositoryItem(IICTDataRepository Repository)
        {
            repository = Repository;
        }

        public virtual object View<T>(T dataItem)
        {
            throw new NotImplementedException();
        }

        public virtual Object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            return dataItem;
        }

        public virtual List<T> ViewAll<T>(List<T> List)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            return default;
        }
        public bool Create<T>(T dataItem)
        {
            throw new NotImplementedException();
        }

        public virtual bool Create<T>(T dataItem, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public bool Update<T>(T dataItem)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update<T>(T dataItem, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>(T dataItem)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete<T>(T dataItem, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public bool Clone<T>(T dataItem, int Count, CloneType Type = CloneType.ctDefault, string CloneOption = "")
        {
            throw new NotImplementedException();
        }

        public virtual bool Clone<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public virtual bool Clone<T>(T dataItem, int Count, UserAccountClass user, CloneType Type = CloneType.ctDefault, string CloneOption = "")
        {
            throw new NotImplementedException();
        }

        public virtual string Type<T>(T dataItem)
        {
            throw new NotImplementedException();
        }

    }
}
