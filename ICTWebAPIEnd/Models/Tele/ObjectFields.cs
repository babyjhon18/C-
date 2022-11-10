using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTWebAPIEnd.Models.Tele
{
    public class ObjectFields
    {
        public int ID { get; set; }
        public string TagDesc { get; set; }
        public string TagType { get; set; }
        public int BottomToFactor { get; set; }
        public int TopToFactor { get; set; }
        public int LowestLevel { get; set; }
        public int HighestLevel { get; set; }
        public string AlarmMessageLowest { get; set; }
        public string AlarmMessageHighest { get; set; }
    }
}
