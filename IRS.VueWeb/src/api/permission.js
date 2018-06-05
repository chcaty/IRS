import fetch from '@/utils/fetch'

export function getInfo(searchObj = {}, page = 1, pageSize = 10) {
  return fetch({
    url: '/api/permission',
    method: 'get',
    params: {
      pageSize,
      page,
      PermissionName: searchObj.PermissionName,
      PermissionRoute: searchObj.PermissionRoute,
      PermissionApi: searchObj.PermissionApi
    },
  })
}

export function getPermission() {
  return fetch({
    url: '/api/permission/all',
    method: 'get',
  })
}

export function getInfoById(id) {
  return fetch({
    url: '/api/permission/' + id,
    method: 'get'
  })
}

export function updateInfo(id, data) {
  return fetch({
    url: '/api/permission/' + id,
    method: 'put',
    data: {
      PermissionId: data.permissionId,
      PermissionName: data.permissionName,
      PermissionRoute: data.permissionRoute,
      PermissionApi: data.permissionApi,
      PermissionDecs: data.permissionDecs,
      IsDeleted: data.isDeleted,
      ParentId:data.parentId,
    }
  })
}

export function addInfo(data) {
  return fetch({
    url: '/api/permission',
    method: 'post',
    data: {
      PermissionName: data.permissionName,
      PermissionRoute: data.permissionRoute,
      PermissionApi: data.permissionApi,
      PermissionDecs: data.permissionDecs,
      IsDeleted: data.isDeleted,
      ParentId:data.parentId,
    }
  })
}

export function deleteInfoById(id) {
  return fetch({
    url: '/api/permission/' + id,
    method: 'delete'
  })
}

export function Model(permissionId = null, permissionName = null, permissionRoute = null, permissionApi = null, 
  permissionDecs = null, isDeleted = null, parentId=null,parentIdName=null) {
  this.PermissionId = permissionId;
  this.PermissionName = permissionName;
  this.PermissionRoute = permissionRoute;
  this.PermissionApi = permissionApi;
  this.PermissionDecs = permissionDecs;
  this.IsDeleted = isDeleted;
  this.ParentId = parentId;
  this.ParentIdName = parentIdName;
}

export function SearchModel(PermissionName = null, PermissionRoute = null, PermissionApi = null) {
  this.PermissionApi = PermissionApi;
  this.PermissionName = PermissionName;
  this.PermissionRoute = PermissionRoute;
}


