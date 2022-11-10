using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DictionaryController : CustomICTAPIController
    {
        public DictionaryController(IAPIDataRepository Repository) :
            base(Repository)
        {
        }

        //справочник группы объектов
        [HttpGet]
        [Authorize]
        public IActionResult ObjectGroups()
        {
            return new JsonResult(new
            {
                ObjectGroups = ApiRepository.Common.DictionaryData("ObjectsGroup", "ID", "GroupName", "GroupName")
            });
        }

        //справочника типа подключения объекта 
        [HttpGet]
        [Authorize]
        public IActionResult ObjectConnectionTypes()
        {
            return new JsonResult(new
            {
                ConnectionTypes = ApiRepository.Common.DictionaryData("DeviceConnectionType", "ID",
                "ConnectionType", "ConnectionType", "ConnectionType", true)
            });
        }

        //справочник типов объекта
        [HttpGet]
        [Authorize]
        public IActionResult ObjectTypes()
        {
            return new JsonResult(new
            {
                ObjectTypes = ApiRepository.Common.DictionaryData("ObjectType", "ID", "TypeName", "TypeName", "TypeName", true)
            });
        }

        //справочник видов установки (типов учета)
        [HttpGet]
        [Authorize]
        public IActionResult AccountingTypes()
        {
            return new JsonResult(new
            {
                AccountingTypes = ApiRepository.Common.DictionaryData("AccountingType", "TypeIntCode", "AccountingType",
                "AccountingType", "TypeIntCode", true)
            });
        }

        //справочник типов приборов учета
        [HttpGet]
        [Authorize]
        public IActionResult CounterTypes()
        {
            return new JsonResult(new
            {
                CounterTypes = ApiRepository.Common.DictionaryData("CounterType", "ID", "TypeName", "TypeName", "TypeName", true)
            });
        }

        //справочник сред учета
        [HttpGet]
        [Authorize]
        public IActionResult EnvironmentTypes()
        {
            return new JsonResult(new
            {
                EnvironmentTypes = ApiRepository.Common.DictionaryData("EnvironmentType", "ID", "EnvironmentType",
                "EnvironmentType", "TypeIntCode", true)
            });
        }

        //условный справочник типов контроллеров
        [HttpGet]
        [Authorize]
        public IActionResult DeviceTypes()
        {
            return new JsonResult(new
            {
                DeviceTypes = new List<BaseItemClass>()
                {
                    new BaseItemClass() { ID = 0, Name = "Без контроллера" },
                    new BaseItemClass() { ID = 1, Name = "Индел 1708 (старый)" },
                    new BaseItemClass() { ID = 2, Name = "Индел 1708 (новый)" },
                    new BaseItemClass() { ID = 3, Name = "Индел 1708 (стандартные приборы учёта газа)" },
                    new BaseItemClass() { ID = 4, Name = "Индел 1708 (БУГ-01, Ирвис, Исток-ТМ)" }
                }
            });
        }

        //справочник состояний прибора
        [HttpGet]
        [Authorize]
        public IActionResult States()
        {
            return new JsonResult(new
            {
                States = ApiRepository.Common.DictionaryData("CounterState", "ID", "StateName", "StateName", "StateName")
            });
        }

        [HttpGet]
        [Authorize]
        //справочник потребителей
        public IActionResult Consumers()
        {
            return new JsonResult(new
            {
                Consumers = ApiRepository.Consumer.ViewAll(new List<ParentedContactClass>(),
                    ControllerContext.HttpContext.Request.Query, CurrentUser)
            });
        }
    }
}
