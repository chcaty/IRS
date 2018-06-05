import { asyncRouterMap, constantRouterMap } from '@/router/index'

/**
 * 通过meta.role判断是否与当前用户权限匹配
 * @param roles
 * @param route
 */
function hasPermission(routes, route) {
  //if (route.meta && route.meta.role) {

    //return roles.some(role => route.meta.role.indexOf(role) >= 0)
  if(route.name){
    return routes.some(role => route.name == role)
  } else {
    return true
  }
}

/**
 * 递归过滤异步路由表，返回符合用户角色权限的路由表
 * @param asyncRouterMap
 * @param roles
 */
function filterAsyncRouter(asyncRouterMap, routes) {
  const accessedRouters = asyncRouterMap.filter(route => {
    if (hasPermission(routes, route)) {
      if (route.children && route.children.length) {
        route.children = filterAsyncRouter(route.children, routes)
      }
      return true
    }
    return false
  })
  return accessedRouters
}

const permission = {
  state: {
    routers: constantRouterMap,
    addRouters: []
  },
  mutations: {
    SET_ROUTERS: (state, routers) => {
      state.addRouters = routers
      state.routers = constantRouterMap.concat(routers)
    }
  },
  actions: {
    GenerateRoutes({ commit }, data) {
      return new Promise(resolve => {
        //const { roles } = ['']
        const { routes } = data
        //alert(roles)
        //console.log(data);
         let accessedRouters
        // if (roles.indexOf('admin') >= 0) {
        //   accessedRouters = asyncRouterMap
        // } else {
          accessedRouters = filterAsyncRouter(asyncRouterMap, routes)
        //}
         // accessedRouters = data;
          console.log(accessedRouters);
          
        commit('SET_ROUTERS', accessedRouters)
        resolve()
      })
    }
  }
}

export default permission
