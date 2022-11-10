using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SystemController : CustomICTAPIController
    {
        public SystemController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }

        [HttpGet]
        [Authorize]
        public object Search(int searchType, string condition)
        {
            return Status(ApiRepository.System.Search(new List<BaseObjectClass>(),
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }
    }
}
