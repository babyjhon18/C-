using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPIObjectTreeDataRepository : BaseICTDataRepositoryClass, IAPIBaseDataRepositoryItem
    {
        public ICTAPIObjectTreeDataRepository(IICTDataRepository Repository)
            : base(Repository)
        {
        }

        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            List<Object> Regions = new List<Object>();
            var objectTree = repository.Common.ObjectTree(user);
            foreach (var region in objectTree)
            {
                var Locations = new List<Object>();
                foreach (var location in region.Locations)
                {
                    var Objects = new List<Object>();
                    foreach (var _object in location.Objects)
                    {
                        Objects.Add(new
                        {
                            _object.ID,
                            _object.Name,
                            _object.Active,
                            _object.Description,
                            _object.LastRequestDate,
                            Device = new
                            {
                                ID = _object.Device.ID,
                                RTU = _object.Device.RTU,
                                PhoneNumber = _object.Device.Connection.PhoneNumber,
                                IPAddress = _object.Device.Connection.IPAddress,
                            },
                            LastRequestStatus = new
                            {
                                Status = _object.LastRequestStatus.Status,
                                Code = _object.LastRequestStatus.StatusCode,
                            }
                        });
                    }
                    Locations.Add(new { location.ID, location.Name, Objects });
                }
                Regions.Add(new { ID = region.ID, Name = region.Name, Locations });
            }
            return Regions;
        }
    }
}
