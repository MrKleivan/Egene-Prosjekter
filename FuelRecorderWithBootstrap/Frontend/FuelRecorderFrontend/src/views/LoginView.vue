<script setup>
import { BContainer, BRow } from 'bootstrap-vue-next';
import Login from '../components/Login.vue'
import MyHeader from './Header.vue';
</script>

<template>
    <MyHeader />
    <BContainer style="margin-top: 20%;">
        <BRow align-v="center">
            <Login />
        </BRow>
    </BContainer>
</template>

<style>


</style>