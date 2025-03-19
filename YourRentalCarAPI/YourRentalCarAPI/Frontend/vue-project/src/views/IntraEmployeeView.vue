<script setup>
import EmployeeIntra from '../components/EmployeeIntra.vue'
import intraHeader from '@/components/intraHeader.vue';
</script>

<template>
    <intraHeader />
    <br />
    <div class="employeeWrapper">
        <h1>Våre ansatte</h1><hr /><br>
        <EmployeeIntra />
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