import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      //@ts-ignore
      component: LoginView,
    },
    {
      path: '/protected',
      name: 'ProtectedPage',
      component: HomeView
    }
  ],
})

export default router
