using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDecs { get; set; }
        public int IsDeleted { get; set; }
    }
}
