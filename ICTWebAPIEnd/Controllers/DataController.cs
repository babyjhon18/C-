using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : CustomICTAPIController
    {
        public DataController(IAPIDataRepository Repository) :
            base(Repository)
        {
        }

        //example for get:https://localhost:44398/api/Data/Current?counterID=763
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Counter.CurrentData")]
        public object Current(int counterID = 0)
        {
            return Status(ApiRepository.Counter.Current.View(new CounterClass(),
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        //example:https://localhost:44398/api/Data/Archive?counterID=763&archiveType=1&dateFrom=2020-10-23T00:00:00&toDate=2020-10-27T01:20:23
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Counter.ArchiveData")]
        public object Archive(int counterID = 0, int archiveType = 0, string dateFrom = "", string toDate = "")
        {
            return Status(ApiRepository.Counter.Archive.View(new CounterClass(),
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Counter.ArchiveData")]
        public object Last(int objectID = 0, int counterID = 0)
        {
            return Status(ApiRepository.Object.Current.View(new BaseObjectClass(),
               ControllerContext.HttpContext.Request.Query, CurrentUser));
        }
    }
}
