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

        const response = await fetch(`/User/update`, {
            method: 'PUT', 
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                id: userRegistration.id,
                oldUserName: userRegistration.oldUserName,
                newUserName: userRegistration.newUserName,
                currentPassword: userRegistration.oldPassword,
                newPassword: userRegistration.newPasswordRepeat
            })
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(`Feil ved oppdatering (kode: ${errorData})`);
        }

        const data = await response.json();
        localStorage.setItem('userId', data.user.id);
        localStorage.setItem('userName', data.user.username);
        await router.push('/edidt');

    } catch (err) {
        error.value = `Oppdatering mislyktes: ${err}`;
    } finally {
        loading.value = false;
    }
};

const confirmUpdate = async () => {
    if (confirm("Er du sikker på at du vil gjøre denne endringen?")) {
        await EditUser();
    }
};

const goBack = () => {
    router.push('/edidt'); 
};

</script>µ

<template>
    <div v-if="loading">Oppdaterer bruker...</div>
    <div v-if="error" class="error">{{ error }}</div>
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
                            :style="{ backgroundColor: userRegistration.newPassword.length > 0 ? (isPasswordMatch ? 'green' : 'red') : '' }"
                        />
                    </BFormFloatingLabel>

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
