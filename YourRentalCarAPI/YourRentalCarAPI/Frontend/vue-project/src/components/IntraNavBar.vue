<script setup>
import { RouterLink, useRoute } from 'vue-router'
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';

const isActiveLink = (routePath) => {
    const route = useRoute();

    return route.path === routePath;
}

const store = useStore();
const router = useRouter();

const logoutUser = async () => {
  store.dispatch('account/logout'); // Kaller logout-action
  router.push('/login'); // Omdiriger til login
  
};
</script>

<template>
    <nav>
        <RouterLink 
            to="/intra"
            :class="[
                isActiveLink('/intra')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]"
            >Hjem
        </RouterLink>
        <RouterLink 
            to="/intra/cars"
            :class="[
                isActiveLink('/intra/cars')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]"
            >Biler
        </RouterLink>
        <RouterLink 
            to="/intra/about"
            :class="[
                isActiveLink('/intra/about')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]">Om oss
        </RouterLink>
        <RouterLink 
            to="/intra/Employee"
            :class="[
                isActiveLink('/intra/Employee')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]"
            >Ansatte
        </RouterLink>
        <button @click="logoutUser">Logg ut</button>
    </nav>
</template>

<style scoped>

.homeLinkActive {
    background-color: rgba(50, 100, 55, 0.5);
}

.homeLinkNotActive {
    background-color: none;
}

.linkButton {
    border-left: 1px solid black;
}

.linkButton:first-child {
    border-left: none;
}


</style>