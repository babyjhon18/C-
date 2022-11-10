using System.Collections.Generic;

namespace ICTWebAPIEND.Models.CounterArchiveTypeModel
{
    public class ListDataPoint
    {
        public ictweb5.Domain.ArchiveType ArchiveType { get; set; }
        public List<DataPointClass> Points { get; set; }
        public ListDataPoint()
        {
            Points = new List<DataPointClass>();
        }
    }
}
