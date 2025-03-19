export default {
    namespaced: true,
    state: {
      token: localStorage.getItem('token') || null
    },
    mutations: {
      setToken(state, token) {
        state.token = token;
        localStorage.setItem('token', token);
      },
      logout(state) {
        state.token = null;
        localStorage.removeItem('token');
      }
    },
    actions: {
      login({ commit }, token) {
        commit('setToken', token);
      },
      logout({ commit }) {
        commit('logout');
      }
    }
  };
  