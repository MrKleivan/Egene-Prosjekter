<script setup>
import {ref, onMounted, reactive } from 'vue';
import apiClient from '../axios';

const employees = ref([]);
const employee = reactive({
    FirstName: '',
    LastName: '',
    Title: '',
    Email: '',
    PhoneNumber: '',
});


const UpdateEmployeeList = (async () => {
try {
    const response = await apiClient.get('/api/Employee');
    employees.value = response.data;
}
catch (error) {
    console.log('Feil ved opplasting', error);
}
});

onMounted(UpdateEmployeeList);

const addEmployee = (async () => {
    try {
        const token = localStorage.getItem('token');
        await apiClient.post("/api/Employee", 
        {
            firstName: employee.FirstName,
            lastName: employee.LastName,
            title: employee.Title,
            phoneNumber: employee.PhoneNumber,
            email: employee.Email
        },
        {
            headers: {
                Authorization: `Baerer ${token}`, 
            }
        });
        Object.assign(employee, {
            FirstName: '',
            LastName: '',
            Title: '',
            PhoneNumber: '',
            Email: '',
        })

        await UpdateEmployeeList();
    }
    catch (error) {
        alert("Feil", error);
        console.log(error)
    }
});

const DeleteEmployee = async (id) => {
    try {
        const token = localStorage.getItem('token');
        await apiClient.delete(`https://localhost:7114/api/Employee/${id}`, 
         {
            headers: {
                Authorization: `Baerer ${token}`,
            }
        });

        await UpdateEmployeeList();
    }
    catch (error) {
        console.log("Feil", error);
    }
};

</script>

<template>
    <div>
        <table>
            <tbody>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Stilling</th>
                    <th>Telefon</th>
                    <th>Epost</th>
                    <th>Buttons</th>
                </tr>
                <tr v-for="employee in employees" :key="employee.id">
                    <td>{{employee.firstName}}</td>
                    <td>{{employee.lastName}}</td>
                    <td>{{employee.title}}</td>
                    <td>{{employee.phoneNumber}}</td>
                    <td>{{employee.email}}</td>
                    <td><button @click="UpdateEmployee(employee.id)">Update</button><button @click="DeleteEmployee(employee.id)">Delite</button></td>
                </tr>
            </tbody>
        </table>
        <br />
        <table>
            <tbody>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Stilling</th>
                    <th>Telefon</th>
                    <th>Epost</th>
                    <th>Buttons</th>
                </tr>
                <tr>
                    <td><input v-model="employee.FirstName"/></td>
                    <td><input v-model="employee.LastName"/></td>
                    <td><input v-model="employee.Title"/></td>
                    <td><input v-model="employee.PhoneNumber"/></td>
                    <td><input v-model="employee.Email"/></td>
                    <td><button @click="addEmployee">AddWorker</button></td>
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

button {
    width: 40%;
    margin-left: 1em;
    border-radius: 5px;
    border: none;
}

button:first-child {
    margin-left: 0;
}

button:hover {
    background-color: antiquewhite;
    cursor: pointer;
}

input {
    width: 95%;
    border-radius: 5px;
    border: none;
}

input:focus {
    background-color: rgb(177, 235, 235);
}
</style>