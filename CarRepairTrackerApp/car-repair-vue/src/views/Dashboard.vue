<template>
    <b-container class="dashboard">

         <!-- Employee and Admin Nav -->
         <b-row class="text-center">
         <b-col v-if="isEmpOrAdmin" cols="2" >
            <b-button-group vertical>
              <b-button class="btn4" @click="changeFilter('Pending')">Pending Incident</b-button>
              <b-button class="btn4" @click="changeFilter('Approved')">Approved Incidents</b-button>
              <b-button class="btn4" @click="changeFilter('In Progress')">In Progress</b-button>
              <b-button class="btn4" @click="changeFilter('Completed')">Completed</b-button>
            </b-button-group> 
        </b-col>
        <!-- Customer Nav -->
        <b-col v-if="!isEmpOrAdmin" cols="1"></b-col>
        <!-- Employee and Admin Display Incidents Area for Buttons Clicked in Nav -->
        <b-col cols="10" >     
            <router-link v-if="!isEmpOrAdmin" :to="{name:'new-incident'}" tag="b-button" class="btn1">New Incident</router-link>
            <b-button v-show="(statusfilter === 'Pending' && !isEmpOrAdmin)" class="btn4" @click="changeFilter('Completed')">Incident History</b-button>
            <b-button v-show="(statusfilter != 'Pending' && !isEmpOrAdmin)" class="btn4" @click="changeFilter('Pending')">Current</b-button>

            <div id="incidents">
              <display-incidents :incidents="filteredIncidents"/>
            </div>    
         </b-col>
        <!-- Empty column. Do not use -->
        <b-col cols="1"></b-col>
         </b-row>
         
    </b-container>

</template>

<script>
import auth from '@/shared/auth';
import DisplayIncidents from "@/components/DisplayIncidents"
import { APIService } from "@/shared/APIService"
const apiService = new APIService();

export default {
    name: "customer-incidents",
    components: {
        DisplayIncidents
    },
    computed: {
        isEmpOrAdmin() {
            return auth.isEmpOrAdmin();
        },
        isAdmin() {
            return auth.isAdmin();
        },
        filteredIncidents() {
          if (this.statusfilter === "") {
            return this.incidents;
          } else {
            return this.filterIncidents(this.statusfilter, this.incidents)
          }
        }
    },
    props: {
      update: Boolean
    },
    data() {
        return {
            incidents: [],
            statusfilter: "Pending"
        }
    },
    methods: {
        async getCustomerIncidents() {
            try {
                this.incidents = await apiService.getUserIncidents();
            } catch (error) {
                this.error = error.message;
            }
        },
        async getIncidents() {
            try {
                this.incidents = await apiService.getIncidents();
            } catch (error) {
                this.error = error.message;
            }
        },
        updateIncidents() {
          if (!this.isEmpOrAdmin){
            this.getCustomerIncidents();
          } else {
            this.getIncidents();
          }
        },
        filterIncidents(filter, incidents) {
          let filteredList = [];
          incidents.forEach(incident => {
            if (filter === "Pending") {
              if (incident.status != "Completed") {
                filteredList.push(incident);
              }
            } else if(filter === "Approved") {
              if (incident.status != "Completed" && incident.status != "Awaiting Evaluation" 
                && incident.status != "Awaiting Approval") {
                  filteredList.push(incident);
                }
            } else if(incident.status === filter || filter === "") {
              filteredList.push(incident);
            }
          });
          return filteredList;
        },
        changeFilter(newFilter) {
          this.statusfilter = newFilter;
        }
    },
    created() {
      this.updateIncidents();
    }
}
</script>

<style scoped>

.col-10{

  padding-top:12%;
}

.customer-incidents{
  border-color: red;
}

 .btn4 {
    border-radius: 5px;
    padding-left: 5%;
    padding-right: 5%;
  }
@media only screen and (max-width: 500px) {
 
 .dashNav{
     width: 100%;

 }
  
 
 .btn1 {
   background-color:limegreen;
   border-color: limegreen;
   border-radius: 5px;  
   padding-left: 5%;
   padding-right: 5%;
  }

.btn1:hover{
  background-color:darkgreen;
  border-color:darkgreen;
}

}
@media only screen and (max-width: 768px){
  .col-10 {
      padding-top: 30%;
    
  }
}

</style>

