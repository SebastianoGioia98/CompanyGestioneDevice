<template>

    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-5 flex-grow-0">
            <v-btn icon="mdi-arrow-left" @click="changePage('users/')"
                   variant="text" size="small"
                   class="mr-4">
            </v-btn>

            <h1 class="pageTitle mr-10 ml-auto">User</h1>

            <v-select :items="userList" v-model="selectedUser" @update:modelValue="changeUser"
                      item-title="username" item-value="id"
                      hide-details variant="solo-filled" density="compact"
                      max-width="200" class="mr-auto">
            </v-select>

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
               
               



                //state property
                loadingState: false,
                selectedUser: {},


                //dialog property
                showEditDialog: false,
              
              

               

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
        },
        mounted() {
            let that = this;

        },
        methods: {


            //   === Select methods
            refeshUserList() {
                console.log("ON getDevices");


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
                        console.log("OnMounted, userId: ", that.userId);
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

