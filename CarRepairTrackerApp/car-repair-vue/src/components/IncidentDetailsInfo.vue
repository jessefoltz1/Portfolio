<template>
<b-row class="detailsCard">
        <b-card
         header-bg-variant="info"
         header-text-variant="white"
         border-variant="info"
         no-body
         style="max-width: 20rem;"
        >
            <template v-slot:header >
                <h4 class="text-center" id="detailsTitle"> <b-button :to="{name:'dashboard'}" id="incidentbackbtn"> &lt; </b-button><strong>  INCIDENT DETAILS</strong></h4>
            </template>
             <b-card-body >
                <b-card-title>{{vehicle.year}} {{vehicle.make}} {{vehicle.model}}</b-card-title>
                <b-card-sub-title class="mb-2"><strong>VIN:</strong> {{vehicle.vin}}</b-card-sub-title>
                <b-list-group flush>
                    <b-list-group-item><strong>COLOR:</strong> {{vehicle.color}} </b-list-group-item>
                    <b-list-group-item><strong>OWNER:</strong> {{customer.firstName}} {{customer.lastName}} </b-list-group-item>
                    <b-list-group-item><strong>SUBMITTED ON:</strong> {{submittedDateShort(incident.submittedDate)}} </b-list-group-item>
                    <b-list-group-item>    
                    <b-card-text>
                        <div><strong><u>DESCRIPTION OF PROBLEM</u></strong></div>
                        <div id="descbox">{{incident.description}}</div>
                    </b-card-text>
                    </b-list-group-item>
                    <b-list-group-item> 
                        <div><strong><u>STATUS</u></strong></div>
                        <div>{{status}}</div>
                    </b-list-group-item>
                    <b-list-group-item><strong>PICK-UP DATE:</strong> {{incident.pickupdate}}</b-list-group-item>
                </b-list-group>
            </b-card-body>
        </b-card>

        <b-card
        header-bg-variant="info"
        header-text-variant="white"
        border-variant="info"
        no-body
        style="max-width: 20rem;"
        >
            <template v-slot:header  id="repairTitle">
                <h4 class="text-center" ><b-button :to="{name:'dashboard'}" id="repairbackbtn">&lt;</b-button><strong>REPAIR DETAILS</strong></h4>
            </template>
                <display-repair-lines :incidentId="incident.id" @updateCosts="updateCosts" ref="repairLines"/>
                <b-list-group flush>
                    <b-list-group-item><strong>FULL REPAIR COST:</strong> ${{costOfPossible}} </b-list-group-item>
                    <b-list-group-item><strong>APPROVED REPAIR COST:</strong> ${{costOfApproved}} </b-list-group-item>
                    <b-list-group-item><strong>REMAINING BALANCE:</strong> ${{remainingCost}} </b-list-group-item>
                    <b-list-group-item v-show="isEmpOrAdmin" id="total-hours"><strong>TOTAL LABOUR: </strong>{{totalHoursRequired}}</b-list-group-item>
                </b-list-group>
            <add-repair-line v-if="isEmpOrAdmin" :id="incident.id" @reload="reload"/>
        </b-card>
       <div v-if="isEmpOrAdmin" class="buttons">
            <add-pickup-date :id="incident.id"  @reload="reload"/>
            <button v-on:click="markComplete" type="submit" class="btn btn-success">Complete</button>
        </div>
    
<b-col cols="2"></b-col>

</b-row>
</template>

<script>
import auth from '@/shared/auth';
import DisplayRepairLines from "@/components/DisplayRepairLines"
import AddRepairLine from "@/components/AddRepairLine"
import { APIService } from "@/shared/APIService"
import AddPickupDate from '@/components/AddPickupDate'
const apiService = new APIService();

export default {
    name: "incident-details-info",
    components: {
        DisplayRepairLines, 
        AddRepairLine,
        AddPickupDate
    },
    data() {
        return {
            customer: {},
            payIncidentForm: {
                incidentId: "",
                CompletedByDate: ""
            },
            costOfPossible: 0,
            costOfApproved: 0,
            totalHoursRequired: 0,
            markCompleteField: {
                incidentId: ''
            }
        }
    },

  
    props: {
        incident: Object,
        vehicle: Object,
        status: String
    },
    computed: {
        isEmpOrAdmin() {
            return auth.isEmpOrAdmin();
        },
        isAdmin() {
            return auth.isAdmin();
        },
        remainingCost() {
            if (this.incident.paid) {
                return 0;
            } else {
                return this.costOfApproved;
            }
        }
    },
    methods: {
        submittedDateShort(date) {
            if (date != null) {
                const dateTime = date.split("T");
                const unformatedDate = dateTime[0].split("-");
                const formatedDate = `${unformatedDate[1]}/${unformatedDate[2]}/${unformatedDate[0]}`;
                return formatedDate;
            }
        },
        async getCustomer() {
            try {
                this.customer = await apiService.getUserById(this.vehicle.userId);
            } catch (error) {
                this.error = error.message;
            }
        },
        async markComplete() {
            try {
                await apiService.markIncidentComplete(this.markCompleteField);
                this.$router.push({name: 'dashboard'});
            } catch (error) {
                this.error = error.message;
            }
        },
        reload() {
            this.$refs.repairLines.getRepairLines(this.incident.id);
        },
        updateCosts(costs) {
            this.costOfPossible = costs.possible;
            this.costOfApproved = costs.approved;
            this.totalHoursRequired = costs.hours;
        }
    },
    created() {
        this.getCustomer();
        this.markCompleteField.incidentId = this.incident.id;
    }
}
</script>

<style scoped>

#incidentbackbtn{

    margin-right: 2%;
}


#repairbackbtn{
    
    margin-right: 8.5%;
}


.card{
    margin-top: 3%;
    width: 100%;
}
#incident-details{

    padding-top: 15%;
}

.detailsCard{
padding-top: 35%;

}
.card-body{

    background-color:floralwhite;
}
.list-group-item{

   background-color:floralwhite;
}
.border-info{

    border-width: 2px;
}

#repairTitle{

    margin-top: 5%;
}
.cost {
    display: flex;
}

.buttons{
    margin-top: 2%;
    margin-left: 16%;
    padding-left: 1%;
}

.btn-success{
    margin-top: 2%;
}

</style>