<script setup>
import { reactive, ref } from 'vue';
import { onMounted } from 'vue';


const FuelRecords = ref([]);
const error = ref(null);
const loading = ref(false);

const FuelRecord = reactive({
    kilometer: '',
    fuelFilled: '',
    price: ''
});


const GetRecords = async () =>  {
    loading.value = true;
    error.value = null;
    FuelRecords.value = [];
    try {
        const token = localStorage.getItem('jwtToken');
        if(!token){
            throw new Error('Ingen token funnet. Vennligst logg inn');
        }
        const response = await fetch('/FuelRecorder', {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`Feil ved opplasting av data. Status: ${response.status}`)
        }
        
        
        FuelRecords.value = await response.json();
        getDifferences();
    }
    catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
};
onMounted(GetRecords);

const AddRecord = async () => {
    loading.value = true;
    error.value = null;
    try {
        const token = localStorage.getItem('jwtToken');
        if(!token){
            throw new Error('Ingen token funnet. Vennligst logg inn');
        }
        const response = await fetch('/FuelRecorder', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                Kilometer: FuelRecord.kilometer,
                FuelFilled: FuelRecord.fuelFilled,
                Price: FuelRecord.price
            }),
        });
        if (!response.ok) {
            throw new Error(`Feil ved opplasting av data. Status: ${response.status}`)
        }
        FuelRecord.kilometer = '';
        FuelRecord.fuelFilled = '';
        FuelRecord.price = '';
        await GetRecords();
    }
    catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}

const DeleteRecord = async (id) => {
    loading.value = true;
    error.value = null;
    try {
        const token = localStorage.getItem('jwtToken');
        if(!token){
            throw new Error('Ingen token funnet. Vennligst logg inn');
        }
        const response = await fetch(`/FuelRecorder/${id}`, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        });
        if (!response.ok) {
            throw new Error(`Oppstått feil prøv igjenn`)
        }

        await GetRecords();
    }
    catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}

const getDifferences = () => {
    let differenses = 
        {
            kilometer: 0,
            liters: 0,
            cost: 0,
        };
    FuelRecords.value.forEach(record => {
        differenses.kilometer += Number(record.kilometer);
        differenses.liters += Number(record.fuelFilled);
        differenses.cost += Number(record.price)
    }); 

    differenses.cost -= FuelRecords.value[FuelRecords.value.length - 1].price;
    differenses.kilometer -= FuelRecords.value[0].kilometer;

    console.log(differenses.kilometer + ' ' + differenses.cost);

    return differenses;
};

</script>

<template>
    <div class="diagramWrapper">
    </div>
    <div class="tableWrapper">
        <div v-if="loading">Laster data...</div>
        <div v-if="error" class="error">{{ error }}</div>
        <span>
            <p>Dine registreringer</p>
        </span>
        
        <table>
            <tbody>
                <tr>
                    <th>Kilometer</th>
                    <th>Mengde drivstoff fyldt</th>
                    <th>Betalt</th>
                    <th>•••</th>
                </tr>
                <tr v-for="record in FuelRecords" :key="record.id">
                    <td>{{ record.kilometer }}</td>
                    <td>{{ record.fuelFilled }}</td>
                    <td>{{ record.price }}</td>
                    <td @click="DeleteRecord(record.id)">Slett</td>
                </tr>
            </tbody>
        </table>
        <span>
            <p>Ny registrering</p>
        </span>
        <table>
            <tbody>
                <tr>
                    <th>Kilometer</th>
                    <th>Mengde drivstoff fyldt</th>
                    <th>Betalt</th>
                </tr>
                <tr>
                    <td><input type="text" v-model="FuelRecord.kilometer"/></td>
                    <td><input type="text" v-model="FuelRecord.fuelFilled"/></td>
                    <td><input type="text" v-model="FuelRecord.price"/></td>
                </tr>
            </tbody>
        </table>
            <button @click="AddRecord">Legg til</button>
        
    </div>

</template>

<style scoped>

.tableWrapper {
    width: 70%;
    height: 100%;
    text-align: center;
    align-content: center;
    margin: auto;
}


table {
    width: 70%;
    margin: auto;
    padding: 1em;
    border-top: 5px double rgba(36, 91, 107, 0.842);
    border-bottom: 5px double rgba(36, 91, 107, 0.842);
    text-align: center;
}

span {
    font-family: 'Courier New', Courier, monospace;
    font-size: medium;
    font-weight: bolder;
    color: rgb(23, 102, 126);
}


th {
    width: 30%;
    height: 3vh;
    background-color: rgb(30, 49, 56);
    border-radius: 5px;
    color: aliceblue;
}

td {
    height: 2em;
    border-radius: 4px;
    font-family: 'Courier New', Courier, monospace;
    font-weight: bolder;
    color: black;
    transition-duration: 1s;
}
td:last-child {
    width: 10%;
}
td:nth-child(4):hover {
    background-color: rgb(41, 49, 56);
    color: aliceblue;
    cursor: pointer;
}
tr:nth-child(odd) {
    background-color: rgba(41, 49, 56, 0.5);
}
tr:nth-child(even) {
    background-color: rgba(41, 49, 56, 0.2);
}

button {
    width: 200px;
    height: 20px;
    border-radius: 4px;
    margin-top: 1em;
    background-color: rgb(41, 49, 56);
    font-family: 'Courier New', Courier, monospace;
    color: aliceblue;
    transition-duration: 1s;
}

button:hover {
    width: 300px;
    background-color: rgba(41, 49, 56, 0.98);
    cursor:grab;
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
}

button:focus {
    border-style: ridge;
    cursor:grabbing;
}

input {
    width: 95%;
    height: 80%;
    background-color: rgba(41, 49, 56, 0.2);
    border: none;
    font-size: larger;
    text-align: center;
}

</style>