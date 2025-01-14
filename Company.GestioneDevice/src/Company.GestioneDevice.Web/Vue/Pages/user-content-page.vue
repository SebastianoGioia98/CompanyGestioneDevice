<template>

    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-5 flex-grow-0">
            <v-btn icon="mdi-arrow-left" @click="changePage('users/')"
                   variant="text" size="small"
                   class="ml-auto mr-4">
            </v-btn>

            <h1 class="pageTitle mr-10 ">User</h1>

            <v-select :items="userList" v-model="selectedUser" @update:modelValue="changeUser"
                      item-title="username" item-value="id"
                      hide-details variant="solo-filled" density="compact"
                      max-width="200" class="mr-6">
            </v-select>


            <!--menu-->
            <v-btn id="menu-activator" color="primary" class="mr-auto" density="comfortable" icon="mdi-menu">
            </v-btn>

            <v-menu activator="#menu-activator">
                <v-list lines="two">
                    <v-list-item class="listItem">
                        <v-list-item-title @click="showEditDialog = true">Edit User</v-list-item-title>
                    </v-list-item>
                    <v-list-item class="listItem">
                        <v-list-item-title @click="showAssignUserPoliciesDialog = true">Assign User Policies</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>

        </v-row>




        <!--    === Info Section ===   -->
        <v-row no-gutters class="d-flex flex-grow-1 overflow-y-hidden">
            <v-card v-if="userDetail !== null" class="elevation-0 w-100 h-100 d-flex flex-column pa-4" rounded="lg" color="transparent">
                <v-row class="h-100">
                    <v-col cols="7" class="d-flex flex-column">
                        <v-row>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Name</div>
                                <div class="text-h5 opacity-80">{{userDetail.name}}</div>
                            </v-col>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Surname</div>
                                <div class="text-h5 opacity-80">{{userDetail.surname}}</div>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="12" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Email</div>
                                <div class="text-h5 opacity-80">
                                    {{userDetail.email}}
                                </div>
                            </v-col>

                        </v-row>

                        <v-row>
                            <v-col cols="6" class="d-flex flex-column">
                                <div class="text-h5 font-weight-bold">Create Time</div>
                                <div class="text-h5 opacity-80">
                                    {{getDate(userDetail.creationTime)}}
                                </div>
                            </v-col>

                        </v-row>

                    </v-col>

                    <v-col cols="5" class="h-100 d-flex">
                        <v-row class="h-100">
                            <v-col cols="12" class="h-100 d-flex flex-column">
                                <div class="flex-grow-0 text-h5  font-weight-bold">Active Policies</div>

                                <div class="h-100 d-flex flex-column overflow-y-auto">
                                    <div v-for="(policie, idx) in userDetail.policies" class="w-100 d-flex align-center mt-3">
                                        <div class="text-h5 opacity-80 mr-auto">
                                            {{policie.name}}
                                        </div>

                                    </div>
                                </div>

                            </v-col>
                        </v-row>

                    </v-col>

                </v-row>




            </v-card>



        </v-row>







        <!--    === Dialogs ===   -->
        <!--    Edit Device-->
        <dialog-edit-user v-model="showEditDialog"  :user="userDetail" @close="onDialogEditClose"></dialog-edit-user>
        
        <!--    Assign User Policies-->
        <v-dialog min-width="500" max-width="400"
                  :modelValue="showAssignUserPoliciesDialog"
                  persistent>
            <v-card prepend-icon="mdi-account-cog" title="Assign User Policies">

                <v-card-text>
                    <v-form v-model="assignUserPolicieFormValid" ref="assignUserForm">
                        <v-container>
                            <v-row>
                                <v-col cols="12">
                                    <!--User-->
                                    <v-select label="Policies"
                                              v-model="assignedPoliciesIds"
                                              item-title="name" item-value="id"
                                              :items="policyList"
                                              :rules="[v => Array.isArray(v) && v.length > 0 || 'At least one feature is required']"
                                              multiple
                                              @blur="validateAssignUserForm"
                                              required
                                              variant="solo-filled">
                                    </v-select>
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-form>
                </v-card-text>


                <template v-slot:actions>
                    <v-spacer></v-spacer>

                    <v-btn @click="onAssignUserPoliciesDialogClose(false)">
                        Cancel
                    </v-btn>

                    <v-btn @click="onAssignUserPoliciesDialogClose(true)">
                        Assign Policies
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
</style>


