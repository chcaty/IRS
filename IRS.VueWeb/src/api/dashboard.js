import fetch from '@/utils/fetch'

export function getCount() {
  return fetch({
    url: '/api/dashboard',
    method: 'get',
  })
}

export function getTodaylist() {
  return fetch({
    url: '/api/dashboard/recordlist',
    method: 'get',
  })
}
