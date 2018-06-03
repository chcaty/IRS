using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserPwd { get; set; }
        public string UserName { get; set; }
        public int IsEnable { get; set; }
        public string Validity { get; set; }
        public int RoleId { get; set; }
        public int IsDeleted { get; set; }
    }
}
