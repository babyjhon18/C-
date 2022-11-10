using ICTWebAPIEND.Models.CounterArchiveTypeModel;
using System.Collections.Generic;

namespace ICTWebAPIEnd.Models.CounterLastArchiveTypeModel
{
    public class LastDataByObject
    {
        public int ObjectID { get; set; }
        public List<CounterListDataPoint> ListOfCounters { get; set; }
        public LastDataByObject()
        {
            ListOfCounters = new List<CounterListDataPoint>();
        }
    }
}
