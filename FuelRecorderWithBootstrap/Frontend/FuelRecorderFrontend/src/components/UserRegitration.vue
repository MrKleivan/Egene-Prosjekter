<script setup>
import { BButton, BFormFloatingLabel, BFormInput } from 'bootstrap-vue-next';
import { reactive, ref , computed} from 'vue';
import { useRouter, useRoute } from 'vue-router';

const route = useRoute();
const router = useRouter();
const error = ref(null);
const loading = ref(false);

const userRegistration = reactive({
    regUsername: route.query.username || '',
    name: '',
    regPassword: '',
    regRePassword: ''
}
);

const isPasswordMatch = computed(() => {
  return userRegistration.regPassword.length > 0 &&
         userRegistration.regPassword === userRegistration.regRePassword;
}); 

const RegUser = async () => {
    loading.value = true;
    error.value = null;

    if (!userRegistration.regUsername || !userRegistration.regPassword) {
    error.value = "Brukernavn og passord må fylles ut";
    loading.value = false;
    return;
    }

    if (!isPasswordMatch.value) {
    error.value = "Passordene stemmer ikke overens";
    loading.value = false;
    return;
    }

    console.log(JSON.stringify({ id: 0, username: userRegistration.regUsername, passwordHash: userRegistration.regPassword }));

    try {
        const response = await fetch(`/Register/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: 0,
                username: userRegistration.regUsername,
                password: userRegistration.regPassword
            })
        });

        if(!response.ok){
            const errorData = await response.json();
            throw new Error('Registrering mislyktes');
        }

        await router.push('/');

    } catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}




const token = localStorage.getItem('jwtToken');
</script>

<template>

<BContainer class="bv-example-row">
    <div v-if="error">{{ error }}</div>
    <BRow>
        <BCol sm>

        </BCol>
        <BCol sm>
            <BForm>
                <h3>Registrer bruker</h3>
                <BFormFloatingLabel label="Email address/userName" label-for="floatingEmail" class="my-2">
                    <BFormInput id="floatingEmail" type="email"  placeholder="Email address" v-model="userRegistration.regUsername" autocomplete="username"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="Password" label-for="floatingPassword" class="my-2">
                    <BFormInput id="floatingPassword" type="password" placeholder="Password" v-model="userRegistration.regPassword" autocomplete="new-password"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="RepeatPassword" label-for="floatingPasswordRe" class="my-2">
                    <BFormInput id="floatingPasswordRe" 
                    :style="{ backgroundColor: userRegistration.regRePassword.length > 0 ? (isPasswordMatch ? 'green' : 'red') : '' }" 
                    type="password" 
                    placeholder="RepeatPassword" 
                    v-model="userRegistration.regRePassword" 
                    autocomplete="new-password"/>
                </BFormFloatingLabel>
                <BButton size="sm" @click="RegUser" variant="dark">Registrer bruker</BButton>
            </BForm>
        </BCol>
        <BCol sm>

        </BCol>
    </BRow>
</BContainer>
</template>

<style>

</style>