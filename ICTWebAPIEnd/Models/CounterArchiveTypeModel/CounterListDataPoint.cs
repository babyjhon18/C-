using System.Collections.Generic;

namespace ICTWebAPIEND.Models.CounterArchiveTypeModel
{
    public class CounterListDataPoint
    {
        public int CounterID { get; set; }
        public List<ListDataPoint> Data { get; set; }
        public CounterListDataPoint()
        {
            Data = new List<ListDataPoint>();
        }
    }
}
