import fetch from '@/utils/fetch'

var CheckList = [];

export function getInfo(searchObj = {}) {
  return fetch({
    url: '/api/chart',
    method: 'get',
    params: {
      StartDate: searchObj.Date[0],
      EndDate: searchObj.Date[1],
      DormCheck: searchObj.CheckList[0],
      FaultCheck: searchObj.CheckList[1],
      SolverCheck: searchObj.CheckList[2],
      UserCheck: searchObj.CheckList[3]
    }
  })
}

export function getCurrentPage(current_page) {
  return fetch({
    url: '/api/record',
    method: 'get',
    params: {
      page: current_page,
    },
  })
}

export function getInfoById(id) {
  return fetch({
    url: '/api/record/' + id,
    method: 'get',
  })
}

export function getRecordById(id) {
  return fetch({
    url: '/api/record/process/' + id,
    method: 'get',
  })
}

export function updateInfo(id, data) {
  return fetch({
    url: '/api/record/' + id,
    method: 'put',
    data: {
      RecordId: data.recordId,
      RecordUser: data.recordUser,
      DormCategory: data.dormCategory,
      UserDorm: data.userDorm,
      RecordTime: data.recordTime,
      FaultCategory: data.faultCategory,
      FaultDesc: data.faultDesc,
      UserPhone: data.userPhone,
      FaultResult: data.faultResult
    },
  })
}

export function deleteInfoById(id) {
  return fetch({
    url: '/api/record/' + id,
    method: 'delete',
  })
}

export function addInfo(data) {
  console.log(data)
  return fetch({
    url: '/api/record',
    method: 'post',
    data: {
      UserPhone: data.userPhone,
      RecordUser: data.recordUser,
      DormCategory: data.dormCategory,
      UserDorm: data.userDorm,
      FaultCategory: data.faultCategory,
      FaultDesc: data.faultDesc
    }
  })
}

export function addProcessInfo(data) {
  return fetch({
    url: '/api/record/process',
    method: 'post',
    data: {
      RecordId: data.recordId,
      ProcessingPeople: data.processingPeople,
      ProcessingRecordId: data.processingRecordId,
      ProcessingDesc: data.processingDesc,
      ProcessingTime: data.processingTime,
      ProcessingResult: data.processingResult,
      UserId: data.userId
    }
  })
}

export function exportCurrentPage(pageSize = 10, page = 1, searchObj = {}) {
  return fetch({
    url: '/api/record/export',
    method: 'post',
    data: {
      page,
      pageSize,
      name: searchObj.name,
      email: searchObj.email
    }
  })
}

export function exportAll(searchObj = {}) {
  return fetch({
    url: '/api/record/exportAll',
    method: 'post',
    data: {
      name: searchObj.name,
      email: searchObj.email
    }
  })
}

export function getMenuByType(type) {
  return fetch({
    url: '/api/menu/' + type,
    method: 'get'
  })
}

export function getEndFlagByType(type) {
  return fetch({
    url: '/api/menu/endflag/' + type,
    method: 'get'
  })
}

export function Model(recordId = '', recordUser = '', dormCategory = '', userDorm = '',
  recordTime = '', faultCategory = '', faultDesc = '', faultResult = '', userPhone = '',
  dormCategoryName = '', faultResultName = '', faultCategoryName = '') {
  this.RecordId = recordId
  this.RecordUser = recordUser
  this.DormCategory = dormCategory
  this.UserPhone = userPhone
  this.DormCategoryName = dormCategoryName
  this.UserDorm = userDorm
  this.RecordTime = recordTime
  this.FaultCategory = faultCategory
  this.FaultCategoryName = faultCategoryName
  this.FaultDesc = faultDesc
  this.FaultResult = faultResult
  this.FaultResultName = faultCategoryName
}

export function ProcessModel(processingRecordId = '', recordId = '', processingPeople = '',
  processingDesc = '', processingTime = '', processingResult = '', userId = '') {
  this.RecordId = recordId
  this.ProcessingPeople = processingPeople
  this.ProcessingRecordId = processingRecordId
  this.ProcessingDesc = processingDesc
  this.ProcessingTime = processingTime
  this.ProcessingResult = processingResult
  this.UserId = userId
}

export function SearchModel(date = [], checkList = []) {
  this.Date = date
  this.CheckList = checkList
}
