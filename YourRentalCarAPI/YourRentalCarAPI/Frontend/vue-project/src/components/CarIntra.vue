<script setup>
import {ref, onMounted, reactive } from 'vue';
import apiClient from '../axios';

const Cars = ref([]);
const Car = reactive({
    Brand: '',
    ModelYear: '',
    WheelDrive: '',
    Seats: '',
    Fuel: '',
    Picture: '',
});


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

const addCar = (async () => {
    try {
        const token = localStorage.getItem('token');
        await apiClient.post("/api/CarInnStock", 
        {
            Brand: Car.Brand,
            ModelYear: Car.ModelYear,
            WheelDrive: Car.WheelDrive,
            Seats: Car.Seats,
            Fuel: Car.Fuel,
            Picture: Car.Picture
        },
        {
            headers: {
                Authorization: `Baerer ${token}`, 
            }
        });
        Object.assign(Car, {
            Brand: '',
            ModelYear: '',
            WheelDrive: '',
            Seats: '',
            Fuel: '',
            Picture: '',
        });

        await UpdateCarsList();
    }
    catch (error) {
        alert("Feil", error);
        console.log(error)
    }
});

const DeleteCar = async (id) => {
    try {
        const token = localStorage.getItem('token');
        await apiClient.delete(`https://localhost:7114/api/CarInnStock/${id}`, 
         {
            headers: {
                Authorization: `Baerer ${token}`,
            }
        });

        await UpdateCarsList();
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
                <th>Merke</th>
                <th>Årsmodell</th>
                <th>Hjuldrift</th>
                <th>Seter</th>
                <th>Drivstoff</th>
                <th>Bilde</th>
                <th>Buttons</th>
            </tr>
            <tr v-for="car in Cars" :key="Cars.id">
                <td>{{car.brand}}</td>
                <td>{{car.modelYear}}</td>
                <td>{{car.wheelDrive}}</td>
                <td>{{car.seats}}</td>
                <td>{{car.fuel}}</td>
                <td>{{car.picture}}</td>
                <td><button @click="UpdateCar(car.id)">Update</button><button @click="DeleteCar(car.id)">Delite</button></td>
            </tr>
        </tbody>
    </table>
    <br />
    <table>
        <tbody>
            <tr>
                <th>Merke</th>
                <th>Årsmodell</th>
                <th>Hjuldrift</th>
                <th>Seter</th>
                <th>Drivstoff</th>
                <th>Bilde</th>
                <th>Buttons</th>
            </tr>
            <tr>
                <td><input v-model="Car.Brand"/></td>
                <td><input v-model="Car.ModelYear"/></td>
                <td><input v-model="Car.WheelDrive"/></td>
                <td><input v-model="Car.Seats"/></td>
                <td><input v-model="Car.Fuel"/></td>
                <td><input v-model="Car.Picture"/></td>
                <td><button @click="addCar">Legg til Bil</button></td>
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