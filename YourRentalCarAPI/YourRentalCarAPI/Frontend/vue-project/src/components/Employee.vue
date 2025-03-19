<script setup>
import apiClient from '@/axios';
import {ref, onMounted } from 'vue';



const employees = ref([]);

onMounted(async () => {
try {
    const response = await apiClient.get('/api/Employee');
    employees.value = response.data;
}
catch (error) {
    console.log('Feil ved opplasting', error);
    console.log(employees.value)
}
});


</script>

<template>
    <div>
        <table>
            <tbody>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Stilling</th>
                    <th>Epost</th>
                    <th>Telefon</th>
                </tr>
                <tr v-for="employee in employees" :key="employee.id">
                    <td>{{employee.firstName}}</td>
                    <td>{{employee.lastName}}</td>
                    <td>{{employee.title}}</td>
                    <td>{{employee.email}}</td>
                    <td>{{employee.phoneNumber}}</td>
                </tr>
            </tbody>
        </table>
    </div>

</template>

<style scoped>

table {
    width: 80%;
    margin: auto;
    background-color:azure;
    border-collapse: collapse;
    border-radius: 10px;
    overflow: hidden;
}

table tr:first-child {
    border-radius: 10px;
}

th {
    width: 400px;
    height: 4vh;
    background-color:  white;
    border-bottom: solid lightgrey;
}

/* th:first-child {
    border-radius: 5px 0 0 0;
}

th:last-child {
    border-radius: 0 5px 0 0;
} */

td {
    text-align: center;
    height: 3vh;
}

tr:nth-child(even){
    background-color: rgba(255, 255, 255, 0.2);
    color: black;
}

tr:nth-child(odd){
    background-color: rgba(255, 255, 255, 0.9);
    color: black;
}
</style>