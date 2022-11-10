using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICTWebAPIEnd.ProxyDataRepository.Entities
{
    public class ICTAPILocationItemDataRepository : APICRUDDataRepositoryItem
    {
        public ICTAPILocationItemDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public override bool Create<T>(T dataItem, UserAccountClass user)
        {
            LocationClass location = dataItem as LocationClass;
            return repository.Location.Create(location);
        }

        public override bool Update<T>(T dataItem, UserAccountClass user)
        {
            LocationClass location = dataItem as LocationClass;
            if (repository.User.HasAccess(user, location) &&
                repository.User.HasAccess(user, new RegionClass() { ID = location.Region.ID }))
                return repository.Location.Update(location);
            return false;
        }

        public override bool Delete<T>(T dataItem, UserAccountClass user)
        {
            LocationClass location = dataItem as LocationClass;
            if (repository.User.HasAccess(user, location))
                return repository.Location.Delete(location);
            return false;
        }

        public override object View<T>(T dataItem) => repository.Location.View(dataItem);

        public override IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            var locations = repository.Common.Locations(user)
                .Select(l => new
                {
                    ID = l.ID,
                    Name = l.Name,
                    region = new
                    {
                        ID = l.Region.ID,
                        Name = l.Region.Name
                    }
                });
            if (Params.ContainsKey("regionID"))
                locations = locations.Where(l => l.region.ID == Convert.ToInt32(Params["regionID"]));
            if (Params.ContainsKey("locationID"))
                locations = locations.Where(l => l.ID == Convert.ToInt32(Params["locationID"]));
            return locations;
        }
    }
}
