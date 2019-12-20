<template>
   <div>
        <div class="form-group">
            <label for="date">Date</label>
            <input
            type="Date"
            class="pickup-date"
            id="description"
            placeholder="Please enter date"
            v-model.trim="payIncidentForm.CompletedByDate">
        </div>
            <button v-on:click="payIncident" class="btn btn-primary float-center">Submit Pickup Date</button>   
    </div>
</template>

<script> 
import { APIService } from "@/shared/APIService";
const apiService = new APIService();

export default {
    name: "add-pickup-date",
    data() {
        return {
            payIncidentForm:{
                CompletedByDate: Date,
                incidentId: ""
            },
            ItemFormShow: false, 
        }
    },
    props:{
        id : Number
    },
    methods: {
        async payIncident() {
            try {
                await apiService.putPayIncident(this.payIncidentForm);
                this.$router.push({name: 'dashboard'});
            } catch (error) {
                this.error = error.message;
            }
        }
    },
    created() {
        this.payIncidentForm.incidentId = this.id;
    }
}
</script>

<style scoped>

.form-group {
  justify-content: center;  
  align-content: center;
}
.btn {
    height: 100%;
    font-size: 100%;
    justify-content: center;  
    align-content: center;
  }

.slide-out{
    display: flex;
    align-items: flex-start;
}
</style>