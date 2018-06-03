using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class ProcessingRecord
    {
        public int ProcessingRecordId { get; set; }
        public int RecordId { get; set; }
        public string ProcessingPeople { get; set; }
        public string ProcessingDesc { get; set; }
        public string ProcessingTime { get; set; }
        public int ProcessingResult { get; set; }
        public int UserId { get; set; }
    }
}
