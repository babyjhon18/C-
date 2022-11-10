using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjectTreeController : CustomICTAPIController
    {
        public ObjectTreeController(IAPIDataRepository Repository) :
            base(Repository)
        {
        }

        //example for get:https://localhost:44398/api/ObjectTree
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Config.ObjectTree")]
        public object Get()
        {
            return Status(ApiRepository.ObjectTree.ViewAll(new List<RegionClass>(),
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }
    }
}
