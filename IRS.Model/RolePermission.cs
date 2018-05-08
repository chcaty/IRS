using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Model
{
    public class RolePermission
    {
        public int RolePermissionId { get; set; }
        public int RolesId { get; set; }
        public int PermissionId { get; set; }
    }
}
