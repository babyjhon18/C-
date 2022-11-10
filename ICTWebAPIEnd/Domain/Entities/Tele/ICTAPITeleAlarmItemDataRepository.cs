using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.Models.Tele;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Domain.Entities.Tele
{
    public class ICTAPITeleAlarmItemDataRepository : BaseICTDataRepositoryClass, IAPITeleReport
    {
        public ICTAPITeleAlarmItemDataRepository(IICTDataRepository Repository):
            base(Repository)
        {
        }

        public object View(EntityClass Object, string DateFrom, string DateTo, UserAccountClass user)
        {
            if (repository.User.HasAccess(user, new CommonObjectClass() { ID = Object.ID }))
            {
                Dictionary<String, Object> Params = new Dictionary<String, Object>();
                Params.Add("@BeginTime", DateTime.ParseExact(DateFrom, "dd.MM.yyyy", null));
                Params.Add("@EndTime", DateTime.ParseExact(DateTo, "dd.MM.yyyy", null));
                string SQLStatement = "select LocationName, ObjectName, TagDesc, ObjectAlarmData.AlarmMessage, " +
                "BeginTime, EndTime, AlarmValue, AcceptTime from ObjectAlarmData " +
                "join ObjectFields on ObjectFields.ID = ObjectAlarmData.FK_ObjectFieldID " +
                "join Objects on ObjectFields.FK_ObjectID = Objects.ObjectID " +
                "join Location on Location.LocationID = Objects.FK_LocationID " +
                "where FK_ObjectID = " + Object.ID + " and BeginTime between @BeginTime and @EndTime " +
                "order by LocationName, ObjectName, BeginTime";
                DataTable data = repository.Common.OpenQuery(SQLStatement, Params);
                List<object> AlarmData = new List<object>();
                foreach (DataRow row in data.Rows)
                {
                    AlarmData.Add(new
                    {
                        signal = new
                        {
                            name = row["TagDesc"].ToString()
                        },
                        Message = row["AlarmMessage"].ToString(),
                        BeginTime = row["BeginTime"].ToString(),
                        EndTime = row["EndTime"].ToString(),
                        Value = row["AlarmValue"].ToString(),
                        AcceptTime = row["AcceptTime"].ToString()
                    });
                }
                return AlarmData;
            }
            return null;
        }
    }
}
