using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeleController : CustomICTAPIController
    {
        public TeleController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }

        //example https://localhost:44398/api/Tele/Object/Signals?objectID=8338
        [HttpGet]
        [Route("Object/Signals")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Tele.Main.Index")]
        public object GetObjectSignals(int objectID)
        {
            return Status(ApiRepository.Tele.Object.Signal.ViewAll(new EntityClass() { ID = objectID }, CurrentUser));
        }

        //example https://localhost:44398/api/Tele/Signal/Value?signalID=8007&dateFrom=10.10.2022&dateTo=13.10.2022
        [HttpGet]
        [Route("Signal/Value")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Tele.Main.Index")]
        public object GetSignal(int signalID, string dateFrom, string dateTo)
        {
            return Status(ApiRepository.Tele.Object.Signal.Value(new EntityClass() { ID = signalID },
                dateFrom, dateTo, CurrentUser));
        }

        //example https://localhost:44398/api/Tele/Object/Data?objectID=8338
        [HttpGet]
        [Route("Object/Data")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Tele.Main.Index")]
        public object GetTeleData(int objectID)
        {
            return Status(ApiRepository.Tele.Object.View(new EntityClass() { ID = objectID }, CurrentUser));
        }

        //example https://localhost:44398/api/Tele/Object/Alarm?objectID=8338&dateFrom=10.10.2022&dateTo=13.10.2022
        [HttpGet]
        [Route("Object/Alarm")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Tele.Main.Index")]
        public object GetObjectAlarms(int objectID, string dateFrom, string dateTo)
        {
            return Status(ApiRepository.Tele.Object.Alarm.View(new EntityClass() { ID = objectID },
                dateFrom, dateTo, CurrentUser));
        }
    }
}
