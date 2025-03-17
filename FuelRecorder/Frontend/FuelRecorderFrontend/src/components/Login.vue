<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const username = ref('');
const password = ref('');
const error = ref(null);
const loading = ref(false);

const router = useRouter();

const LoginUser = async () => {
    loading.value = true;
    error.value = null;

    try {
        const response = await fetch('/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                username: username.value,
                password: password.value
            })
        });

        if(!response.ok){
            throw new Error('Innlogging mislyktes');
        }

        const data = await response.json();
        localStorage.setItem('jwtToken', data.token);
        router.push({name: 'ProtectedPage'});

    } catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}

</script>

<template>
    <div>
        <form @submit.prevent="LoginUser">
            <label for="usr">Brukernavn</label>
            <input id="usr" v-model="username" type="text" required/>
            <br />
            <label for="pass">Passord</label>
            <input id="pass" v-model="password" type="password" required/>
            <br />
            <button type="submit" :disabled="loading">Logg inn</button>
            <p v-if="error" class="error">{{ error }}</p>
        </form>
    </div>

</template>

<style scoped>

button {
    width: 30%;
    margin-top: 1em;
    border-radius: 4px;
    background-color: rgb(41, 49, 56);
    font-family: 'Courier New', Courier, monospace;
    color: aliceblue;
    transition-duration: 4s;
}

button:hover {
    background-color: rgba(41, 49, 56, 0.98);
    cursor:grab;
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
}

button:focus {
    border-style: ridge;
    cursor:grabbing;
}

input {
    border-radius: 5px;
}

input:focus {
    background-color: rgb(41, 49, 56);
    color: aliceblue;
}

</style>