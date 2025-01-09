<template>

    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-5 flex-grow-0">
            <v-btn icon="mdi-arrow-left" @click="changePage('devices/')"
                   variant="text" size="small"
                   class="mr-4">
            </v-btn>

            <h1 class="pageTitle mr-10 ml-auto">Device</h1>

            <v-select :items="deviceList" v-model="selectedDevice" @update:modelValue="changeDevice"
                      item-title="name" item-value="id"
                      hide-details variant="solo-filled" density="compact"
                      max-width="200" class="mr-6">
            </v-select>


            <!--menu-->
            <v-btn id="menu-activator" color="primary" class="mr-auto" density="comfortable" icon="mdi-menu">
            </v-btn>

            <v-menu activator="#menu-activator">
                <v-list lines="two">
                    <v-list-item class="listItem">
                        <v-list-item-title @click="onClick">Edit Device</v-list-item-title>
                    </v-list-item>
                    <v-list-item class="listItem">
                        <v-list-item-title>Assign User</v-list-item-title>
                    </v-list-item>
                    <v-list-item class="listItem">
                        <v-list-item-title>Update Device Software</v-list-item-title>
                    </v-list-item>
                    <v-list-item class="listItem">
                        <v-list-item-title @click="getDeviceGeolocalization">Find Device</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>



        </v-row>




        <!--    === Info Section ===   -->
        <v-row no-gutters class="d-flex flex-grow-1 overflow-y-hidden">
            <v-card v-if="deviceDetail !== null" class="elevation-0 w-100 h-100 d-flex flex-column pa-4" rounded="lg" color="transparent">
                <v-row class="h-100">
                    <v-col cols="7" class="d-flex flex-column">
                        <v-row>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Type</div>
                                <div class="text-h5 opacity-80">{{getDeviceType(deviceDetail.type)}}</div>
                            </v-col>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Model</div>
                                <div class="text-h5 opacity-80">{{deviceDetail.model}}</div>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Software Version</div>
                                <div class="text-h5 opacity-80">
                                    {{deviceDetail.lastSoftwareVersion.name}}
                                </div>
                            </v-col>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Holder</div>
                                <div class="text-h5 opacity-80">{{deviceDetail.user.username}}</div>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Create Time</div>
                                <div class="text-h5 opacity-80">
                                    {{getDate(deviceDetail.creationTime)}}
                                </div>
                            </v-col>

                        </v-row>

                    </v-col>

                    <v-col cols="5" class="h-100 d-flex">
                        <v-row class="h-100">
                            <v-col cols="12" class="h-100 d-flex flex-column">
                                <div class="flex-grow-0 text-h5  font-weight-bold">Features</div>

                                <div class="h-100 d-flex flex-column overflow-y-auto">
                                    <div v-for="(feature, idx) in deviceDetail.features" class="w-100 d-flex align-center mt-3">
                                        <div class="text-h5 opacity-80 mr-auto">
                                            {{feature.name}}
                                        </div>
                                        <v-switch v-model="feature.state" disabled hide-details>
                                        </v-switch>
                                    </div>
                                </div>

                            </v-col>
                        </v-row>

                    </v-col>

                </v-row>




            </v-card>



        </v-row>







        <!--    === Dialogs ===   -->

<!--    Geolocalization   -->
        <v-dialog min-width="350" max-width="400"
                  :modelValue="showGeolocalizationDialog"
                  persistent>
            <v-card prepend-icon="mdi-map-marker" title="Your Device is Here">
               
                <v-card-text>
                    latitude: {{geolocalization.latitude}}, longitude: {{geolocalization.longitude}}
                </v-card-text>


                <template v-slot:actions>
                    <v-spacer></v-spacer>
                    <v-btn @click="showGeolocalizationDialog = false">
                        OK
                    </v-btn>
                </template>
            </v-card>
        </v-dialog>




    </v-container>
</template>

<style scoped>
    .listItem:hover {
        cursor: pointer !important;
        background: rgb(var(--v-theme-primary));
    }
</style>


<script>
    import { services } from "../../Scripts/Services/serviceBuilder";
    import { useTheme } from 'vuetify';

    export default {
        name: "device-page",
        data: function () {
            const theme = useTheme();
            return {
                deviceList: [],
                deviceDetail: null,
                geolocalization: {
                    latitude: 0,
                    longitude: 0
                },



                //state property
                loadingState: false,
                selectedDevice: {},


                //dialog property
                showEditDialog: false,
                showGeolocalizationDialog:false,
               

                //menu properties




            };
        },
        props: {
            deviceId: null
        },
        computed: {

        },
        created() {
            let that = this;
            //chiedo la lista dei device e riempio la deviceList
            that.refeshDeviceList();
        },
        mounted() {
            let that = this;

        },
        methods: {


            //   === Select methods
            refeshDeviceList() {
                console.log("ON getDevices");


                let that = this;

                //SET Loading State
                that.loading = true;

                //call to  getDevices
                services.ApiCallerDevices
                    .getDevices().then(res => {
                        //load deviceList
                        that.deviceList = res.data;
                        that.totalItems = that.deviceList.length;
                        console.log("deviceList: ", that.deviceList);
                        console.log("totalItems: ", that.totalItems);

                        //dopo setto il selectedDevice
                        console.log("OnMounted, deviceId: ", that.deviceId);
                        if (that.deviceId !== null) {
                            that.selectedDevice = that.deviceList.find(x => x.id == that.deviceId);
                            console.log("selectedDevice: ", that.selectedDevice)
                        }

                        //scarico il contenuto nel dettaglio
                        if (that.selectedDevice !== null) {
                            services.ApiCallerDevices
                                .getDeviceDetails(that.deviceId).then(res => {
                                    that.deviceDetail = res.data;
                                    console.log("On getDeviceDetails, res: ", that.deviceDetail);
                                });
                        }

                    }).finally(_ => {
                        //UNSET Loading State
                        that.loading = false;


                    });
            },

            changeDevice() {
                let that = this;
                //Redirect to retrigger API
                this.changePage("devices/" + that.selectedDevice);
            },



            //   === btn methods





            //    === menu methods
            onClick() {
                console.log("CLICKED")
            },

            getDeviceGeolocalization() {
                let that = this;
                services.ApiCallerDevices
                    .getDeviceGeolocalization(that.deviceDetail.id).then(res => {
                        console.log("On getDeviceGeolocalization, res: ", res.data);
                        that.geolocalization = res.data;
                        that.showGeolocalizationDialog = true;
                    });
            },




            //    === dialog methods







            //    === other mothods
            getDate(dateString) {
                return services.formatDateTime(dateString);
            },

            getDeviceType(type) {
                return services.getDeviceType(type);
            },

            changePage(destination) {
                window.location.href = destination;
            },
        },

    };
</script>

