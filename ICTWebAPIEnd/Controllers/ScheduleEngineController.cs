using ictweb5.Models;
using ICTWebAPIEnd.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScheduleEngineController : CustomICTAPIController
    {
        public ScheduleEngineController(IAPIDataRepository Repository) :
             base(Repository)
        {
        }

        [HttpPost]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Schedule.ReadCurrent")]
        public Object ReadCurrent([FromBody] GeneralScheduleClass data)
        {
            return Status(ApiRepository.ScheduleEngine.AddSchedule(data, CurrentUser));
        }

        [HttpPost]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Schedule.ReadArchive")]
        public Object ReadArchive([FromBody] GeneralScheduleClass data)
        {
            return Status(ApiRepository.ScheduleEngine.AddSchedule(data, CurrentUser));
        }

        [HttpGet]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Schedule.ViewScheduleContent")]
        public Object CheckScheduleStatus(string scheduleID)
        {
            return Status(ApiRepository.ScheduleEngine.ViewScheduleContent(new ScheduleClass() { ID = scheduleID }));
        }
    }
}
