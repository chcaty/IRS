using IRS.BLL.Interface;
using IRS.Dal.Interface;
using IRS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.BLL.Implements
{
    public partial class UserService : BaseService<User>, IUserService
    {
        private IUserDal _userDal;
        public UserService(IUserDal userDal) : base(userDal)
        {
            _userDal = userDal;
        }
    }

    public partial class RoleService : BaseService<Role>, IRoleService
    {
        private IRoleDal _roleDal;
        public RoleService(IRoleDal roleDal) : base(roleDal)
        {
            _roleDal = roleDal;
        }
    }

    public partial class PermissionService : BaseService<Permission>, IPermissionService
    {
        private IPermissionDal _permissionDal;
        public PermissionService(IPermissionDal permissionDal) : base(permissionDal)
        {
            _permissionDal = permissionDal;
        }
    }

    public partial class RolePermissionService : BaseService<RolePermission>, IRolePermissionService
    {
        private IRolePermissionDal _rolepermissionDal;
        public RolePermissionService(IRolePermissionDal rolepermissionDal) : base(rolepermissionDal)
        {
            _rolepermissionDal = rolepermissionDal;
        }

        public bool DeleteEntityById(int id)
        {
            var list = _rolepermissionDal.LoadEntities(p => p.RolesId == id);
            foreach (var i in list)
            {
                _rolepermissionDal.DeleteEntity(i);
            }
            return _rolepermissionDal.SaveChanges();
        }
    }

    public partial class RecordService : BaseService<Record>, IRecordService
    {
        private IRecordDal _recordDal;
        public RecordService(IRecordDal recordDal) : base(recordDal)
        {
            _recordDal = recordDal;
        }
    }

    public partial class ProcessingRecordService : BaseService<ProcessingRecord>, IProcessingRecordService
    {
        private IProcessingRecordDal _processingrecordDal;
        public ProcessingRecordService(IProcessingRecordDal processingrecordDal) : base(processingrecordDal)
        {
            _processingrecordDal = processingrecordDal;
        }
        
    }

    public partial class CategoryInfoService:BaseService<CategoryInfo>,ICategoryInfoService
    {
        private ICategoryInfoDal _categoryInfoDal;
        public CategoryInfoService(ICategoryInfoDal categoryInfoDal):base(categoryInfoDal)
        {
            _categoryInfoDal = categoryInfoDal;
        }
    }
}
