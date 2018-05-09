using IRS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Dal.Interface
{
    public partial interface IUserDal : IBaseDal<User>
    {

    }
    public partial interface IRoleDal : IBaseDal<Role>
    {

    }
    public partial interface IPermissionDal : IBaseDal<Permission>
    {

    }
    public partial interface IRolePermissionDal : IBaseDal<RolePermission>
    {

    }
    public partial interface IRecordDal : IBaseDal<Record>
    {

    }
    public partial interface IProcessingRecordDal : IBaseDal<ProcessingRecord>
    {

    }
}
