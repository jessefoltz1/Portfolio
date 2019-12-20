<template>
<b-container >
    <b-row class="newform">
      <b-col></b-col> 
        <b-col cols="10" class="info">  
        <incident-info @update="emitUpdate"/>
        </b-col>
     <b-col></b-col>
    </b-row>
</b-container>
</template>

<script>
import IncidentInfo from "@/components/IncidentInfo"


export default {
    name: "new-incident",
    components: {
        IncidentInfo
      
    },
    methods: {
        emitUpdate() {
            this.$emit("update");
        }
    }
};
</script>

<style scoped>
.newform{
justify-content: center;
}
.row {

    margin-top: 5%;
}
@media only screen and (max-width: 500px) {
.newform {
    -ms-text-size-adjust: 30%;
    padding-top: 15%;

}
}

</style>