using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : CustomICTAPIController
    {
        public MapController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }
        
        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Map.Index")]
        public object Get(int region = 0, int locationID = 0, int objectID = 0)
        {
            return Status(ApiRepository.Map.View(new EntityClass(),
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }
    }
}
