using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class CategoryInfo
    {
        public int CategoryInfoId { get; set; }
        public int CategoryInfoType { get; set; }
        public string CategoryInfoName { get; set; }
        public int CategoryInfoEnable { get; set; }
        public int CategoryInfoOrder { get; set; }
        public int StartFlag { get; set; }
        public int EndFlag { get; set; }
    }
}
