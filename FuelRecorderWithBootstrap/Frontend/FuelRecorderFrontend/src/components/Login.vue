<script setup>
import { BButton, BCol, BContainer, BForm, BFormFloatingLabel, BFormInput, BInputGroup, BRow } from 'bootstrap-vue-next';
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
        const response = await fetch('/User/login', {
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
        localStorage.setItem('userId', data.userId);
        localStorage.setItem('userName', data.userName);
        router.push({name: 'ProtectedPage'});

    } catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}


</script>

<template>
<BContainer >
    <BRow>
        <BCol sm>

        </BCol>
        <BCol class="loginContainer" lg style="box-shadow: 0px 0px 300px 10px; border-radius: 10px; padding: 10px;">
            <BForm>
                <BInputGroup id="login" size="sm" prepend="Brukernavn" >
                    <BFormInput id="floatingEmail" size="sm" type="email"  autocomplete="email" placeholder="E-postaddress" v-model="username" required/>
                </BInputGroup>
                <BInputGroup id="login" size="sm" prepend="Passord" >
                    <BFormInput id="floatingPassword" size="sm" type="password" autocomplete="current-password" placeholder="Skriv inn passord" v-model="password" required/>
                </BInputGroup>
            </BForm>
            <br/>
            <BRow>
                <BCol style="text-align: start;">
                    <BButton style="width: 100%;" size="sm" @click="LoginUser" variant="colorMode">Logg inn</BButton>
                </BCol>
                <BCol style="text-align: end;">
                    <BButton style="width: 100%;" size="sm" @click="router.push({ path: '/regUser', query: { username: username } })" variant="colorMode">Registrer</BButton>
                </BCol>
            </BRow>
        </BCol>
        <BCol sm>

        </BCol>
    </BRow>
</BContainer>
</template>

<style>

</style>