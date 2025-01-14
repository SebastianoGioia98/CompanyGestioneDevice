<template>

    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-5 flex-grow-0">
            <v-btn icon="mdi-arrow-left" @click="changePage('devices/')"
                   variant="text" size="small"
                   class="ml-auto mr-4">
            </v-btn>

            <h1 class="pageTitle mr-10 ">Device</h1>

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
                        <v-list-item-title @click="onBtnEditDeviceClick">Edit Device</v-list-item-title>
                    </v-list-item>
                    <v-list-item class="listItem">
                        <v-list-item-title @click="showAssignUserDialog = true">Assign User</v-list-item-title>
                    </v-list-item>
                    <v-list-item class="listItem">
                        <v-list-item-title @click="showUpdateDeviceDialog = true">Update Device Software</v-list-item-title>
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
                                    {{deviceDetail.lastSoftwareVersion.name}}  {{deviceDetail.lastSoftwareVersion.version}}
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

        <!--    Update Software Version-->
        <v-dialog min-width="500" max-width="400"
                  :modelValue="showUpdateDeviceDialog"
                  persistent>
            <v-card prepend-icon="mdi-update" title="Update Software Device">

                <v-card-text>
                    <v-form v-model="updateSoftwareFormValid" ref="updateSoftwareForm">
                        <v-container>
                            <v-row>
                                <v-col cols="6">
                                    <v-text-field v-model="newSoftwareVersion.name"
                                                  :counter="30"
                                                  label="Software Name"
                                                  :rules="[v => !!v || 'Name is required']"
                                                  variant="solo"
                                                  @input="validateUpdateSoftwareForm"
                                                  required>
                                    </v-text-field>
                                </v-col>

                                <v-col cols="6">
                                    <v-text-field v-model="newSoftwareVersion.version"
                                                  :counter="3"
                                                  label="Sofware Version"
                                                  :rules="[v => !!v || 'Version is required']"
                                                  variant="solo"
                                                  @input="validateUpdateSoftwareForm"
                                                  required>
                                    </v-text-field>
                                </v-col>


                            </v-row>
                        </v-container>
                    </v-form>
                </v-card-text>


                <template v-slot:actions>
                    <v-spacer></v-spacer>

                    <v-btn @click="onUpdateSoftareDeviceDialogClose(false)">
                        Cancel
                    </v-btn>

                    <v-btn @click="onUpdateSoftareDeviceDialogClose(true)" :disabled="!updateSoftwareFormValid">
                        Update
                    </v-btn>
                </template>
            </v-card>
        </v-dialog>

        <!--    Edit Device-->
        <dialog-edit-device v-model="showEditDialog" :featureList="featureList" :device="editDeviceInfo" @close="onDialogEditClose"></dialog-edit-device>


        <!--    Assign user-->
        <v-dialog min-width="500" max-width="400"
                  :modelValue="showAssignUserDialog"
                  persistent>
            <v-card prepend-icon="mdi-pensile" title="Assign User to Device">

                <v-card-text>
                    <v-form v-model="assignUserFormValid" ref="assignUserForm">
                        <v-container>
                            <v-row>
                                <v-col cols="12">
                                    <!--User-->
                                    <v-select label="User"
                                              v-model="assignedUser"
                                              item-title="username" item-value="id"
                                              :items="userList"
                                              :rules="[v => !!v || 'User is required']"
                                              @blur="validateAssignUserForm"
                                              variant="solo-filled">
                                    </v-select>
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-form>
                </v-card-text>


                <template v-slot:actions>
                    <v-spacer></v-spacer>

                    <v-btn @click="onAssignUserDialogClose(false)">
                        Cancel
                    </v-btn>

                    <v-btn @click="onAssignUserDialogClose(true)" :disabled="!assignUserFormValid">
                        Assign User
                    </v-btn>
                </template>
            </v-card>
        </v-dialog>


        <!--    === Snackbar ===   -->
        <snackbar :option="snackbarOpt"></snackbar>

    </v-container>
</template>

