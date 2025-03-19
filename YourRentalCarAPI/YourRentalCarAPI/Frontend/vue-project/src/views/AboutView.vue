<script setup>
import MainHeader from '@/components/MainHeader.vue';
</script>


<template>
  <MainHeader />
  <br>
  <div class="about">
    <h1>Om oss</h1><hr/>
  </div>
</template>

<style scoped>
.about {
  width: 70%;
  height: 705vh;
  margin: auto;
  text-align: center;
}
@media (min-width: 1024px) {
  .about {
    min-height: 75vh;
  }
}
</style>
