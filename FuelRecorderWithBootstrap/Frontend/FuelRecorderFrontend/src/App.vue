<script setup>
import { RouterView } from 'vue-router'
import { colorMode } from './components/ColorMode';
</script>

<template>
  <div :class="colorMode.value">
    <RouterView />
  </div>
</template>

<style>

</style>
