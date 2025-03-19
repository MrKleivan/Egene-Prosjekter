<script setup>
import { RouterLink, useRoute } from 'vue-router'

const isActiveLink = (routePath) => {
    const route = useRoute();

    return route.path === routePath;
}

</script>

<template>
    <nav>
        <RouterLink 
            to="/"
            :class="[
                isActiveLink('/')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]"
            >Hjem
        </RouterLink>
        <RouterLink 
            to="/cars"
            :class="[
                isActiveLink('/cars')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]"
            >Biler
        </RouterLink>
        <RouterLink 
            to="/about"
            :class="[
                isActiveLink('/about')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]">Om oss
        </RouterLink>
        <RouterLink 
            to="/employee"
            :class="[
                isActiveLink('/employee')
                ? 'homeLinkActive'
                : 'homeLinkNotActive',
                'linkButton',
            ]"
            >Våret personell
        </RouterLink>
        
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