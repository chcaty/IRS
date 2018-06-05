import router from './router'
import store from './store'
import NProgress from 'nprogress' // Progress 进度条
import 'nprogress/nprogress.css'// Progress 进度条样式
import { getToken, removeToken } from '@/utils/auth' // 验权

const whiteList = ['/user', '/login']
router.beforeEach((to, from, next) => {
  NProgress.start()
  if (getToken()) {
    if (to.path === '/login') {
      next({ path: '/' })  // 如果已经登录了 就无需再输入密码
    } else {
      if (store.getters.permissions.length === 0) {
        store.dispatch('GetInfo').then(res => {
          const roles = res.data.role
          const routes = res.data.route
          store.dispatch('GenerateRoutes', { routes }).then(() => {
            router.addRoutes(store.getters.addRouters)
            next({ ...to })
          })
        })
      } else {
        next()
      }
    }
  } else {
    if (whiteList.indexOf(to.path) !== -1) {
      next()
    } else {
      next('/login')
      NProgress.done()
    }
  }
})

router.afterEach(() => {
  NProgress.done() // 结束Progress
})