<style scoped>
    .listItem:hover {
        cursor: pointer !important;
        background: rgb(var(--v-theme-primary));
    }

    button {
        color: rgb(var(--v-theme-OnPrimary)) !important;
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
                featureList: [],
                userList:[],
                deviceDetail: null,

                //features property
                geolocalization: {
                    latitude: 0,
                    longitude: 0
                },
                newSoftwareVersion: {
                    name: "",
                    version: ""
                },
                editDeviceInfo: {},
                assignedUser:null,

                //state property
                loadingState: false,
                selectedDevice: {},


                //dialog property
                showEditDialog: false,
                showGeolocalizationDialog: false,
                showUpdateDeviceDialog: false,
                showAssignUserDialog:false,

                //form properties
                updateSoftwareFormValid: false,
                assignUserFormValid: false,

                //snackbar
                snackbarOpt: {
                    snackbar: false,
                    text: '',
                    timeout: 2500,
                    color: ''
                }


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

            //get featureList
            services.ApiCallerFeatures
                .getFeatureList().then(res => {
                    //load featureList
                    that.featureList = res.data.items;

                    console.log("featureList : ", that.featureList);
                });


            //get userList
            services.ApiCallerUsers
                .getUsers().then(res => {
                    //load featureList
                    that.userList = res.data.items;

                    console.log("userList : ", that.userList);

                });
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
            onBtnEditDeviceClick() {
                let that = this;

                const filteredFeatures = that.featureList
                    .filter(feature => {
                        const match = that.deviceDetail.features.some(f => f.name === feature.name);
                        console.log('Checking feature:', feature.name, 'Match:', match);
                        return match;
                    })
                    .map(feature => feature.id);

                that.editDeviceInfo = {
                    id: that.deviceDetail.id,
                    name: that.deviceDetail.name,
                    type: that.deviceDetail.type,
                    model: that.deviceDetail.model,
                    deviceFeaturesIds: filteredFeatures
                }

                //console.log("on onBtnEditDeviceClick, device: ", filteredFeatures)
                that.showEditDialog = true;
            },




            //    === menu methods
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
            onUpdateSoftareDeviceDialogClose(state) {
                let that = this;
                console.log("onUpdateDeviceDialogClose, state: ", state);

                //close the dialog
                that.showUpdateDeviceDialog = false;

                //cancel update
                if (state === false) return;


                //prepare data
                let data = {
                    deviceId: that.deviceDetail.id,
                    newSoftwareVersion: {
                        name: that.newSoftwareVersion.name,
                        version: that.newSoftwareVersion.version
                    }
                }

                //update
                console.log("data", data);

                services.ApiCallerDevices
                    .updateSoftareDevice(data).then(res => {
                        console.log("On onUpdateSoftareDeviceDialogClose, res: ", res.data);
                        that.deviceDetail.lastSoftwareVersion = res.data;

                        //launch snackbar
                        that.snackbarOpt = {
                            snackbar: true,
                            text: 'Device software updated successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    });
            },

            onDialogEditClose(e) {
                let that = this;
                console.log("onDialogEditClose, res: ", e);

                that.showEditDialog = false;


                if (e.state === false) return;

                let snackOpt = {};


                //SET Loading State
                that.loadingState = true;

                //call to  addDevice
                services.ApiCallerDevices
                    .editDevice(e.data)
                    .then((res) => {
                        console.log("on ApiCallerDevices.createDevice, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'Device created successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    })
                    .catch((err) => {
                        console.log("on ApiCallerDevices.createDevice, err", err);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'Something got wrong. Please retry later',
                            timeout: 2500,
                            color: 'red'
                        }
                    })
                    .finally(() => {
                        //UNSET Loading State
                        that.loading = false;

                        //chiedo la lista dei device e riempio la deviceList
                        that.refeshDeviceList();

                        //preparo il messaggio per lo snackbar
                        that.snackbarOpt = snackOpt;
                    });

                
            },

            onAssignUserDialogClose(state) {
                let that = this;
                console.log("onAssignUserDialogClose, state: ", state);

                //close the dialog
                that.showAssignUserDialog = false;

                //cancel update
                if (state === false) return;


                //prepare data
                let data = {
                    deviceId: that.deviceDetail.id,
                    userId: that.assignedUser
                }


                let snackOpt = {};


                //SET Loading State
                that.loadingState = true;

                //call to  addDevice
                services.ApiCallerDevices
                    .assignUser(data)
                    .then((res) => {
                        console.log("on ApiCallerDevices.assignUser, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'User Assigned successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    })
                    .catch((err) => {
                        console.log("on ApiCallerDevices.assignUser, err", err);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'Something got wrong. Please retry later',
                            timeout: 2500,
                            color: 'red'
                        }
                    })
                    .finally(() => {
                        //UNSET Loading State
                        that.loading = false;

                        //chiedo la lista dei device e riempio la deviceList
                        that.refeshDeviceList();

                        //preparo il messaggio per lo snackbar
                        that.snackbarOpt = snackOpt;
                    });
            },




            //forms Methods
            validateUpdateSoftwareForm() {
                this.$refs.updateSoftwareForm.validate();
                // console.log("validateUpdateSoftwareForm", this.updateSoftwareFormValid)
            },

            validateAssignUserForm() {
                this.$refs.assignUserForm.validate();
                // console.log("validateAssignUserForm", this.updateSoftwareFormValid)
            },

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

