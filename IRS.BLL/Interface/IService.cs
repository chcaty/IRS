using IRS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.BLL.Interface
{
    public partial interface IUserService : IBaseService<User>
    {

    }
    public partial interface IRoleService : IBaseService<Role>
    {

    }
    public partial interface IPermissionService : IBaseService<Permission>
    {

    }
    public partial interface IRolePermissionService : IBaseService<RolePermission>
    {
        bool DeleteEntityById(int id);
    }
    public partial interface IRecordService : IBaseService<Record>
    {

    }
    public partial interface IProcessingRecordService : IBaseService<ProcessingRecord>
    {

    }

    public partial interface ICategoryInfoService : IBaseService<CategoryInfo>
    {

    }
}
