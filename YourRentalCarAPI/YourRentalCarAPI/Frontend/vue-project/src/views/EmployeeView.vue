<script setup>
import MainHeader from '@/components/MainHeader.vue';
import Employee from '../components/Employee.vue'
</script>

<template>
    <MainHeader />
    <br />
    <div class="employeeWrapper">
        <h1>Våre ansatte</h1><hr /><br>
        <Employee />
    </div>
</template>

<style scoped>

.employeeWrapper {
    width: 60%;
    max-height: fit-content;
    margin: auto;
    text-align: center;
}

</style>