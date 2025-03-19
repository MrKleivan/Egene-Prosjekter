<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';
import apiClient from '../axios.ts';

const router = useRouter();
const store = useStore();

const Email = ref('');
const Password = ref('');

const loading = ref(false);

const GetAccess = async () => {
    loading.value = true;
    try {
        const response = await apiClient.post("/login", {
            email: Email.value, 
            password: Password.value
        });

        const token = response.data.tokenType + " " + response.data.accessToken;
        
        store.commit('account/setToken', token);
        localStorage.setItem('token', token);
        
        router.push("/intra");
    }
    catch (error) {
        alert('Feil ved brukernavn eller passord');
        console.log(error);
    }
    finally {
        loading.value = false;
        Email.value = '';
        Password.value = '';
    }
};

</script>

<template>
    <label for="email">Email:</label>
    <input id="email" v-model="Email" type="text"/>
    <br />
    <label for="password">Passord:</label>
    <input id="password" v-model="Password" type="password"/>
    <br />
    <button @click="GetAccess" :disabled="loading">
        {{loading ? 'Logger Inn' : 'Logg Inn'}}
    </button>
</template>

<style scoped>

input {
    border-radius: 5px;
    border: none;
    margin-left: 1em;
}

button {
    width: 30%;
    border-radius: 5px;
    border: none;
    margin-left: 2em;
}

</style>




