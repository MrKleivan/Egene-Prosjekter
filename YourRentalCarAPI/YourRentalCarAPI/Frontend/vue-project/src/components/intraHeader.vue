<script setup>
import IntraNavBarNavBar from './IntraNavBar.vue';
</script>

<template>
    <div class="headerWrapper">
        <div class="headerLeft">
            
        </div>
        <div class="headerRight">
            <div class="navBarInHeader">
                <IntraNavBarNavBar />
            </div>
        </div>
    </div>
</template>


<style scoped>

.headerWrapper {
    display: flex;
    width: 100%;
    height: 10vh;
    background: linear-gradient(aliceblue, lightBlue);
}

.headerLeft {
    width: 100%;
    height: 100%;
    margin: auto;
}

.headerRight {
    align-content: end;
    width: 100%;
    height: 100%;
}

.navBarInHeader {
    width: 40%;
    margin: auto;
    text-align: right;
}

</style>