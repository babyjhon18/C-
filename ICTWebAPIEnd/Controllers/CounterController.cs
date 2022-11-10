using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterController : CustomICTAPIController
    {
        public CounterController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }

        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Object.ViewCounters")]
        public object Get(int counterID = 0)
        {
            return Status(ApiRepository.Counter.View(new CounterClass() { ID = counterID },
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Counter.Create;Counter.Update")]
        public override void Post([FromBody] Object data, int counterID = 0)
        {
            base.Post(data, counterID);
        }

        [HttpDelete]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Counter.Delete")]
        public override void Delete(int counterID)
        {
            base.Delete(counterID);
        }

        [HttpGet]
        [Route("Clone")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Object.Create")]
        public void Clone(int counterID = 0, int count = 0)
        {
            Status(ApiRepository.Counter.Clone(new CounterClass() { ID = counterID },
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        protected override bool DoCreate(Object dataItem)
        {
            return ApiRepository.Counter.Create(JsonConvert.DeserializeObject<CounterClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoUpdate(Object dataItem, int entityID)
        {
            return ApiRepository.Counter.Update(JsonConvert.DeserializeObject<CounterClass>(dataItem.ToString()),
                    CurrentUser);
        }

        protected override bool DoDelete(int counterID)
        {
            return ApiRepository.Counter.Delete(new CounterClass() { ID = counterID }, CurrentUser);
        }
    }
}
