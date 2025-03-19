<script setup>
import { BButton, BFormFloatingLabel, BFormInput, BContainer, BRow, BCol, BForm } from 'bootstrap-vue-next';
import { reactive, ref, computed } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const route = useRoute();
const router = useRouter();
const error = ref(null);
const loading = ref(false);

const userRegistration = reactive({
    id: localStorage.getItem('userId') || '',  // Henter bruker-ID fra localStorage
    oldUserName: localStorage.getItem('userName') || '',
    newUserName: '',
    oldPassword: '',
    newPassword: '',
    newPasswordRepeat: ''
});

const isPasswordMatch = computed(() => 
    (userRegistration.newPassword.length > 0 && userRegistration.newPassword === userRegistration.newPasswordRepeat)
    || ( userRegistration.newPassword.length < 1)
);

const EditUser = async () => {
    loading.value = true;
    error.value = null;

    if (!isPasswordMatch.value) {
        error.value = "Passordene stemmer ikke overens";
        loading.value = false;
        return;
    }

    try {
        const token = localStorage.getItem('jwtToken'); // Henter token i funksjonen

        const response = await fetch(`/Register/update`, {
            method: 'PUT', 
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                id: userRegistration.id,
                newUsername: userRegistration.newUserName,
                currentPassword: userRegistration.oldPassword,
                newPassword: userRegistration.newPasswordRepeat
            })
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Oppdatering mislyktes');
        }
        
        await router.push('/');

    } catch (err) {
        error.value = err.message;
    } finally {
        loading.value = false;
    }
};

const confirmUpdate = async () => {
    if (confirm("Er du sikker på at du vil gjøre denne endringen?")) {
        await EditUser();
        router.push('/edidt');
    }
};

const goBack = () => {
    router.push('/edidt'); 
    console.log(localStorage.getItem('userName'))
};

</script>µ

<template>
    <BContainer class="bv-example-row">
        <BRow>
            <BCol sm></BCol>
            <BCol sm>
                <BForm>
                    <h3>Oppdater bruker</h3>
                    
                    <BFormFloatingLabel label="Eksisterende Brukernavn" label-for="OldUserName" class="my-2">
                        <BFormInput id="OldUserName" type="email" placeholder="Email address" v-model="userRegistration.oldUserName" autocomplete="oldUsername"/>
                    </BFormFloatingLabel>
                    <BFormFloatingLabel label="Nytt Brukernavn" label-for="NewUserName" class="my-2">
                        <BFormInput id="NewUserName" type="email" placeholder="Email address" v-model="userRegistration.newUserName" autocomplete="newUsername"/>
                    </BFormFloatingLabel>

                    <BFormFloatingLabel label="Gjeldene Passord" label-for="floatingPassword" class="my-2">
                        <BFormInput id="floatingPassword" type="password" autocomplete="current-password" placeholder="Nåværende passord" v-model="userRegistration.oldPassword"/>
                    </BFormFloatingLabel>

                    <BFormFloatingLabel label="Nytt passord" label-for="newPassword" class="my-2">
                        <BFormInput 
                            id="newPassword" 
                            type="password" 
                            placeholder="Nytt passord" 
                            v-model="userRegistration.newPassword"
                            autocomplete="new-password"
                        />
                    </BFormFloatingLabel>

                    <BFormFloatingLabel label="Gjenta nytt passord" label-for="newPasswordRepeat" class="my-2">
                        <BFormInput 
                            id="newPasswordRepeat" 
                            type="password" 
                            placeholder="Gjenta nytt passord" 
                            v-model="userRegistration.newPasswordRepeat"
                            autocomplete="new-password-repeat"
                            :style="{ backgroundColor: userRegistration.newPassword.length > 0 ? (isPasswordMatch ? 'green' : 'red') : 'transparent' }"
                        />
                    </BFormFloatingLabel>

                    <p v-if="error" class="text-danger">{{ error }}</p>

                    <BButton pill class="mx-2" @click="confirmUpdate" variant="dark" :disabled="loading">
                        {{ loading ? 'Lagrer...' : 'Lagre' }}
                    </BButton>
                    <BButton pill class="mx-2" @click="goBack" variant="dark">Tilbake</BButton>
                </BForm>
            </BCol>
            <BCol sm></BCol>
        </BRow>
    </BContainer>
</template>
