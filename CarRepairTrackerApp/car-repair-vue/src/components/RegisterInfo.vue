<template>
   
                <form v-on:submit.prevent="register" class="rgForm">
                    
                    <div class="form-group">
                        <label for="firstName">First Name</label>
                        <input
                        type="text"
                        class="form-control"
                        id="firstName"
                        placeholder="First name"
                        v-model.trim="user.firstName">
                    </div>
                       
                    <div class="form-group">
                        <label for="lastName">Last Name</label>
                        <input
                        type="text"
                        class="form-control"
                        id="lastName"
                        placeholder="Last name"
                        v-model.trim="user.lastName">
                    </div>
                           
                    <div class="form-group">
                        <label for="username">Username</label>
                        <input
                        type="text"
                        class="form-control"
                        id="username"
                        placeholder="Username"
                        v-model.trim="user.username">
                    </div>

                    <div class="form-group">
                        <label for="email">Email</label>
                        <input
                        type="text"
                        class="form-control"
                        id="email"
                        placeholder="Email"
                        v-model.trim="user.email">
                    </div>

                    <div class="form-group">
                        <label for="phoneNumber">Phone Number</label>
                        <input
                        type="text"
                        class="form-control"
                        id="phoneNumber"
                        placeholder="XXX-XXX-XXXX"
                        v-model.trim="user.phoneNumber">
                    </div>
                   
                    <div class="form-group">
                        <label for="password">Password</label>
                        <input
                        type="password"
                        class="form-control"
                        id="password"
                        placeholder=" "
                        v-model.trim="user.password">
                    </div>
                            
                    <div class="form-group">
                        <label for="confirmPassword">Confirm Password</label>
                        <input
                        type="password"
                        class="form-control"
                        id="confirmPassword"
                        placeholder=" "
                        v-model.trim="user.confirmPassword">
                    </div>
                      <button type="submit" class="btn btn-primary float-center">Register</button>
                 </form>
     
              
                

</template>

<script>
import auth from "@/shared/auth";
import { APIService } from "@/shared/APIService";
const apiService = new APIService();

export default {
    name: "register-info",
    data() {
        return {
            user:{
                firstName: "",
                lastName: "",
                username: "",
                email: "",
                phoneNumber: "",
                password: "",
                confirmPassword: "",
            }
        }
    },
    methods: {
        async register() {
            try {
                let token = await apiService.register(this.user);
                auth.saveToken(token);
                this.goDashboard();
            } catch (error) {
                this.error = error.message;
            }
        },
        goDashboard() {
            this.$router.push({ name: 'dashboard'});
        }   
    }
}
</script>

<style scoped>

.form-group {
  justify-content: center;  
  align-content: center;
}
.btn {

    font-size: 100%;
  }
</style>