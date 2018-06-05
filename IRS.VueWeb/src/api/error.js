import fetch from '@/utils/fetch'

export function getInfo() {
  return fetch({
    url: '/api/error',
    method: 'get',
  })
}

export function getInfoById(id) {
  return fetch({
    url: '/api/error/' + id,
    method: 'get'
  })
}

export function updateInfo(id, data) {
  return fetch({
    url: '/api/error/' + id,
    method: 'put',
    data: {
      CategoryInfoId: data.categoryInfoId,
      CategoryInfoName: data.categoryInfoName,
      CategoryInfoOrder: data.categoryInfoOrder,
      CategoryInfoEnable: data.categoryInfoEnable,
      CategoryInfoType: 2,
      StartFlag: data.startFlag,
      EndFlag: data.endFlag
    },
  })
}

export function addInfo(data) {
  return fetch({
    url: '/api/error',
    method: 'post',
    data: {
      CategoryInfoName: data.categoryInfoName,
      CategoryInfoOrder: data.categoryInfoOrder,
      CategoryInfoEnable: data.categoryInfoEnable,
      CategoryInfoType: 2,
      StartFlag: data.startFlag,
      EndFlag: data.endFlag
    },
  })
}

export function deleteInfoById(id) {
  return fetch({
    url: '/api/error/' + id,
    method: 'delete',
  })
}

export function Model(categoryInfoId = '', categoryInfoType = '', categoryInfoName = '', categoryInfoOrder = '',
  categoryInfoEnable = '', startFlag = '', endFlag = '') {
  this.CategoryInfoId = categoryInfoId
  this.CategoryInfoName = categoryInfoName
  this.CategoryInfoOrder = categoryInfoOrder
  this.CategoryInfoEnable = categoryInfoEnable
  this.CategoryInfoType = categoryInfoType
  this.StartFlag = startFlag
  this.EndFlag = endFlag
}
