import Vue from 'vue'
import Router from 'vue-router'
const _import = require('./_import_' + process.env.NODE_ENV)

Vue.use(Router)

/* Layout */
import Layout from '../views/layout/Layout'

/**
* hidden: true                   if `hidden:true` will not show in the sidebar(default is false)
* redirect: noredirect           if `redirect:noredirect` will no redirct in the breadcrumb
* name:'router-name'             the name is used by <keep-alive> (must set!!!)
* meta : {
    title: 'title'               the name show in submenu and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar,
  }
**/
export const constantRouterMap = [
  {
    path: '/user',
    component: _import('user/Index'),
    hidden: true
  },
  {
    path: '/login',
    component: _import('login/Index'),
    hidden: true
  },
  {
    path: '/404',
    component: _import('404'),
    hidden: true
  },

  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
  },
  {
    path: '/dashboard',
    component: Layout,
    redirect: '/dashboard/index',
    meta: {
      role: ['admin'],
      icon: 'icon',
      title: '仪表盘'
    },
    children: [
      {
        path: 'index',
        name: 'dashboard/index',
        component: _import('dashboard/Index'),
        meta: {
          role: ['admin'],
          title: '首页',
          icon: 'icon'
        }
      }]
  }
  // {
  //   path: '*',
  //   redirect: '/404',
  //   hidden: true
  // }
]

export default new Router({
  mode: 'history', //后端支持可开
  scrollBehavior: () => ({
    y: 0
  }),
  routes: constantRouterMap
})


export const asyncRouterMap = [
  {
    path: '/record',
    component: Layout,
    redirect: '/record/index',
    name: 'record',
    meta: {
      role: ['admin', 'user'],
      icon: 'user',
      title: '报修管理'
    },
    children: [
      {
        path: 'index',
        name: 'record/index',
        component: _import('record/Index'),
        meta: {
          role: ['admin', 'user'],
          title: '报修列表',
          icon: 'table'
        }
      }
    ]
  },
  {
    path: '/admin',
    component: Layout,
    redirect: '/admin/index',
    name: 'admin',
    meta: {
      role: ['admin'],
      icon: 'user',
      title: '用户管理'
    },
    children: [
      {
        path: 'index',
        name: 'admin/index',
        component: _import('admin/Index'),
        meta: {
          role: ['admin'],
          title: '用户列表',
          icon: 'table'
        }
      }
    ]
  },
  {
    path: '/role',
    component: Layout,
    redirect: '/role/index',
    name: 'role',
    meta: {
      role: ['admin'],
      icon: 'tab',
      title: '权限管理',
    },
    children: [
      {
        path: 'index',
        name: 'role/index',
        component: _import('role/Index'),
        meta: {
          role: ['admin'],
          title: '角色管理',
          icon: 'table'
        }
      },
      {
        path: 'permission',
        name: 'permission/index',
        component: _import('permission/Index'),
        meta: {
          role: ['admin'],
          title: '功能管理',
          icon: 'table'
        }
      }
    ]
  },
  {
    path: '/charts',
    component: Layout,
    redirect: '/charts/index',
    name: 'charts',
    meta: {
      role: ['admin'],
      icon: 'tubiao',
      title: '统计分析'
    },
    children: [
      {
        path: 'index',
        name: 'charts/index',
        component: _import('charts/Index'),
        meta: {
          role: ['admin'],
          title: '统计图表',
          icon: 'zonghe'
        }
      }
    ]
  },
  {
    path: '/category',
    component: Layout,
    redirect: '/category/address',
    name: 'category',
    meta: {
      role: ['admin'],
      icon: 'tubiao',
      title: '基础信息管理'
    },
    children: [
      {
        path: 'address',
        name: 'category/address',
        component: _import('category/Address'),
        meta: {
          role: ['admin'],
          title: '宿舍楼分类管理',
          icon: 'zonghe'
        }
      },
      {
        path: 'error',
        name: 'category/error',
        component: _import('category/Error'),
        meta: {
          role: ['admin'],
          title: '故障分类管理',
          icon: 'zonghe'
        }
      },
      {
        path: 'solver',
        name: 'category/solver',
        component: _import('category/Solver'),
        meta: {
          role: ['admin'],
          title: '处理分类管理',
          icon: 'zonghe'
        }
      }
    ]
  },
  {
    path: '*',
    redirect: '/404',
    hidden: true
  }
]
