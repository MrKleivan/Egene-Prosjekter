<script setup>
import { useRouter } from 'vue-router';
import { onMounted, ref} from 'vue';
import { BButton } from 'bootstrap-vue-next';
const router = useRouter();

const isLoggedInn = () => {
  token.value = localStorage.getItem('jwtToken') || '';
  if (!token){
    return false;
  } else {
    return true;
  }
  
};
const token = ref(isLoggedInn);

onMounted(isLoggedInn)



const logout = () => {
  // Fjern tokenet fra localStorage
  localStorage.removeItem('jwtToken');
  localStorage.removeItem('userId');
  
  // Naviger brukeren til innloggingssiden (eller en offentlig side)
  token.value = '';
  router.push({ name: 'login' });
};
</script>

<template>
        <BButton v-if="token" size="sm" @click="logout" variant="colorMode">Logg ut</BButton>
        <BButton v-if="token" class="mx-1" size="sm" @click="router.push('/edidt')" variant="colorMode">Rediger</BButton>
</template>

<style>


</style>