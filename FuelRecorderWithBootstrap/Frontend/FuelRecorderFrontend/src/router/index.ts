import { createRouter, createWebHistory } from 'vue-router'
//@ts-ignore
import HomeView from '../views/HomeView.vue'
//@ts-ignore
import LoginView from '../views/LoginView.vue'
//@ts-ignore
import UserRegistrationView from '@/views/UserRegistrationView.vue'
//@ts-ignore
import VehicleRegistrationView from '@/views/VehicleRegistrationView.vue'
//@ts-ignore
import EdidtView from '@/views/EdidtView.vue'
//@ts-ignore
import UserEdidtView from '@/views/UserEdidtView.vue'
//@ts-ignore
import VehicleEdidtView from '@/views/VehicleEdidtView.vue'
//@ts-ignore
import VehicalNavigation from '@/components/VehicalNavigation.vue'
//@ts-ignore
import VehiclesView from '@/views/VehiclesView.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/regUser',
      name: 'userRegistration',
      component: UserRegistrationView,
    },
    {
      path: '/regVehicle',
      name: 'vehicleRegistration',
      component: VehicleRegistrationView,
      meta: { requiresAuth: true },
    },
    {
      path: '/protected',
      name: 'ProtectedPage',
      component: HomeView,
      meta: { requiresAuth: true },
    },
    {
      path: '/edidt',
      name: 'edidtPage',
      component: EdidtView,
      meta: { requiresAuth: true },
    },
    {
      path: '/edidtUser',
      name: 'edidtUserPage',
      component: UserEdidtView,
      meta: { requiresAuth: true },
    },
    {
      path: '/vehicleNav',
      name: 'vehicleNav',
      component: VehicalNavigation,
      meta: { requiresAuth: true },
    },
    {
      path: '/edidtVehicle',
      name: 'edidtVehiclePage',
      component: VehicleEdidtView,
      meta: { requiresAuth: true },
    },
    {
      path: '/vehicleList',
      name: '/vehicleListPage',
      component: VehiclesView,
      meta: { requiresAuth: true },
    },
  ],
});

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('jwtToken');
  const isAuthenticated = !!token; 

  // 🚀 Sjekk om tokenet er utløpt
  if (token) {
    const payload = JSON.parse(atob(token.split('.')[1])); // Dekoder JWT
    const tokenExp = payload.exp * 1000; // JWT bruker sekunder, vi må konvertere til millisekunder
    const now = Date.now();

    if (now >= tokenExp) {
      console.log('Token er utløpt, logger ut...');
      localStorage.removeItem('jwtToken'); // Slett token
      next('/'); // Send til login
      return;
    }
  }

  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/'); // 🚫 Send til login hvis brukeren ikke er logget inn
  } else {
    next(); // ✅ Tillat navigasjon
  }
});

export default router
