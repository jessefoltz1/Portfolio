<template>
  <b-container class="home">
    <b-row class="text-center">
      <b-col align-v="start"> </b-col>
      <b-col cols="8" align-v="center">
        <img alt="Repair logo" src="../assets/repairlogo.png">
        <h5>Welcome to Repair Tracker</h5>
          <b-col>
          <router-link :to="{ name: 'login'}" tag="b-button" class="btn1">Login</router-link>
          <router-link :to="{ name: 'register'}" tag="b-button"  class="btn2">Register</router-link>
          </b-col>
      </b-col> 
      <b-col align-v="end"></b-col> 
    </b-row>
  </b-container>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'home',
  components: {
    
  }
}
</script>

<style scoped>

#nav {
  padding-bottom: 5%;
}

#nav {

  padding-top: 2%;
  padding-bottom: 1%;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.home{

  padding-top: 10%;
}
.home img {

  width: 40%;

}

 .home .btn1  {
   border-color: green;
   margin: 2%;
   background-color:green;
}

.home .btn1:hover {
  border-color:darkgreen;
  background-color:darkgreen;
}

@media only screen and (max-width: 1024px) {
  .col-8{
    
    padding-top: 30%;

  }
}
@media only screen and (max-width: 500px) {

  .col-8{
    
    padding-top: 30%;

  }

  .home {
    font-size: 200%;
    align-content: center;
  }

  .home img {

  width: 100%;
  }
  

  .btn1 {
    font-size: 50%;
  }

  .btn2 {
    font-size: 50%;
  }

}

</style>