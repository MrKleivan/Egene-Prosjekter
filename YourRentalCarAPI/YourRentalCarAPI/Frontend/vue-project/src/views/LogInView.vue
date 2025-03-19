<script setup>
import { RouterLink } from 'vue-router';
import LogIn from '@/components/LogIn.vue';
</script>

<template>
    <div class="LogInWrapper">
        <div class="LogInContent">
            <LogIn />
        </div>
    </div>
</template>

<style scoped>

.LogInWrapper {
    position: relative;
    width: 30%;
    height: 75vh;
    margin: auto;
    align-content: center;
}

.LogInContent {
    width: 60%;
    height: 15vh;
    margin: auto;
    text-align: center;
    align-content: center;
    background-color: rgba(155, 230, 180, 0.6);
    border-radius: 10px;
}


</style>