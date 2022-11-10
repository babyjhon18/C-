using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : CustomICTAPIController
    {
        public LocationController(IAPIDataRepository Repository) :
            base(Repository)
        {
        }

        //example for get:https://localhost:44398/Location?locationID=1019
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Config.ObjectTree")]
        public object Get(int regionID = 0, int locationID = 0)
        {
            return Status(ApiRepository.Location.ViewAll(new List<LocationClass>(),
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        //example for post(create):https://localhost:44398/api/Location
        //then body
        /*
          {
           "name":"LocationRomaLocation",
           "region":{
               "id":88
              }
           } 
        */
        //exapmle for post(update):https://localhost:44398/api/Location?locationID=1019
        //then body
        /*
           {
                "id":"1439",
                "name":"LocationRomaLocation123",
                "region":{
                    "id":88
                }
            } 
         */

        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin")]
        public override void Post([FromBody] Object data, int locationID = 0)
        {
            base.Post(data, locationID);
        }

        //example for delete:https://localhost:44398/api/Location?locationID=1440
        [HttpDelete]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin")]
        public override void Delete(int locationID)
        {
            base.Delete(locationID);
        }

        protected override bool DoCreate(Object dataItem)
        {
            return ApiRepository.Location.Create(JsonConvert.DeserializeObject<LocationClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoUpdate(Object dataItem, int entityID)
        {
            return ApiRepository.Location.Update(JsonConvert.DeserializeObject<LocationClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoDelete(int locationID)
        {
            return ApiRepository.Location.Delete(new LocationClass() { ID = locationID }, CurrentUser);
        }
    }
}
