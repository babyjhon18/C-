using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICTWebAPIEnd.ProxyDataRepository.Entities
{
    public class ICTAPIObjectItemDataRepository : APICRUDDataRepositoryItem, IAPIObjectItemDataRepository
    {
        public ICTAPIObjectItemDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
            Current = new ICTAPIObjectItemLastDataRepository(Repository);
        }

        public IAPIItemCurrentDataRepository Current { get; }

        public override bool Create<T>(T dataItem, UserAccountClass user)
        {
            CommonObjectClass Object = dataItem as CommonObjectClass;
            if (repository.User.HasAccess(user, new LocationClass() { ID = Object.Location.ID }))
                return repository.Object.Create(Object);
            return false;
        }

        public override bool Update<T>(T dataItem, UserAccountClass user)
        {
            CommonObjectClass Object = dataItem as CommonObjectClass;
            if (repository.User.HasAccess(user, Object) &&
                repository.User.HasAccess(user, new LocationClass() { ID = Object.Location.ID }))
                return repository.Object.Update(Object);
            return false;
        }

        public override bool Delete<T>(T dataItem, UserAccountClass user)
        {
            CommonObjectClass Object = dataItem as CommonObjectClass;
            if (repository.User.HasAccess(user, Object))
                return repository.Object.Delete(Object);
            return false;
        }

        public override bool Clone<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            CommonObjectClass Object = dataItem as CommonObjectClass;
            if (repository.User.HasAccess(user, Object))
                return repository.Object.Clone(Object, Convert.ToInt32(Params["count"]),
                    (ictweb5.Domain.CloneType)Convert.ToInt32(Params["cloneType"]),
                    Params["cloneOptions"].ToString());
            return false;
        }

        public override object View<T>(T dataItem) => repository.Object.View(dataItem);

        public override IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            var objects = repository.Common.ObjectTree(user)
                .SelectMany(r => r.Locations
                .SelectMany(l => l.Objects, (l, o) => new
                {
                    ID = o.ID,
                    Name = o.Name,
                    Active = o.Active,
                    Description = o.Description,
                    LastRequestDate = o.LastRequestDate,
                    Device = new
                    {
                        ID = o.Device.ID,
                        RTU = o.Device.RTU,
                    },
                    LastRequestStatus = new
                    {
                        Status = o.LastRequestStatus.Status,
                        Code = o.LastRequestStatus.StatusCode,
                    },
                    Location = new
                    {
                        ID = l.ID,
                        Name = l.Name,
                        region = new
                        {
                            ID = r.ID,
                            Name = r.Name
                        }
                    }
                }));
            if (Convert.ToInt32(Params["objectID"]) == -1)
            {
                var obj = new List<object>();
                obj.Add(repository.Object.View(new CommonObjectClass() { ID = -1 }, user));
                return obj;
            }
            if (Params.ContainsKey("regionID"))
                objects = objects.Where(o => o.Location.region.ID == Convert.ToInt32(Params["regionID"]));
            if (Params.ContainsKey("locationID"))
                objects = objects.Where(o => o.Location.ID == Convert.ToInt32(Params["locationID"]));
            if (Params.ContainsKey("objectID"))
            {
                objects = objects.Where(o => o.ID == Convert.ToInt32(Params["objectID"]));
                if (objects.Count() != 0)
                {
                    var obj = new List<object>();
                    obj.Add(repository.Object.View(new CommonObjectClass() { ID = Convert.ToInt32(Params["objectID"]) }, user));
                    return obj;
                }
            }
            return objects;
        }
    }
}
