<script setup lang="ts">
import { BButton } from 'bootstrap-vue-next';
import { reactive, ref , computed} from 'vue';
import { useRouter, useRoute } from 'vue-router';

const route = useRoute();
const router = useRouter();
const error = ref<string | null>(null);
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

const EdidtUser = async () => {
    loading.value = true;
    error.value = null;

    if (!isPasswordMatch.value) {
    error.value = "Passordene stemmer ikke overens";
    loading.value = false;
    return;
    }

    try {
        const response = await fetch('/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                username: userRegistration.regUsername,
                passwordHash: userRegistration.regPassword
            })
        });

        if(!response.ok){
            throw new Error('Registrering mislyktes');
        }

        const data = await response.json();
        await router.push('/');

    } catch (err: any) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
}

const goBack = () => {
    router.push('/edidt')
}


const token = localStorage.getItem('jwtToken');
</script>

<template>

<BContainer class="bv-example-row">
    <BRow>
        <BCol sm>

        </BCol>
        <BCol sm>
            <BForm>
                <h3>Registrer bruker</h3>
                <BFormFloatingLabel label="Email address/userName" label-for="floatingEmail" class="my-2">
                    <BFormInput id="floatingEmail" type="email"  placeholder="Email address" v-model="userRegistration.regUsername"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="Name" label-for="floatingName" class="my-2">
                    <BFormInput id="floatingName" type="text" placeholder="Name" v-model="userRegistration.name"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="Password" label-for="floatingPassword" class="my-2">
                    <BFormInput id="floatingPassword" type="password" placeholder="Password" v-model="userRegistration.regPassword"/>
                </BFormFloatingLabel>
                <BFormFloatingLabel label="RepeatPassword" label-for="floatingPasswordRe" class="my-2">
                    <BFormInput id="floatingPasswordRe" :style="{ backgroundColor: userRegistration.regRePassword.length > 0 ? (isPasswordMatch ? 'green' : 'red') : 'transparent' }" type="password" placeholder="PasswordRe" v-model="userRegistration.regRePassword"/>
                </BFormFloatingLabel>
                <BButton pill class="mx-2" @click="EdidtUser" variant="dark">Lagre</BButton>
                <BButton pill class="mx-2" @click="goBack" variant="dark">Tilbak</BButton>
            </BForm>
        </BCol>
        <BCol sm>

        </BCol>
    </BRow>
</BContainer>
</template>

<style>

</style>