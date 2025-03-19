import { createRouter, createWebHistory } from 'vue-router'
import store from '../stores'
import HomeView from '../views/HomeView.vue'



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
    },
    {
      path: '/employee',
      name: 'employee',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      //@ts-ignore
      component: () => import('../views/EmployeeView.vue'),
    },
    {
      path: '/cars',
      name: 'cars',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      //@ts-ignore
      component: () => import('../views/CarsView.vue'),
    },
    {
      path: '/login',
      name: 'login',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      //@ts-ignore
      component: () => import('../views/LogInView.vue'),
    },
    {
      path: '/intra',
      name: 'intra',
      // this generates a separate chunk (About.[hash].js) for this route
      //@ts-ignore
      component: () => import('../views/intraView.vue'),
      meta: { requiresAuth: true },
    },
    {
      path: '/intra/about',
      name: 'intraAbout',
      // this generates a separate chunk (About.[hash].js) for this route
      //@ts-ignore
      component: () => import('../views/IntraAboutView.vue'),
      meta: { requiresAuth: true },
    },
    {
      path: '/intra/cars',
      name: 'intraCars',
      // this generates a separate chunk (About.[hash].js) for this route
      //@ts-ignore
      component: () => import('../views/IntraCarView.vue'),
    },
    {
      path: '/intra/Employee',
      name: 'intraEmployee',
      // this generates a separate chunk (About.[hash].js) for this route
      //@ts-ignore
      component: () => import('../views/intraEmployeeView.vue'),
    },
    {
      path: '/:catchAll(.*)',
      name: 'not-found',
      // this generates a separate chunk (About.[hash].js) for this route
      //@ts-ignore
      component: () => import('../views/NotFoundView.vue'),
    },
    {
      path: '/intra/:catchAll(.*)',
      name: 'intra-not-found',
      // this generates a separate chunk (About.[hash].js) for this route
      //@ts-ignore
      component: () => import('../views/NotFoundView.vue'),
    },
  ],
});
router.beforeEach((to, from, next) => {
  const isAuthendicated = store.state.account?.token;

  if (to.meta.requiresAuth && !isAuthendicated) {
    if(from.path !== '/login'){

      next('/login'); // Send bruker til login hvis ikke logget inn
    }
  } else {
      next();
  }
});



export default router
