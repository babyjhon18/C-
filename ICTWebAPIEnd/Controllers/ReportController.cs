using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Mvc;

namespace ICTWebAPIEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : CustomICTAPIController
    {
        public ReportController(IAPIDataRepository Repository)
            : base(Repository)
        {
        }

        //example for get:https://localhost:44398/api/Report/Heat/ObjectCard?locationsID=1042&archiveType=0&accountingType=1&dateFrom=2021-10-23T00:00:00&toDate=2021-10-29T00:00:00
        [HttpGet]
        [Route("Heat/ObjectCard")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Report.ictweb5.Domain.Reports.Heat.ObjectCardDataReportSQLDataRepositoryClass")]
        public object ObjectCardDataReport(string regionsID = "", string locationsID = "", string objectsID = "",
            int archiveType = 0, int accountingType = 0, string dateFrom = "", string toDate = "")
        {
            return Status(ApiRepository.Report.View("ictweb5.Domain.Reports.Heat.ObjectCardDataReportSQLDataRepositoryClass",
                 ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        //example for get:https://localhost:44398/api/Report/Heat/LocationHeatCurrent?objectsID=677&accountingType=13&dateFrom=2021-10-23T00:00:00&toDate=2021-10-29T00:00:00
        [HttpGet]
        [Route("Heat/LocationHeatCurrent")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Report.ictweb5.Domain.Reports.Heat.LocationHeatCurrentDataReportSQLDataRepositoryClass")]
        public object LocationHeatCurrentDataReport(string regionsID = "", string locationsID = "", string objectsID = "",
            int accountingType = 0, string dateFrom = "", string toDate = "")
        {
            return Status(ApiRepository.Report.View("ictweb5.Domain.Reports.Heat.LocationHeatCurrentDataReportSQLDataRepositoryClass",
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        [HttpGet]
        [Route("Heat/LocationRegCurrent")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Report.ictweb5.Domain.Reports.Heat.LocationRegCurrentDataReportSQLDataRepositoryClass")]
        public object LocationRegCurrentDataReport(string regionsID = "", string locationsID = "", string objectsID = "",
            int archiveType = 0, string dateFrom = "", string toDate = "")
        {
            return Status(ApiRepository.Report.View("ictweb5.Domain.Reports.Heat.LocationRegCurrentDataReportSQLDataRepositoryClass",
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        //example for get:https://localhost:44398/api/Report/Water/LocationFlatWaterTotal?locationsID=1273&archiveType=1&dateFrom=2021-10-23T00:00:00&toDate=2021-10-29T00:00:00
        [HttpGet]
        [Route("Water/LocationFlatWaterTotal")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Report.ictweb5.Domain.Reports.Water.LocationFlatWaterTotalDataReportSQLDataRepositoryClass")]
        public object LocationFlatWaterTotalDataReport(string regionsID = "", string locationsID = "", string objectsID = "",
            int archiveType = 0, string dateFrom = "", string toDate = "")
        {
            return Status(ApiRepository.Report.View("ictweb5.Domain.Reports.Water.LocationFlatWaterTotalDataReportSQLDataRepositoryClass",
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        //example for get:https://localhost:44398/api/Report/Gas/LocationGasCurrentReport?locationsID=1273&archiveType=1&dateFrom=2021-10-23T00:00:00&toDate=2021-10-29T00:00:00
        [HttpGet]
        [Route("Gas/LocationGasCurrentReport")]
        [ICTAPIMultiplePolicysAuthorize("UserIsAdmin;Report.ictweb5.Domain.Reports.Gas.LocationGasCurrentReportSQLDataRepositoryClass")]
        public object LocationGasCurrentReport(string regionsID = "", string locationsID = "", string objectsID = "",
            int archiveType = 0, string dateFrom = "", string toDate = "")
        {
            return Status(ApiRepository.Report.View("ictweb5.Domain.Reports.Gas.LocationGasCurrentReportSQLDataRepositoryClass",
                ControllerContext.HttpContext.Request.Query, CurrentUser));
        }
    }
}
