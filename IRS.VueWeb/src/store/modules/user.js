import { login, logout, getInfo, loginToken } from '@/api/login'
import { getToken, setToken, removeToken } from '@/utils/auth'

const user = {
  state: {
    token: getToken(),
    name: '',
    avatar: '',
    roles: [],
    permissions: [],
    routeName: [],
    routes: [],
  },

  mutations: {
    SET_TOKEN: (state, token) => {
      state.token = token
    },
    SET_NAME: (state, name) => {
      state.name = name
    },
    SET_AVATAR: (state, avatar) => {
      state.avatar = avatar
    },
    SET_ROLES: (state, roles) => {
      state.roles = roles
    },
    SET_PERMISSIONS: (state, permissions) => {
      state.permissions = permissions
    },
    SET_ROUTENAME: (state, routeName) => {
      state.routeName = routeName
    },
    SET_ROUTES: (state, routes) => {
      state.routes = routes
    }
  },

  actions: {
    // 登录
    Login({ commit }, userInfo) {
      const username = userInfo.username.trim()
      return new Promise((resolve, reject) => {
        login(username, userInfo.password).then(response => {
          const data = response
          setToken(data.token)
          commit('SET_TOKEN', data.token)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },

    // 获取用户信息
    GetInfo({ commit, state }) {
      return new Promise((resolve, reject) => {
        var token = getToken();
        getInfo(token).then(response => {
          const data = response.data
          commit('SET_ROLES', data.role)
          commit('SET_NAME', data.name)
          //commit('SET_AVATAR', data.avatar)
          commit('SET_PERMISSIONS', data.permission)
          commit('SET_ROUTES', data.route)
          let routeName = [];
          let permissions = data.permission;
          routeName = permissions.map(item => {
            return item.name
          })
          commit('SET_ROUTENAME', routeName)
          resolve(response)
        }).catch(error => {
          reject(error)
        })
      })
    },

    // 登出
    LogOut({ commit }) {
      return new Promise((resolve, reject) => {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        commit('SET_NAME', '')
        commit('SET_AVATAR', '')
        commit('SET_PERMISSIONS', [])
        commit('SET_ROUTES', [])
        removeToken()
        resolve()
      })
    },

    // 刷新token
    RefreshToken({ commit }) {
      return new Promise((resolve, reject) => {
        loginToken().then((response) => {
          const data = respons
          setToken(data.token)
          commit('SET_TOKEN', data.token)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })

    },
    // 前端 登出
    FedLogOut({ commit }) {
      return new Promise(resolve => {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        commit('SET_NAME', '')
        commit('SET_AVATAR', '')
        commit('SET_PERMISSIONS', [])
        commit('SET_ROUTES', [])
        removeToken()
        resolve()
      })
    }
  }
}

export default user
