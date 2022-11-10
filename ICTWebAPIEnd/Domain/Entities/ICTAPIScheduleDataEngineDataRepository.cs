using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ICTWebAPIEnd.Models;
using ICTWebAPIEnd.ProxyDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICTWebAPIEnd.Domain.Entities
{
    public class ICTAPIScheduleDataEngineDataRepository : BaseICTDataRepositoryClass, IAPIScheduleDataEngine
    {
        public ICTAPIScheduleDataEngineDataRepository(IICTDataRepository Repository) :
            base(Repository)
        {
        }

        public ScheduleClass AddSchedule(GeneralScheduleClass GeneralSheduleClass, UserAccountClass User)
        {
            try
            {
                List<RegionClass> Regions = new List<RegionClass>();
                List<LocationClass> Locations = new List<LocationClass>();
                List<BaseObjectClass> Objects = new List<BaseObjectClass>();
                List<CounterClass> Counters;
                Objects.Add(new BaseObjectClass() { ID = GeneralSheduleClass.ViewParam.ObjectID });
                if (GeneralSheduleClass.ViewParam.Counters != null && GeneralSheduleClass.ViewParam.Counters != "")
                {
                    Counters = (from el in GeneralSheduleClass.ViewParam.Counters.Split(',')
                                select new CounterClass() { ID = Convert.ToInt32(el) }).ToList();
                }
                else Counters = new List<CounterClass>();
                return base.repository.ScheduleEngine.AddSchedule(Regions, Locations, Objects,
                    Counters, GeneralSheduleClass.ScheduleParam, User);
            }
            catch
            {
                return null;
            }
        }

        public void CheckScheduleStatus(ScheduleClass Schedule)
        {
            base.repository.ScheduleEngine.CheckScheduleStatus(Schedule);
        }

        public ScheduleClass AddSchedule(List<RegionClass> Regions, List<LocationClass> Locations,
            List<BaseObjectClass> Objects, List<CounterClass> Counters, ScheduleParamClass Parameters, UserAccountClass User)
        {
            throw new NotImplementedException();
        }

        public bool DeleteObjectSchedule(ScheduleClass Schedule, ObjectScheduleClass ObjectSchedule)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSchedule(ScheduleClass Schedule)
        {
            throw new NotImplementedException();
        }

        public List<ScheduleJournalItemClass> Journal(RegionClass Region, LocationClass Location,
            BaseObjectClass Object, ObjectRequestStatus Status, UserAccountClass User, DateTime DateFrom, DateTime DateTo)
        {
            throw new NotImplementedException();
        }

        public List<ObjectScheduleClass> ViewScheduleContent(ScheduleClass Schedule)
        {
            return repository.ScheduleEngine.ViewScheduleContent(Schedule);
        }
    }
}
