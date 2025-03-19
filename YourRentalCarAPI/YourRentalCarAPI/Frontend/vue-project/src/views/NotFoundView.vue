<script setup>
import { RouterLink } from 'vue-router'
import MainHeader from '@/components/MainHeader.vue';
</script>

<template>
    <MainHeader />
    <section>
        <h2>Siden ble ikke funnet</h2>
        <RouterLink
        to="/"
        class="Back"
        >Gå tilbake
        </RouterLink>
    </section>
</template>