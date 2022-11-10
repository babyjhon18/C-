using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICTWebAPIEnd.ProxyDataRepository.Entities
{
    public class ICTAPIRegionItemDataRepository : APICRUDDataRepositoryItem
    {
        public ICTAPIRegionItemDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public override bool Create<T>(T dataItem, UserAccountClass user)
        {
            RegionClass region = dataItem as RegionClass;
            return repository.Region.Create(region);
        }

        public override bool Update<T>(T dataItem, UserAccountClass user)
        {
            RegionClass region = dataItem as RegionClass;
            if (repository.User.HasAccess(user, region))
                return repository.Region.Update(region);
            return false;
        }

        public override bool Delete<T>(T dataItem, UserAccountClass user)
        {
            RegionClass region = dataItem as RegionClass;
            if (repository.User.HasAccess(user, region))
                return repository.Region.Delete(region);
            return false;
        }

        public override object View<T>(T dataItem) => repository.Region.View(dataItem);

        public override IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            var regions = repository.Common.Regions(user)
                .Select(r => new
                {
                    ID = r.ID,
                    Name = r.Name
                });
            if (Params.ContainsKey("regionID"))
                regions = regions.Where(r => r.ID == Convert.ToInt32(Params["regionID"]));
            return regions;
        }
    }
}
