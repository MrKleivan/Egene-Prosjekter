<script setup>
import Login from '../components/Login.vue'
</script>

<template>
    

        <div class="wrapper">
            <div class="wleft">
                <h1>Logg Inn</h1>
            </div>
            <div class="wright">
                <div class="w-right-login">
                    <Login />
                </div>
            </div>
        </div>

    
</template>

<style scoped>



.wrapper {
    display: flex;
    width: 50%;
    height: 200px;
    border-radius: 50px 50px 50px 50px;
    border-style: inset;
    border-width: 1ex;
    border-color: silver;
    background-color: rgb(41, 49, 56);
    margin: auto;
    margin-top: 20%;
    overflow: hidden;
}

.wleft {
    width: 50%;
    height: 200px;
    align-content: center;
    text-align: center;
    font-family: 'Courier New', Courier, monospace;
    font-size: larger;
    color: linear-gradient(silver, black);
}

.wright {
    width: 50%;
    height: 200px;
    align-content: center;
    overflow: hidden;
    border-top-left-radius: 80%;
    border-bottom-left-radius: 80%;
    background-color: aliceblue;
}

.w-right-login {
    width: 80%;
    text-align: right;
}

</style>