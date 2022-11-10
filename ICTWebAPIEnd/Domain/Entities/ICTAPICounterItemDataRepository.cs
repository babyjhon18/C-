using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace ICTWebAPIEnd.ProxyDataRepository.Entities
{
    public class ICTAPICounterItemDataRepository : APICRUDDataRepositoryItem, IAPIITemplatesItemDataRepository
    {
        public ICTAPICounterItemDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
            Current = new ICTAPICounterItemCurrentDataRepository(Repository);
            Archive = new ICTAPICounterItemArchiveDataRepository(Repository);
        }

        public ITemplates Templates { get; set; }
        public IAPIItemCurrentDataRepository Current { get; }
        public IAPIItemArchiveDataRepository Archive { get; set; }
        public IItemFieldDataRepository Field { get; set; }
        public IPassport Passport { get; set; }
        IItemCurrentDataRepository IDataItemDataRepository.Current { get; set; }
        IItemArchiveDataRepository IDataItemDataRepository.Archive { get; set; }

        public IAPIItemLastDataRepository Last { get; set; }

        public override bool Create<T>(T dataItem, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            if (repository.User.HasAccess(user, new DeviceClass() { ID = counter.Device.ID }))
                return repository.Counter.Create(counter);
            return false;
        }

        public override bool Update<T>(T dataItem, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            if (repository.User.HasAccess(user, counter))
                return repository.Counter.Update(counter);
            return false;
        }

        public override bool Delete<T>(T dataItem, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            if (repository.User.HasAccess(user, counter))
                return repository.Counter.Delete(counter);
            return false;
        }

        public override bool Clone<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            if (repository.User.HasAccess(user, counter))
                return repository.Counter.Clone(counter, Convert.ToInt32(Params["count"]),
                    ictweb5.Domain.CloneType.ctDefault, "");
            return false;
        }

        public override object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            CounterClass counter = dataItem as CounterClass;
            counter.ID = Convert.ToInt32(Params["counterID"]);
            if (counter.ID == -1)
                return repository.Counter.View(counter);
            if (repository.User.HasAccess(user, counter))
                return repository.Counter.View(counter);
            return default(Object);
        }
    }
}
