<script setup>
import { useRouter, useRoute } from 'vue-router';
import { BButton, BNavbar, BNavForm } from 'bootstrap-vue-next';
import LogOut from '@/components/LogOut.vue';
import { colorMode, toggleColorMode,  } from '../components/ColorMode'

const router = useRouter();
const route = useRoute();

const goHome = () => {
  if (route.path === '/protected'){
    router.go(0);
    router.replace('/protected');
  } else {
    router.replace('/protected');
  }
};

</script>

<template>
<BNavbar :variant="colorMode === 'light' ? 'light' : colorMode === 'dark' ? 'dark' : 'primary'">
  <BButton class="mx-1" size="sm" @click="goHome" variant="colorMode">Hjem</BButton>
  <BNavForm>
    <LogOut />
    <BButton class="mx-1" size="sm" @click="toggleColorMode" variant="colorMode">
        {{ colorMode === 'light' ? 'Dark Mode' : colorMode === 'dark' ? 'Med Mode' : 'Light Mode' }}
    </BButton>
  </BNavForm>
</BNavbar>
</template>
