<script setup>
import {ref, onMounted, reactive } from 'vue';
import apiClient from '../axios';


const Cars = ref([]);


const UpdateCarsList = (async () => {
try {
    const response = await apiClient.get('/api/CarInnStock');
    Cars.value = response.data;
}
catch (error) {
    console.log('Feil ved opplasting', error);
}
});

onMounted(UpdateCarsList);


console.log(Cars)

</script>

<template>

<br />

<div class="carHeader">
    <h2>Våres biler</h2>
    <hr>
</div>
<br/>

<div>
    <table>
        <tbody>
            <tr>
                <th>Merke</th>
                <th>Årsmodell</th>
                <th>Hjuldrift</th>
                <th>Seter</th>
                <th>Drivstoff</th>
                <th>Bilde</th>
            </tr>
            <tr v-for="car in Cars" :key="Cars.id">
                <td>{{car.brand}}</td>
                <td>{{car.modelYear}}</td>
                <td>{{car.wheelDrive}}</td>
                <td>{{car.seats}}</td>
                <td>{{car.fuel}}</td>
                <td><img v-bind:src="'src/assets/img/'+ car.picture"/></td>
            </tr>
        </tbody>
    </table>
</div>

</template>

<style scoped>

.carHeader {
    width: 80%;
    margin: auto;
    text-align: center;
}

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

img {
    position: relative;
    width: 100%;
}

td {
text-align: center;
height: 13vh;
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