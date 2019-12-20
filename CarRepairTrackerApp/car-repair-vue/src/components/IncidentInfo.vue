<template>
        <form v-on:submit.prevent="submitIncident" class="form">
             <h1><strong>New Incident Information</strong></h1>
            <div class="form-group">
                <label for="vin" class="label">VIN</label>
                <input
                type="text"
                class="form-control"
                id="vin"
                placeholder="Vehicle VIN"
                v-model.trim="incident.vin">
            </div>

            <div class="form-group">
                <label for="make" class="label">Make</label>
                <input
                type="text"
                class="form-control"
                id="make"
                placeholder="make"
                v-model.trim="incident.make">
            </div>

            <div class="form-group">
                <label for="vin" class="label">Model</label>
                <input
                type="text"
                class="form-control"
                id="model"
                placeholder="model"
                v-model.trim="incident.model">
            </div>

            <div class="form-group">
                <label for="year" class="label">Year</label>
                <input
                type="text"
                class="form-control"
                id="year"
                placeholder="XXXX"
                v-model.trim.number="incident.year">
            </div>

            <div class="form-group">
                <label for="color" class="label">Color</label>
                <input
                type="text"
                class="form-control"
                id="color"
                placeholder="Color"
                v-model.trim="incident.color">
            </div>

            <div class="form-group">
                <label for="problem description" class="label">Problem Description</label>
                <textarea 
                type="text"
                class="form-control"
                id="description"
                placeholder="Description of problem"
                v-model.trim="incident.description"></textarea>
            </div>

            
            <button type="submit" class="btn btn-primary float-center" center>Submit</button>
        </form>
  
</template>

<script>
    import { APIService } from "@/shared/APIService";
    const apiService = new APIService();

    export default {
     name: "incident-info",
     data() {
        return {
            incident:{
                vin: "",
                make: "",
                model: "",
                year: "",
                color: "",
                description: "",
            }
        }
    },
    methods: {
        async submitIncident() {
            try {
                await apiService.postIncident(this.incident);
                this.$emit("update"); // Tell vue there's a new incident to show.
                this.goDashboard();
            } catch (error) {
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

 .label{
text-transform: uppercase;

}
 
h1{
    text-align: center;
     text-transform:uppercase;
}


.btn {
    
    font-size: 100%;
}

@media only screen and (max-width: 500px) {

h1{
    font-size: 115%;
    padding-top: 15%;
    padding-bottom: 5%;
}

.form-group {
  justify-content: center;  
  align-content: center;
}
}

@media only screen and (min-width: 501px) and (max-width: 768px) {

h1{
    font-size: 135%;
    padding-top: 20%;
    padding-bottom: 2%;
}

.btn-primary{

    min-width: -webkit-fill-available;
}


}


</style>