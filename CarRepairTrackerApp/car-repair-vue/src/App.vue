<template>
  <div id="app">
      <b-navbar fixed="top" type="dark" variant="dark" class="navbar">
          <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

          <b-collapse id="nav-collapse" is-nav>
            <b-nav-item-dropdown left >
            <template slot="button-content">
              <img src="@/assets/repairlogoW.png" id="repairLogoWhite">
              </template>
            <b-dropdown-item :to="{name:'dashboard'}">Home</b-dropdown-item>
            <b-dropdown-item :to="{name:'about'}">About</b-dropdown-item>
       
          
          </b-nav-item-dropdown>
            <b-nav-item >
           <b-navbar-brand ><em>Repair Tracker</em></b-navbar-brand>
            </b-nav-item>
          <!-- Navbar dropdowns -->
          <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown v-if="isLoggedIn()" right>
            <template v-slot:button-content>
              <img src="@/assets/user.png" id="userLogo">
            </template>
            <b-dropdown-item v-if="isAdmin" :to="{name:'add-employee'}">Add Employee</b-dropdown-item>
            <b-dropdown-item v-on:click="logOut" >Logout</b-dropdown-item>
          </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
    <router-view @update="updateBool" :update="update"/>
  </div>
</template>

<script>

import auth from "@/shared/auth";
export default {
  data() {
    return {
      update: false
    }
  },
  computed: {
    isEmpOrAdmin() {
      return auth.isEmpOrAdmin();
    },
    isAdmin() {
      return auth.isAdmin();
    }
  },
  methods: {
      logOut(){
        auth.destroyToken();
        this.$router.push("{ name: 'home'}");
      },
      isLoggedIn() {
        return auth.hasToken();
      },
      updateBool() {
        this.update = !this.update;
    }
  }
}
</script>

<style scoped>

/* Desktop css */

@media only screen and (max-width: 1950px) {


.navbar {
padding:0%;
 box-shadow: 0px 10px 5px grey;

}

.navbar-brand {
  padding-left: 200%;
  font-size: 3.25rem;
}

#repairLogoWhite{
width: 90%;
margin-bottom: 0%;
margin-top:0%;
padding-top:0%;
padding-bottom: 5%;
}

#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  /* background-color: bisque; */
  padding-bottom: 10%;
}
}

/* MOBILE CSS */
@media only screen and (max-width: 500px) {

  #app {
  height: 950px;
  background-color: lightgrey;
}
 
 .navbar-brand{
  padding-left: 12%;
   font-size: 1.25rem;
 }

#navbar {
position:fixed;

}
.navbar[data-v-7ba5bd90] {
    padding: 0%;
    margin-top: -5%;
}

.navbar-nav{
    padding-top: 7%;
}

}


@media only screen and (min-width: 501px) and (max-width: 768px) {
#app {
  height: 1024px;
  background-color: lightgrey; 
}


.navbar-brand[data-v-7ba5bd90] {
    padding-left: 65%;
    font-size: 2.25rem;
}

}
@media only screen and (min-width: 769px) and (max-width: 1024px) {

#app {
  height: 1366px;
  background-color:lightgray;
}
.navbar-brand {
  padding-left: 110%;
  font-size: 2.25rem;
}

}



</style>

