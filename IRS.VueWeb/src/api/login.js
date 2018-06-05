import fetch from '@/utils/fetch'

export function login(username, password) {
  return fetch({
    url: '/api/login',
    method: 'post',
    data: {
      UserCode: username,
      UserPwd: password
    }
  })
}

export function getInfo(token) {
  return fetch({
    url: '/api/login',
    method: 'get',
    params:{
      Token:token
    }
  })
}

export function logout() {
  return fetch({
    url: '/api/logout',
    method: 'post'
  })
}
export function loginToken() {
  return fetch({
    url: '/api/token/refresh',
    method: 'post'
  })
}
