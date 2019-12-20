<template>
    <div id="display-incidents">
        <router-link v-for="incident in incidents" :key="incident.id" v-bind:to="{ name: 'incident-details', params: { incident: incident } }" tag="div" class="incident">
            <div class="make" id="cardfield"><strong>MAKE:</strong> <br> {{incident.vehicle.make}}</div>
            <div class="model" id="cardfield"><strong>MODEL:</strong> <br>{{incident.vehicle.model}}</div>
            <div class="submitted" id="cardfield"><strong>SUBMITTED:</strong> <br>{{submittedDateShort(incident.incident.submittedDate)}}</div>
            <div class="status" id="cardfield"><strong>STATUS:</strong> <br>{{incident.status}}</div>
        </router-link>
    </div>
</template>

<script>

export default {
    name: "display-incidents",
    components: {
    },
    props: {
        incidents: Array
    },
    methods: {
        submittedDateShort(date) {
            const dateTime = date.split("T");
            const unformatedDate = dateTime[0].split("-");
            const formatedDate = `${unformatedDate[1]}/${unformatedDate[2]}/${unformatedDate[0]}`;
            return formatedDate;
        },
    }
}
</script>

<style scoped>
.incident {
    display: grid;
    grid-template-columns: 1fr 1fr;
    grid-template-areas: "make submitted"
                         "model status";
    margin-top: 9%;
    margin-bottom: 5%;
    padding-top: 3%;
    padding-bottom: 3%;
    border-radius: 8px;
    border-style: solid;
    border-width: 2px;
    border-color:#17a2b8;
    box-shadow: 10px 10px 5px grey;
    box-sizing: content-box;
    width:50%;
    background-color: darkgrey;
    height: auto;

}


.make {
    grid-area: make;
    padding-top: 5%;
    padding-bottom: 3%;
    background-color: grey;
    border-left-style: solid;
    border-left-color: #17a2b8;
    border-left-width: 10px;
    border-top-left-radius: 10%;
    text-decoration-color: white;
 
}

.submitted {
    grid-area: submitted;
    padding-top: 5%;
    padding-bottom: 3%;
    border-right-style: solid;
    border-right-color: #17a2b8;
    border-right-width: 10px;
    border-top-right-radius: 10%;

}

.model {
    grid-area: model;
    padding-bottom: 3%;
    border-left-style: solid;
    border-left-color: #17a2b8;
    border-left-width: 10px;
    border-bottom-left-radius: 10%;
    
  
}

.status {
    grid-area: status;
    padding-bottom: 3%;
    padding-left: 1%;
    border-right-style: solid;
    border-right-color: #17a2b8;
    border-right-width: 10px;
    border-bottom-right-radius: 10%;
}

#cardfield {
    
    -webkit-text-fill-color: black;
    background-color: floralwhite;

}
</style>