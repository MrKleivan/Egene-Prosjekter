<script setup lang="ts">
import { BButton, BCol, BContainer, BForm, BFormFloatingLabel, BFormInput, BFormSelect, BRow } from 'bootstrap-vue-next';
import { reactive, ref , toRefs } from 'vue';
import { useRouter, useRoute } from 'vue-router';
// @ts-ignore
import VehicalNavigation from './VehicalNavigation.vue';

const route = useRoute();
const router = useRouter();
const error = ref<string | null>(null);
const loading = ref(false);


const vehiclesRegistration = reactive({
    Id: Number(route.query.id) || 0,
    Type: route.query.type as string || '',
    Name: route.query.name as string || '',
    Brand: route.query.brand as string || '',
    Fuel: route.query.fuel as string || '',
    StartKilometer: Number(route.query.start) || 0,
}
);


const UpdateVehicle = async () => {
    loading.value = true;
    error.value = null;


    try {
        const token = localStorage.getItem('jwtToken');
        const response = await fetch('/Vehicles', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                id: vehiclesRegistration.Id,
                type: vehiclesRegistration.Type,
                name: vehiclesRegistration.Name,
                brand: vehiclesRegistration.Brand,
                fuel: vehiclesRegistration.Fuel,
                startKilometer: vehiclesRegistration.StartKilometer,
                userId: Number(localStorage.getItem('userId'))
            })
        });

        if(!response.ok){
            throw new Error('Registrering mislyktes');
        }

        router.push('/vehicleList');
    } catch (err: any) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}

const angre = () => {
    router.push('/vehicleNav')
}

const token = localStorage.getItem('jwtToken');
</script>

<template>

<VehicalNavigation />

<BContainer class="bv-example-row">
    <br />
    <BRow>
        <BCol>
        </BCol>
        <BCol md>
            <BForm>
                <h3>Endre kjøretøy</h3>
                <BFormFloatingLabel label="VehiclesType" label-for="floatingVehiclesType" class="my-2">
                    <BFormSelect v-model="vehiclesRegistration.Type" prepend="Small" :options="['Bil', 'Motorsykkel', 'ATV']" size="sm" />
                </BFormFloatingLabel>
                <BFormFloatingLabel label="VehiclesName" label-for="floatingVehiclesName" class="my-2">
                    <BFormInput id="floatingVehiclesName" type="text"  placeholder="Name" v-model="vehiclesRegistration.Name"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="VehiclesBrand" label-for="floatingVehiclesBrand" class="my-2">
                    <BFormInput id="floatingVehiclesBrand" type="text"  placeholder="Brand" v-model="vehiclesRegistration.Brand"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="VehiclesFuel" label-for="floatingVehiclesFuel" class="my-2">
                    <BFormInput id="floatingVehiclesFuel" type="text"  placeholder="Fuel" v-model="vehiclesRegistration.Fuel"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="VehiclesStartKilometer" label-for="floatingVehiclesStartKilometer" class="my-2">
                    <BFormInput id="floatingVehiclesStartKilometer" type="number"  placeholder="Start kilometer" v-model="vehiclesRegistration.StartKilometer"/>
                </BFormFloatingLabel>
                <div style="text-align: center;">
                    <BButton size="md" class="mx-2" @click="UpdateVehicle()" variant="dark">Lagre</BButton>
                    <BButton size="md" class="mx-2" @click="angre" variant="dark">Angre</BButton>
                </div>
            </BForm>
        </BCol>
        <BCol >

        </BCol>
    </BRow>
</BContainer>
</template>

<style>

</style>