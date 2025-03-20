<script setup>
import { BButton, BCol, BContainer, BInput, BRow } from 'bootstrap-vue-next';
import { reactive, ref } from 'vue';
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const FuelRecords = ref([]);
const Vehicles = ref([]);
const Results = ref([]);
const error = ref(null);
const loading = ref(false);
const isWatching = ref(false);

const FuelRecord = reactive({
    kilometer: '',
    fuelFilled: '',
    price: '',
    vehicleId: 0
});

const Vehicle = reactive({
    Type: '',
    Name: '',
    Brand: '',
    Fuel: '',
    StartKilometer: 0,
});


const fetchData = async (url, method = "GET", body = null) => {
    loading.value = true;
    error.value = null;

    try {
        const token = localStorage.getItem("jwtToken");
        if (!token) throw new Error("Ingen token funnet. Vennligst logg inn");

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        };

        const options = { method, headers };
        if (body) options.body = JSON.stringify(body);

        const response = await fetch(url, options);
        if (!response.ok) throw new Error(`Feil ved henting av data: ${response.status}`);

        // 🚀 Håndter tom respons
        const text = await response.text();
        return text ? JSON.parse(text) : null;
    } catch (err) {
        error.value = err.message;
        return null;
    } finally {
        loading.value = false;
    }
};


const GetAllRecords = async () => {
    isWatching.value = false;
    FuelRecords.value = await fetchData(`/FuelRecorder/${localStorage.getItem("userId")}`);
};

const GetAllVehicles = async () => {
    Vehicles.value = await fetchData(`/Vehicles/${localStorage.getItem("userId")}`);
    if (Vehicles.value) GetTotalResult(Vehicles.value);
};

const GetRecordsByVehicleId = async (id) => {
    isWatching.value = true;
    FuelRecord.vehicleId = id;
    Vehicles.value = Vehicles.value.filter((v) => v.id === id);
    FuelRecords.value = await fetchData(`/FuelRecorder/V/${id}`);
    if (Vehicles.value) GetTotalResult(Vehicles.value);
};

const AddRecord = async () => {
    const newRecord = {
        Kilometer: FuelRecord.kilometer,
        FuelFilled: FuelRecord.fuelFilled,
        Price: FuelRecord.price,
        UserId: Number(localStorage.getItem("userId")),
        VehicleId: FuelRecord.vehicleId,
    };

    const result = await fetchData("/FuelRecorder", "POST", newRecord);

    if (result) {
        Object.assign(FuelRecord, { kilometer: "", fuelFilled: "", price: "", vehicleId: FuelRecord.vehicleId });
        isWatching.value ? await GetRecordsByVehicleId(FuelRecord.vehicleId) : await GetAllRecords();
    }
};

const DeleteRecord = async (id, vehicleId) => {
    const result = await fetchData(`/FuelRecorder/${id}`, "DELETE");
    await GetRecordsByVehicleId(vehicleId);
};

const GetResultOfSingleVehicle = (vehicle) => {
    
    const result = {
        id: vehicle.id,
        type: vehicle.type,
        name: vehicle.name,
        brand: vehicle.brand,
        startKilometer: vehicle.startKilometer,
        kilometer: 0,
        liters: 0,
        cost: 0,
        literPrMil: 0,
        costPrLiter: 0,
    };

    if (FuelRecords.value != null){
        const records = FuelRecords.value.filter(f => f.vehicleId == vehicle.id );
    
        if (records.length < 2) {
        return result;
        }
        
        result.kilometer += Number(records[records.length - 1].kilometer - records[0].kilometer);
    
        
        records.forEach(r => {
            result.liters += Number(r.fuelFilled);
            result.cost += Number(r.price);
        });
        
        result.liters -= Number(records[records.length - 1].fuelFilled);
        result.cost -= Number(records[records.length - 1].price);
        
        result.literPrMil = Number(result.liters / (result.kilometer / 10));
        result.costPrLiter = Number(result.cost / result.liters);
    }

    return result;
};
const GetTotalResult = (vehicles) => {
    const results = [];
   
    vehicles.forEach(vehicle => {
        const result = GetResultOfSingleVehicle(vehicle);
        results.push(result);
    });
    
    Results.value = results;

};

const confirmDelete = async (id, vehicleId) => {
    if (confirm("Er du sikker på at du vil slette denne registreringen?")) {
        await DeleteRecord(id, vehicleId);
    }
};

onMounted( async () => {
    await GetAllRecords();
    await GetAllVehicles();
});

const goBack = async () => {
    isWatching.value = false;
    await GetAllRecords();
    await GetAllVehicles();
    router.push('/protected')
};

