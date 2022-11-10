using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;

namespace ICTWebAPIEnd.Domain.Entities.Tele
{
    public class ICTAPITeleSignalDataRepository : BaseICTDataRepositoryClass, IAPITeleSignal
    {
        public ICTAPITeleSignalDataRepository(IICTDataRepository Repository):
            base(Repository)
        {
        }
        public object Value(EntityClass _Signal, string DateFrom, string DateTo, UserAccountClass user)
        {
            try
            {
                string name = "";
                Dictionary<String, Object> Params = new Dictionary<String, Object>();
                string SQLStatement = "exec ict_GetTeleDataByFieldID @ObjectFieldID, @BeginTime, @EndTime";
                Params.Add("@ObjectFieldID", _Signal.ID);
                Params.Add("@BeginTime", DateTime.ParseExact(DateFrom, "dd.MM.yyyy", null));
                Params.Add("@EndTime", DateTime.ParseExact(DateTo, "dd.MM.yyyy", null));
                DataTable data = repository.Common.OpenQuery(SQLStatement, Params);
                List<object> Values = new List<object>();
                foreach (DataRow row in data.Rows)
                {
                    name = row["TagDesc"].ToString();
                    Values.Add(new
                    {
                        TimeStamp = row["TimeStamp"].ToString(),
                        Value = row["VALUE"].ToString(),
                        Quality = row["Quality"].ToString()
                    });
                };
                Object Signal = new
                {
                    Signal = new
                    {
                        ID = _Signal.ID,
                        Name = name
                    },
                    Values = Values
                };
                return Signal;
            }
            catch
            {
                return null;
            }
        }

        public object ViewAll(EntityClass Object, UserAccountClass user)
        {
            if(repository.User.HasAccess(user, new CommonObjectClass() { ID = Object.ID }))
            {
                string SQLStatement = "select ID, TagDesc, " +
                "case TagType " +
                "when 0 then 'аналог' " +
                "when 1 then 'дискрет' " +
                "when 2 then 'аналог' " +
                "when 3 then 'состояние' " +
                "end TagType, " +
                "BottomToFactor, TopToFactor, LowestLevel, HighestLevel, AlarmMessageLowest, AlarmMessageHighest " +
                "from ObjectFields where FK_ObjectID = " + Object.ID + " and Visible = 1";
                DataTable data = repository.Common.OpenQuery(SQLStatement);
                List<object> Signals = new List<object>();
                foreach (DataRow row in data.Rows)
                {
                    Signals.Add(new
                    {
                        Signal = new
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            name = row["TagDesc"].ToString(),
                            type = row["TagType"].ToString()
                        },
                        BottomToFactor = row["BottomToFactor"].ToString(),
                        TopToFactor = row["TopToFactor"].ToString(),
                        LowestLevel = row["LowestLevel"].ToString(),
                        HighestLevel = row["HighestLevel"].ToString(),
                        AlarmMessageLowest = row["AlarmMessageLowest"].ToString(),
                        AlarmMessageHighest = row["AlarmMessageHighest"].ToString()
                    });
                }
                return Signals;
            }
            return null;
        }

        public object View<T>(T dataItem, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ViewAll<T>(IEnumerable<T> Data, IQueryCollection Params, UserAccountClass user)
        {
            throw new NotImplementedException();
        }
    }
}
