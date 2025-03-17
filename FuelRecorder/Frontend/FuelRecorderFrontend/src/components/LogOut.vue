<script setup>
import { useRouter } from 'vue-router';
const router = useRouter();

const logout = () => {
  // Fjern tokenet fra localStorage
  localStorage.removeItem('jwtToken');
  
  // Naviger brukeren til innloggingssiden (eller en offentlig side)
  router.push({ name: 'login' });
};
</script>

<template>
    <div>
        <button @click="logout"><<</button>
    </div>
</template>

<style scoped>


div {
    width: 5vh;
    height: 5vh;
    align-content: center;
    text-align: center;
    border-radius: 50%;
}

button {
    width: 90%;
    height: 90%;
    border-radius: 50%;
    background-color: rgb(41, 49, 56);
    font-family: 'Courier New', Courier, monospace;
    color: aliceblue;
    font-size: auto;
    font-weight: bolder;
    text-align: center;
    overflow: hidden;
}

button:hover {
    background-color: rgba(41, 49, 56, 0.76);
    cursor:grab;
}

button:focus {
    border-style: ridge;
    cursor:grabbing;
}

</style>