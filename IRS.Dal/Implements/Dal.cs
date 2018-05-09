using IRS.Dal.Interface;
using IRS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Dal.Implements
{
    public partial class UserDal : BaseDal<User>, IUserDal
    {
        public UserDal(IRSContext dbContext) : base(dbContext)
        {

        }
    }
    public partial class RoleDal : BaseDal<Role>, IRoleDal
    {
        public RoleDal(IRSContext dbContext) : base(dbContext)
        {

        }
    }
    public partial class PermissionDal : BaseDal<Permission>, IPermissionDal
    {
        public PermissionDal(IRSContext dbContext) : base(dbContext)
        {

        }
    }
    public partial class RolePermissionDal : BaseDal<RolePermission>, IRolePermissionDal
    {
        public RolePermissionDal(IRSContext dbContext) : base(dbContext)
        {

        }
    }
    public partial class RecordDal : BaseDal<Record>, IRecordDal
    {
        public RecordDal(IRSContext dbContext) : base(dbContext)
        {

        }
    }

    public partial class ProcessingRecordDal:BaseDal<ProcessingRecord>,IProcessingRecordDal
    {
        public ProcessingRecordDal(IRSContext dbContext) : base(dbContext)
        {

        }
    }
}
