using ictweb5.Models;
using ICTWebAPIEnd;
using ICTWebAPIEnd.Controllers;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ICTWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : CustomICTAPIController
    {
        public RegionController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }

        //example for get:https://localhost:44398/api/Region?regionID=84
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Config.ObjectTree")]
        public object Get(int regionID = 0)
        {
            return Status(ApiRepository.Region.ViewAll(new List<EntityClass>(),
                ControllerContext.HttpContext.Request.Query, new UserAccountClass(true)));
        }

        //example for post(create):https://localhost:44398/api/Region
        //then body
        /*
         * {
         *   "name":"RomaRegion"
         * }
        */
        //exapmle for post(update):https://localhost:44398/api/Region?regionID=84
        //then body
        /*
         * {
         *  "id":"84",
         *  "name":"RegionRomaRegion"
         * }
         */

        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin")]
        public override void Post([FromBody] Object data, int regionID = 0)
        {
            base.Post(data, regionID);
        }

        //example for delete:https://localhost:44398/Region?regionID=84
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin")]
        public override void Delete(int regionID)
        {
            base.Delete(regionID);
        }

        protected override bool DoCreate(Object dataItem)
        {
            return ApiRepository.Region.Create(JsonConvert.DeserializeObject<RegionClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoUpdate(Object dataItem, int regionID)
        {
            return ApiRepository.Region.Update(JsonConvert.DeserializeObject<RegionClass>(dataItem.ToString()),
                        CurrentUser);
        }

        protected override bool DoDelete(int regionID)
        {
            return ApiRepository.Region.Delete(new RegionClass() { ID = regionID }, CurrentUser);
        }
    }
}
