<template>
    <b-container class="register">
        <b-row class="text-center">
        <b-col></b-col> 
          <b-col cols="8" > 
            <h2> REGISTRATION INFO </h2>
          <register-info/>
          </b-col>
          <b-col></b-col>
        </b-row>
    </b-container>
</template>

<script>

import RegisterInfo from "@/components/RegisterInfo"
export default {
    name: "register",
    components: {
        RegisterInfo
    }    
};
</script>


<style scoped>

.register{

  padding-top: 10%;
}

h2{

  padding: 10%
}





@media only screen and (min-width: 769px) and (max-width: 1024px) {

.register {
  -webkit-text-size-adjust: 115%;
  padding-top: 20%;
}

}

@media only screen and (max-width: 500px) {

  h2 {
    font-size: 150%;
  }

  .register {
    font-size: 70%;
    align-content: center;
  }

  
 
}

@media only screen and (max-width: 414px) {


}

</style>




