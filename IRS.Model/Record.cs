using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class Record
    {
        public int RecordId { get; set; }
        public string RecordUser { get; set; }
        public string UserPhone { get; set; }
        public int DormCategory { get; set; }
        public string UserDorm { get; set; }
        public string RecordTime { get; set; }
        public int FaultCategory { get; set; }
        public string FaultDesc { get; set; }
        public int FaultResult { get; set; }
    }
}