<script>
    import { services } from "../../Scripts/Services/serviceBuilder";
    import { useTheme } from 'vuetify';

    export default {
        name: "user-content-page",
        data: function () {
            const theme = useTheme();
            return {
                userList: [],
                userDetail: null,
                policyList: [],
               

                //features properties
                assignedPoliciesIds: [],

                //state property
                loadingState: false,
                selectedUser: {},


                //dialog property
                showEditDialog: false,
                showAssignUserPoliciesDialog:false,
              
                //form property
                assignUserPolicieFormValid: false,
               

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
            userId: null
        },
        computed: {

        },
        created() {
            let that = this;
            //chiedo la lista dei device e riempio la userList
            that.refeshUserList();

            //get policyList
            services.ApiCallerPolicies
                .getPolicyList().then(res => {
                    //load featureList
                    that.policyList = res.data.items;

                    console.log("policyList : ", that.policyList);
                });
        },
        mounted() {
            let that = this;

        },
        methods: {


            //   === Select methods
            refeshUserList() {
                console.log("ON refeshUserList");


                let that = this;

                //SET Loading State
                that.loading = true;

                //call to  getUsers
                services.ApiCallerUsers
                    .getUsers().then(res => {
                        //load userList
                        that.userList = res.data.items;
                        that.totalItems = res.data.totalCount;
                        console.log("userList: ", that.userList);
                        console.log("totalItems: ", that.totalItems);

                        //dopo setto il selectedUser
                        if (that.userId !== null) {
                            that.selectedUser = that.userList.find(x => x.id == that.userId);
                            console.log("selectedUser: ", that.selectedUser)
                        }

                        //scarico il contenuto nel dettaglio
                        if (that.selectedUser !== null) {
                            services.ApiCallerUsers
                                .getUserDetails(that.userId).then(res => {
                                    that.userDetail = res.data;
                                    console.log("On getUserDetails, res: ", that.userDetail);

                                    //update assigned Policies
                                    that.assignedPoliciesIds = that.policyList
                                        .filter(policie => {
                                            const match = that.userDetail.policies.some(p => p.name === policie.name);
                                            console.log('Checking policy:', policie.name, 'Match:', match);
                                            return match;
                                        })
                                        .map(policie => policie.id);
                                });
                        }

                    }).finally(_ => {
                        //UNSET Loading State
                        that.loading = false;


                    });
            },

            changeUser() {
                let that = this;
                //Redirect to retrigger API
                this.changePage("users/" + that.selectedUser);
            },



            //   === btn methods












            //    === dialog methods
            onAssignUserPoliciesDialogClose(state) {
                let that = this;
                console.log("onAssignUserPoliciesDialogClose, state: ", state);

                //close the dialog
                that.showAssignUserPoliciesDialog = false;

                //cancel update
                if (state === false) return;


                //prepare data
                let data = {
                    id: that.userDetail.id,
                    policyIds: that.assignedPoliciesIds
                }
                console.log("data: ", data);

                let snackOpt = {};


                //SET Loading State
                that.loadingState = true;

                //call to assignUserPolicies
                services.ApiCallerUsers
                    .assignUserPolicies(data)
                    .then((res) => {
                        console.log("on ApiCallerUsers.assignUserPolicies, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'User Policies Assigned successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    })
                    .catch((err) => {
                        console.log("on ApiCallerUsers.assignUserPolicies, err", err);

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
                        that.refeshUserList();

                        //preparo il messaggio per lo snackbar
                        that.snackbarOpt = snackOpt;
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

                //call to  editUser
                services.ApiCallerUsers
                    .editUser(e.data)
                    .then((res) => {
                        console.log("on ApiCallerUsers.editUser, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'User edited successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    })
                    .catch((err) => {
                        console.log("on ApiCallerUsers.editUser, err", err);

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
                        that.refeshUserList();

                        //preparo il messaggio per lo snackbar
                        that.snackbarOpt = snackOpt;
                    });

            },



            //forms Methods
            validateAssignUserForm() {
                this.$refs.assignUserForm.validate();
                // console.log("validateUpdateSoftwareForm", this.updateSoftwareFormValid)
            },
            



            //    === other mothods
            getDate(dateString) {
                return services.formatDateTime(dateString);
            },

            

            changePage(destination) {
                window.location.href = destination;
            },
        },

    };
</script>

