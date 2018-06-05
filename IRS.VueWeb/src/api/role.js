import fetch from '@/utils/fetch'

export function getInfo() {
  return fetch({
    url: '/api/role',
    method: 'get',
  })
}

export function getRoles() {
  return fetch({
    url: '/api/menu/roles',
    method: 'get',
  })
}

export function getInfoById(id) {
  return fetch({
    url: '/api/role/' + id,
    method: 'get',
  })
}

export function updateInfo(id, data) {
  return fetch({
    url: '/api/role/' + id,
    method: 'put',
    data: {
      RoleName: data.roleName,
      RoleId: data.roleId,
      RoleDecs: data.roleDecs
    }
  })
}

export function deleteInfoById(id) {
  return fetch({
    url: '/api/role/' + id,
    method: 'delete',
  })
}

export function addInfo(data) {
  return fetch({
    url: '/api/role',
    method: 'post',
    data: {
      RoleName: data.roleName,
      RoleId: data.roleId,
      RoleDecs: data.roleDecs
    }
  })
}

export function getPermissionById(id) {
  return fetch({
    url: '/api/role/permission/' + id,
    method: 'get',
  })
}

export function addPermissionInfo(id,permission=[]) {
  let result = permission;
  return fetch({
    url: '/api/role/permission/'+id,
    method: 'put',
    data: {
      Permission : result
    }
  })
}

export function Model(roleName = '', roleId = '', roleDecs = '',permission = []) {
  this.RoleName = roleName
  this.RoleId = roleId
  this.RoleDecs = roleDecs
  this.Permission = permission
}
