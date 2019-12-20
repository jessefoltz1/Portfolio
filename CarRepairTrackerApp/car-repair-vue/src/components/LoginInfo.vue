<template>
    <section id="login">
        <form v-on:submit.prevent="login" >
            <div class="form-group">
                <label for="loginCredentials">Username</label>
                <input
                type="text"
                class="form-control"
                id="username"
                v-model.trim="user.loginCredentials">
                </div>
                
            <div class="form-group">
                <label for="password">Password</label>
                <input
                type="password"
                class="form-control"
                id="password"
                v-model.trim="user.password">
            </div>
            <button type="submit" class="btn btn-primary float-center">Login</button>
        </form>
        <error-message v-bind:error="error"></error-message>
    </section>
</template>

<script>
import ErrorMessage from "@/components/ErrorMessage.vue";
import auth from "@/shared/auth";
import { APIService } from "@/shared/APIService"
const apiService = new APIService();

export default {
    name: "login-info",
    components: {
        ErrorMessage
    },
    data() {
        return {
            error: "",
            user:{
                loginCredentials: "",
                password: "",
            }
        }
    },
    methods: {

        /**
         * Logs the user in and then sends them to the dashboard.
         * NOTE: Uses async/await
         */
        async login() {
            try {
                let token = await apiService.login(this.user);
                auth.saveToken(token);
                this.goDashboard();
            } catch (error) {
                window.console.log(error.message);
                this.error = error.message;
            }
        },
        goDashboard() {
            this.$router.push({name: 'dashboard'});
        }   
    }

};
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
