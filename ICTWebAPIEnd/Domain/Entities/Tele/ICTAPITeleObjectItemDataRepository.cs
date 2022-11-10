using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Domain.Entities.Tele
{
    public class ICTAPITeleObjectItemDataRepository : BaseICTDataRepositoryClass, IAPITeleObject
    {
        public IAPITeleSignal Signal { get; set; }
        public IAPITeleReport Alarm { get; set; }

        public ICTAPITeleObjectItemDataRepository(IICTDataRepository Repository):
            base(Repository)
        {
            Signal = new ICTAPITeleSignalDataRepository(Repository);
            Alarm = new ICTAPITeleAlarmItemDataRepository(Repository);
        }

        public object View(EntityClass Object, UserAccountClass user)
        {
            if (repository.User.HasAccess(user, new CommonObjectClass() { ID = Object.ID }))
            {
                string SQLStatement = "exec ict_GetTeleData " + Object.ID;
                DataTable data = repository.Common.OpenQuery(SQLStatement);
                List<object> ObjectData = new List<object>();
                foreach (DataRow row in data.Rows)
                {
                    ObjectData.Add(new
                    {
                        signal = new
                        {
                            name = row["TagDesc"].ToString(),
                            type = Convert.ToInt32(row["TagType"])
                        },
                        TimeStamp = row["TimeStamp"].ToString(),
                        VALUE = row["VALUE"].ToString(),
                        Quality = Convert.ToInt32(row["Quality"]),
                        LowestLevel = row["LowestLevel"].ToString(),
                        HighestLevel = row["HighestLevel"].ToString(),
                        AlarmMessageLowest = row["AlarmMessageLowest"].ToString(),
                        AlarmMessageHighest = row["AlarmMessageHighest"].ToString(),
                    });
                }
                return ObjectData;
            }
            return null;
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }
    }
}
