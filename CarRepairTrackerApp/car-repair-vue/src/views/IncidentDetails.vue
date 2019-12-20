<template>
<b-container >
    <b-row class="IncidentDetails">
        <b-col></b-col> 
        <b-col cols="10" class="info"> 
        <incident-details-info :incident="incident" :vehicle="vehicle" :status="status"/>
        </b-col>
        <b-col></b-col>
        </b-row>
</b-container>
</template>

<script>
import IncidentDetailsInfo from "@/components/IncidentDetailsInfo"

export default {
    name: "incident-details",
    data() {
        return {
            incident: {},
            vehicle: {},
            status: ""
        }
    },
    components: {
        IncidentDetailsInfo
    },
    created() {
        this.incident = this.$route.params.incident.incident;
        this.vehicle = this.$route.params.incident.vehicle;
        this.status = this.$route.params.incident.status;
    }
}
</script>

<style scoped>

</style>