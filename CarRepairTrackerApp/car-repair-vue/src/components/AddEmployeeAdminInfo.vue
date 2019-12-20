<template>
   
                <form v-on:submit.prevent="registerEmployee" class="rgForm">
                    
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

                    
                    <div>
                       
                    <select v-model="user.roleName" class="form-group form-control">
                        
                        <option value="" disabled selected>Select User Role</option>
                        <option value="Employee">Employee</option>
                        <option value="Administrator">Administrator</option>
                    </select>
                    </div>
                    


                    <div>
                      <button type="submit" class="btn btn-primary float-center">Register</button>
                    </div>
                 </form>


     
              
                

</template>

<script>
import { APIService } from "@/shared/APIService";
const apiService = new APIService();

export default {
    name: "add-employee-info",
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
                roleName: "",
            }
        }
    },
    methods: {
        async registerEmployee() {
            try {
                await apiService.registerEmployee(this.user);
                this.goDashboard();
            } catch (error) {
                this.error = error.message;
            }
        },
        goDashboard() {
            this.$router.push({name: 'dashboard'});
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


/* Dropdown Button */
.dropbtn {
  background-color: #3498DB;
  color: white;
  padding: 16px;
  font-size: 16px;
  border: none;
  cursor: pointer;
}

/* Dropdown button on hover & focus */
.dropbtn:hover, .dropbtn:focus {
  background-color: #2980B9;
}

/* The container <div> - needed to position the dropdown content */
.dropdown {
  position: relative;
  display: inline-block;
}

/* Dropdown Content (Hidden by Default) */
.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f1f1f1;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
}

/* Links inside the dropdown */
.dropdown-content a {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}

/* Change color of dropdown links on hover */
.dropdown-content a:hover {background-color: #ddd}

/* Show the dropdown menu (use JS to add this class to the .dropdown-content container when the user clicks on the dropdown button) */
.show {display:block;}


</style>