using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ICTWebAPIEnd.ProxyDataRepository
{
    public interface IAPIDataRepository
    {
        public IAPIScheduleDataEngine ScheduleEngine { get; }
        public ICommonItemDataRepository Common { get; }
        public IAPIDataRepositoryItem Region { get; }
        public IAuthItemDataRepository User { get; }
        public IAPIBaseDataRepositoryItem ObjectTree { get; }
        public IAPIObjectItemDataRepository Object { get; }
        public IAPIDataRepositoryItem Location { get; }
        public IAPIITemplatesItemDataRepository Counter { get; }
        public IAPIDataRepositoryItem Device { get; }
        public IAPIItemReportDataRepository Report { get; }
        public IAPIBaseDataRepositoryItem Consumer { get; }
        public IAPISystemRoutines System { get; }
        public IAPITele Tele { get; }
    }

    public interface IAPITele
    {
        public IAPITeleObject Object { get; }
    }

    public interface IAPITeleObject 
    {
        public IAPITeleSignal Signal { get; }
        public IAPITeleReport Alarm { get; }
        public Object View(EntityClass Object, UserAccountClass user);
    }
    public interface IAPITeleSignal : IAPIBaseDataRepositoryItem
    {
        object Value(EntityClass Signal, string DateFrom, string DateTo, UserAccountClass user);
        object ViewAll(EntityClass Object, UserAccountClass user);
    }
    public interface IAPITeleReport
    {
        Object View(EntityClass Object, string DateFrom, string DateTo, UserAccountClass user);
    }
    public interface IAPIScheduleDataEngine : IScheduleDataEngine
    {
        public ScheduleClass AddSchedule(GeneralScheduleClass GeneralScheduleClass, UserAccountClass User);
    }

    public interface IAPISystemRoutines
    {
        IEnumerable<object> Search<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user);
    }
    public interface IAPIObjectItemDataRepository : IAPIDataRepositoryItem
    {
        public IAPIItemCurrentDataRepository Current { get; }
    }

    public interface IAPIITemplatesItemDataRepository : ITemplatesItemDataRepository, IAPIDataRepositoryItem
    {
        public IAPIItemCurrentDataRepository Current { get; }
        public IAPIItemArchiveDataRepository Archive { get; }
    }

    public interface IAPIItemLastDataRepository : IAPIBaseDataRepositoryItem
    {
    }

    public interface IAPIItemCurrentDataRepository : IItemCurrentDataRepository, IAPIBaseDataRepositoryItem
    {
    }

    public interface IAPIItemArchiveDataRepository : IItemArchiveDataRepository, IAPIBaseDataRepositoryItem
    {
    }

    public interface IAPIItemReportDataRepository : IAPIBaseDataRepositoryItem
    {
        Object View(String ReportClassName, IQueryCollection Params, UserAccountClass user);
    }

    public interface IAPIDataRepositoryItem : ICRUDItemDataRepository, IAPIBaseDataRepositoryItem
    {
        bool Create<T>(T dataItem, UserAccountClass user);

        bool Update<T>(T dataItem, UserAccountClass user);

        bool Delete<T>(T dataItem, UserAccountClass user);

        bool Clone<T>(T dataItem, IQueryCollection Params, UserAccountClass user);
    }

    public interface IAPIBaseDataRepositoryItem
    {
        IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user);

        Object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user);
    }
}
