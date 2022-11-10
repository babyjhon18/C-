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
    public class ObjectController : CustomICTAPIController
    {
        public ObjectController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }

        //example for get:https://localhost:44398/api/Object?objectID=4141
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Config.ObjectTree;Object.Create")]
        public object Get(int regionID = 0, int locationID = 0, int objectID = 0)
        {
            return Status(ApiRepository.Object.ViewAll(new List<CommonObjectClass>(),
                    ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        /*
           ctDefault = 0, //клонирование с параметрами по умолчанию
           ctByName = 1, //задается новое наименование
           ctByRTU = 2//задается новый номер рту
        */
        //example for clone:https://localhost:44398/api/Object/Clone?objectID=4141&count=2&cloneType=0
        [HttpGet]
        [Route("Clone")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Object.Create")]
        public void Clone(int objectID = 0, int count = 0, int cloneType = 0, string cloneOptions = "")
        {
            Status(ApiRepository.Object.Clone(new CommonObjectClass() { ID = objectID },
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        //example for post(create):https://localhost:44398/api/Object
        //then body
        /*
         * {
         *  
         * }
        */
        //exapmle for post(update):https://localhost:44398/api/Object?objectID=4141
        //then body
        /*
         * {
         * 
         * }
        */
        [HttpPost]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Object.Create;Object.Update")]
        public override void Post([FromBody] Object data, int objectID = 0)
        {
            base.Post(data, objectID);
        }

        //example for delete:https://localhost:44398/api/Object?objectID=4141
        [HttpDelete]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Object.Delete")]
        public override void Delete(int objectID)
        {
            base.Delete(objectID);
        }

        protected override bool DoCreate(Object dataItem)
        {
            return ApiRepository.Object.Create(JsonConvert.DeserializeObject<CommonObjectClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoUpdate(Object dataItem, int entityID)
        {
            return ApiRepository.Object.Update(JsonConvert.DeserializeObject<CommonObjectClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoDelete(int objectID)
        {
            return ApiRepository.Object.Delete(new CommonObjectClass() { ID = objectID }, CurrentUser);
        }
    }
}
