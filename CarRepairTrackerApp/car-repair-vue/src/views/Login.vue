<template>
     <b-container class="login">
        <b-row class="text-center">
          <b-col></b-col>
            <b-col cols="8">
            <img alt="Repair logo" src="../assets/repairlogo.png">
            <login-info/>
            </b-col>
          <b-col></b-col>
        </b-row>
    </b-container>
</template>
    

<script>


import LoginInfo from "@/components/LoginInfo"

export default {
    name: "login",
    components: {
        LoginInfo
    }    
};
</script>

<style scoped>

.login img {
  width: 40%;
}

.col-8{

  padding-top: 10%;
}

@media only screen and (max-width: 1024px) {

  .col-8{

    padding-top: 30%;
  }
}

@media only screen and (max-width: 500px) {

  .col-8{

    padding-top: 20%;
  }

  .login {
    font-size: 100%;
    align-content: center;
  }

  .login img {
  width: 100%;
  }
  
}

</style>