const backgroundStyleOfImage = (result) => ({
  backgroundImage: `url('/assets/img/${result === 'Bil' ? 'car.png' : 'motorBike.png'}')`,
  backgroundPosition: 'center',
  backgroundSize: 'contain',
  backgroundRepeat: 'no-repeat',
});

</script>

<template>
    <div style="text-align: center;">
        <div v-if="loading">Laster data...</div>
        <div v-if="error" class="error">{{ error }}</div>
        <br />
        <BButton v-if="isWatching" @click="goBack">Tilbake</BButton>
        <div v-if="Vehicles != null">
            <BContainer class="vehicle-row-result-container">
                <br/>
                <span v-if="!isWatching"><h4>Dine kjøretøys totale målinger</h4></span>
                <span v-if="isWatching"><h2>{{ Vehicles[0].name }}</h2></span>
                <BRow class="vehicle-row"
                v-for="result in Results" :key="result.id"
                @click="GetRecordsByVehicleId(result.id);">
                    <BCol>
                        <div style=" width: 100%; height: 100%;" :style="backgroundStyleOfImage(result.type)">
                        </div>
                    </BCol>
                    <BCol class="vehicle-row-reult-name">
                        {{ result.brand }} 
                    </BCol>
                    <BCol>
                        <div class="resultColorBox" 
                        :class="
                        result.literPrMil > 1.5 ? 'bad' 
                        : result.literPrMil >= 0.9 ? 'medium' 
                        : result.literPrMil == 0 ? '' : 'good'
                        ">
                        <span style="color: whitesmoke;">{{ !result.literPrMil ? 'Ingen målinger' : parseFloat(result.literPrMil).toFixed(2) }} l/mil</span>
                        <br/> 
                        <span style="color: whitesmoke;">{{ result.costPrLiter == 0 ? 'Ingen målinger' : parseFloat(result.costPrLiter).toFixed(2) }} kr/l</span>
                        </div>
                    </BCol>
                </BRow>
            </BContainer>
            <br/>
        </div>
        <div v-else>
            <h2>Ingen registrerte kjøretøy</h2>
            <h3 v-if="!isWatching">Vill du legge til et nytt kjøretøy? - <BButton pill @click="router.push('/regVehicle')" variant="outline-success">Ja</BButton></h3>
            <h3 v-else-if="isWatching">Kom igang med registreringen</h3>
        </div>
        <br/>
        <span v-if="isWatching" style="text-align: center;">
            <h3>Registrerte fyllinger</h3>
        </span>
        <div v-if="isWatching">
            <BContainer class="vehicle-row-records-container">
                <BRow class="vehicle-row-top">
                    <BCol>
                        Killometer
                    </BCol>
                    <BCol>
                        Fylling
                    </BCol>
                    <BCol>
                        Kostnad
                    </BCol>
                    <BCol>

                    </BCol>
                </BRow>
                <BContainer class="records-container">
                    <BRow v-for="record in FuelRecords" id="noneHover" class="vehicle-row-records">
                        <BCol>
                            {{ record.kilometer }}
                        </BCol>
                        <BCol>
                            {{ record.fuelFilled }}
                        </BCol>
                        <BCol>
                            {{ record.price }}
                        </BCol>
                        <BCol style=" overflow: hidden; padding: 0;">
                            <button class="recordButton" @click="confirmDelete(record.id, record.vehicleId)">Slett</button>
                        </BCol>
                    </BRow>
                </BContainer>
                <BRow class="vehicle-row-bottom">
                </BRow>
            </BContainer>
            <br />
            <span v-if="isWatching" style="text-align: center;">
            <h3>Ny fylling</h3>
            </span>
            <BContainer class="new-record-container">
                <BRow class="vehicle-row-top">
                </BRow>
                <BRow id="noneHover" class="vehicle-row-new-records" style="cursor:default;">
                    <BCol style=" overflow: hidden; padding: 0;">
                        <input class="recordInput" type="text" placeholder="Killometerstand" v-model="FuelRecord.kilometer"/>
                    </BCol>
                    <BCol style=" overflow: hidden; padding: 0;">
                        <input class="recordInput" type="text" placeholder="Liter fylt" v-model="FuelRecord.fuelFilled"/>
                    </BCol>
                    <BCol style=" overflow: hidden; padding: 0;">
                        <input class="recordInput" type="text" placeholder="Betalt" v-model="FuelRecord.price"/>
                    </BCol>
                    <BCol style=" overflow: hidden; padding: 0;">
                        <button class="recordButton" @click="AddRecord">Legg til</button>
                    </BCol>
                </BRow>
                <BRow class="vehicle-row-bottom">
                </BRow>
            </BContainer>
        </div>
        
            
        
    </div>
</template>

<style>


</style>