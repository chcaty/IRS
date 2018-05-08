using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionCode { get; set; }
        public int IsDeleted { get; set; }
    }
}
