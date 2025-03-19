<script setup>
import { useRouter, useRoute } from 'vue-router';
import { BButton, BCol, BNavbar, BNavForm, BRow } from 'bootstrap-vue-next';
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
  <header>
        <BNavbar>
          <BButton class="mx-1" size="sm" @click="goHome" variant="colorMode">Hjem</BButton>
          <BNavForm>
            <LogOut />
            <BButton pill class="mx-1" size="sm" @click="toggleColorMode" variant="colorMode">
                Color mode: {{ colorMode === 'light' ? 'Dark' : colorMode === 'dark' ? 'Med' : 'Light' }}
            </BButton>
          </BNavForm>
        </BNavbar>
  </header>
  <br/>
</template>

<style>

</style>
