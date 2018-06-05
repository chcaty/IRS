import fetch from '@/utils/fetch'

export function getInfo(searchObj = {}, page = 1, pageSize = 10) {
  return fetch({
    url: '/api/admin',
    method: 'get',
    params: {
      page,
      pageSize,
      UserName: searchObj.UserName,
      UserCode: searchObj.UserCode
    }
  })
}

export function getCurrentPage(current_page) {
  return fetch({
    url: '/api/admin',
    method: 'get',
    params: {
      page: current_page,
    },
  })
}

export function getInfoById(id) {
  return fetch({
    url: '/api/admin/' + id,
    method: 'get',
  })
}

export function resetAdminByPsw(id, password) {
  return fetch({
    url: '/api/admin/reset/' + id,
    method: 'post',
    data: {
      password
    }
  })
}

export function updateInfo(id, data) {
  return fetch({
    url: '/api/admin/' + id,
    method: 'put',
    data: {
      UserId: data.userId,
      UserCode: data.userCode,
      UserPwd: data.userPwd,
      UserName: data.userName,
      IsEnable: data.isEnable,
      Validity: data.validity,
      RoleId: data.roldId
    }
  })
}

export function deleteInfoById(id) {
  return fetch({
    url: '/api/admin/' + id,
    method: 'delete',
  })
}

export function addInfo(data) {
  console.log(data)
  return fetch({
    url: '/api/admin',
    method: 'post',
    data: {
      UserCode: data.userCode,
      UserPwd: data.userPwd,
      UserName: data.userName,
      IsEnable: data.isEnable,
      Validity: data.validity,
      RoleId: data.roldId
    }
  })
}

export function exportCurrentPage(pageSize = 10, page = 1, searchObj = {}) {
  return fetch({
    url: '/api/admin/export',
    method: 'post',
    data: {
      page,
      pageSize,
      UserName: searchObj.UserName,
      UserCode: searchObj.UserCode
    }
  })
}

export function exportAll(searchObj = {}) {
  return fetch({
    url: '/api/admin/exportAll',
    method: 'post',
    data: {
      UserName: searchObj.UserName,
      UserCode: searchObj.UserCode
    }
  })
}

export function deleteAll(params) {
  return fetch({
    url: '/api/admin/deleteAll',
    method: 'post',
    data: {
      ids: params
    }
  })

}

export function Model(userId = '', userCode = '', userPwd = '', userName = '', isEnable = '',
  validity = '', roldId = '', roleName = '') {
  this.UserId = userId
  this.UserCode = userCode
  this.UserPwd = userPwd
  this.UserName = userName
  this.IsEnable = isEnable
  this.Validity = validity
  this.RoleId = roldId
  this.RoleName = roleName
}

export function SearchModel(userCode = '', userName = " ") {
  this.UserCode = userCode
  this.UserName = userName
}
