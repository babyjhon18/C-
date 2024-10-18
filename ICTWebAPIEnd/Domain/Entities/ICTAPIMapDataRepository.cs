using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ictweb5.ViewModels;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPIMapDataRepository : BaseItemICTDataRepositoryClass, IAPIMap
    {
        public ICTAPIMapDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            if (Convert.ToInt32(Params["objectID"]) != 0)
            {
                if(repository.User.HasAccess(user, new CommonObjectClass() { ID = Convert.ToInt32(Params["objectID"]) }))
                {
                    var obj = repository.Object.View(new CommonObjectClass() 
                        { ID = Convert.ToInt32(Params["objectID"]) }, user) as ObjectViewClass;
                    var mapObject = repository.Map.View(new BaseObjectClass() { ID = Convert.ToInt32(Params["objectID"]) });
                    return new
                    {
                        id = obj.Object.ID,
                        name = obj.Object.Name,
                        xy = new
                        {
                            x = mapObject.X,
                            y = mapObject.Y
                        }
                    };
                }
            }
            if (Convert.ToInt32(Params["locationID"]) != 0)
                return repository.Map.View(new LocationClass() { ID = Convert.ToInt32(Params["locationID"]) });
            if (Convert.ToInt32(Params["regionID"]) != 0)
                return repository.Map.View(user, new RegionClass() { ID = Convert.ToInt32(Params["regionID"]) });
            if(Params.Count == 0)
            {
                List<Object> MapObjects = new List<Object>();
                var ListOfObjects = repository.Map.Refresh(DateTime.MinValue, user);
                foreach(var _obj in ListOfObjects)
                {
                    MapObjects.Add(new
                    {
                        id = _obj.ID,
                        name = _obj.Name,
                        xy = new 
                        {
                            x = _obj.XY.X,
                            y = _obj.XY.Y
                        }
                    });
                }
                return MapObjects;
            }
            return default;
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }
    }
}