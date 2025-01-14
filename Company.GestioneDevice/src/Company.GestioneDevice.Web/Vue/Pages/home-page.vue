<template>
    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-5 flex-grow-0">
           
            <h1 class="pageTitle mr-10 ">Home</h1>

         


        

        </v-row>




        <!--    === Info Section ===   -->
        <v-row no-gutters class="d-flex flex-grow-1 overflow-y-hidden">
            <v-card class="elevation-0 w-100 h-100 d-flex flex-column pa-4" rounded="lg" color="transparent">

                <v-card-text>
           
                        <v-container>
                            <v-row>
                                <v-col cols="12" class="d-flex flex-column align-center">
                                    <!--userame-->
                                    <h2>Welcome to the Device Manager</h2>
                                </v-col>
                            </v-row>


                            <v-row>
                                <v-col cols="12" class="d-flex flex-column align-center">
                                    <!--Name-->
                                   <v-btn @click="goToDevices()"
                                          color="secondary" class="button" >
                                       See Devices
                                   </v-btn>
                                </v-col>

                                <v-col cols="12" class="d-flex flex-column align-center">
                                    <!--Surname-->
                                    <v-btn @click="goToUsers()"
                                           color="secondary" class="button">
                                        See Users
                                    </v-btn>
                                </v-col>
                            </v-row>


                         


                        </v-container>
                   
                </v-card-text>


            </v-card>



        </v-row>







     

    </v-container>
</template>


<style scoped>
    .button {
        width: 18rem;
        height: 8rem;
    }
</style>



<script>
    import { services } from "../../Scripts/Services/serviceBuilder";
    import { useTheme } from 'vuetify';

    export default {
        name: "home-page",
        data: function () {
            const theme = useTheme();
            return {

            };
        },
        props: {

        },
        computed: {

        },
        methods: {

            openContent(link) {
                window.location.href = link;
            },



            goToUsers() {
                let that = this;
                that.openContent("users");
            },


             goToDevices() {
                 let that = this;
                 that.openContent("devices");
            }

        },

    };
</script>

