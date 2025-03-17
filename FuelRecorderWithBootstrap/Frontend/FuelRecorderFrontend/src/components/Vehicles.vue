<script setup>
import { BButton, BCard, BCol, BRow } from 'bootstrap-vue-next';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import VehicalNavigation from './VehicalNavigation.vue';

const router = useRouter();

const Vehicles = ref([]);
const error = ref(null);
const loading = ref(false);



const GetAllVehicles = async () =>  {
    loading.value = true;
    error.value = null;
    Vehicles.value = [];

    
    try {
        const userId = Number(localStorage.getItem('userId'));
        const token = localStorage.getItem('jwtToken');
        if(!token){
            throw new Error('Ingen token funnet. Vennligst logg inn');
        }
        const response = await fetch(`/Vehicles/${userId}`, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        if (!response.ok) {
            throw new Error(`Fant ingen registrering`)
        }
        Vehicles.value = await response.json();
    }
    catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
};
const DeleteVehicle = async (id) => {
    loading.value = true;
    error.value = null;

    try {
        const token = localStorage.getItem('jwtToken');
        const response = await fetch(`/Vehicles/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        });

        if(!response.ok){
            throw new Error('Registrering mislyktes');
        }

        await GetAllVehicles();
        router.push('/vehicleList');
    } catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}

onMounted(GetAllVehicles);
console.log(Vehicles)
</script>

<template>
<VehicalNavigation />
<br/>
<BRow>
    <BCol sm>

    </BCol>
    <BCol sm>
        <BCard v-for="vehicle in Vehicles">
            <h2>{{ vehicle.name }}</h2>
            <BButton class="mx-2" @click="router.push({ path: '/edidtVehicle', 
            query: { 
                id: vehicle.id,
                type: vehicle.type,
                name: vehicle.name,
                brand: vehicle.brand,
                fuel: vehicle.fuel,
                start: vehicle.startKilometer
                } 
            })" variant="dark">Endre</BButton>
            <BButton class="mx-2" @click="DeleteVehicle(vehicle.id)" variant="dark">Slett</BButton>
        </BCard>
    </BCol>

    <BCol sm>

    </BCol>
</BRow>
</template>

<style>

</style>