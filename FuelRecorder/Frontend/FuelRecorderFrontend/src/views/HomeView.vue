<script setup>
import FuelRecorderView from './FuelRecorderView.vue';
import LogOut from '@/components/LogOut.vue';
</script>

<template>
  <div class="main">
    <LogOut />
    <FuelRecorderView />
  </div>
</template>

<style scoped>

.main {
  width: 80%;
  height: 100vh;
  align-content: center;
  margin: auto;
  padding: 0;
  overflow: scroll;
  background-color: rgba(41, 49, 56, 0.5);
  border-left: outset 1px rgba(41, 49, 56, 0.5);
  border-right: outset 1px rgba(41, 49, 56, 0.5);
  box-shadow: 0 4px 20px 10px rgba(29, 5, 167, 0.03), 0 4px 20px 10px rgba(29, 5, 167, 0.03);
}

.main::-webkit-scrollbar {
    visibility: hidden;
}
.main::-webkit-scrollbar-corner {
    visibility: hidden;
}

</style>
