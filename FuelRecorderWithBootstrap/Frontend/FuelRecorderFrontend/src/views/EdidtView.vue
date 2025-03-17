<script setup>
import { BCol } from 'bootstrap-vue-next';
import MyHeader from './Header.vue';
import { useRouter } from 'vue-router';

const router = useRouter();
</script>

<template>
    <MyHeader />  
    <br />
    <BCol sm style="text-align: center;">
        <BButton class="mx-2" size="sm" @click="router.push('/edidtUser')" variant="dark">Rediger bruker</BButton>
        <BButton class="mx-2" size="sm" @click="router.push('/edidtVehicle')" variant="dark">Rediger kjøretøy</BButton>
    </BCol>

</template>

<style>

</style>