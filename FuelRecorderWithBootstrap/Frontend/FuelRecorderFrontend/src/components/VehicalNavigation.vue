<script setup>
import MyHeader from '@/views/Header.vue';
import { BCol, BRow } from 'bootstrap-vue-next';
import { useRouter } from 'vue-router';

const router = useRouter();

</script>

<template>
    <MyHeader />
    <br/>
    <BRow>
        <BCol sm style="text-align: center;">
            <BButton class="mx-2" @click="router.push('/regVehicle')" variant="dark">Registrer et nytt kjøretøy</BButton>
            <BButton class="mx-2" @click="router.push('/vehicleList')" variant="dark">Eksisterende kjøretøy oversikt</BButton>
        </BCol>
    </BRow>
</template>

<style>

</style>