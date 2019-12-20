<template>
   <div>
       <button v-on:click="ItemFormShow = !ItemFormShow" type="submit" class="btn btn-primary float-center">Add Repair Line</button>
    <form v-on:submit.prevent="addRepairLine" class="rgForm">
        <div v-if="ItemFormShow">
            <div class="form-group">
                <label for="description">Description</label>
                <input
                type="text"
                class="form-control"
                id="description"
                placeholder="Item Description"
                v-model.trim="repairLine.description">
            </div>
                
            <div class="form-group">
                <label for="cost">Cost</label>
                <input
                type="text"
                class="form-control"
                id="cost"
                placeholder="Item Cost"
                v-model.trim="repairLine.cost">
            </div>
                    
            <div class="form-group">
                <label for="timeHours">Time Hours</label>
                <input
                type="text"
                class="form-control"
                id="timeHours"
                placeholder="Time in Hours"
                v-model.trim="repairLine.timeHours">
            </div>
            <button type="submit" class="btn btn-primary float-center">Submit Item Info</button>
        </div>
        </form>
    </div>
</template>

<script>
import { APIService } from "@/shared/APIService";
const apiService = new APIService();

export default {
    name: "add-repair-line",
    data() {
        return {
            repairLine:{
                description: "",
                cost: "",
                timeHours: "",
                incidentId: "",
            },
            ItemFormShow: false, 
        }
    },
    props:{
        id : Number
    },
    methods: {
        async addRepairLine() {
            try {
                await apiService.addRepairLine(this.repairLine);
                this.ItemFormShow = false;
                this.$emit('reload');
            } catch (error) {
                this.error = error.message;
            }
        } 
    },
    created() {
        this.repairLine.incidentId = this.id;
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