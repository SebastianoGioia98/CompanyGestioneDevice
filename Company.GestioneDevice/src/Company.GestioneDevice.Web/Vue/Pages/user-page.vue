<template>

    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-8">
            <h1 class="pageTitle">Users</h1>
        </v-row>




        <!--    === Tools Section ===   -->
        <v-row no-gutters class="mb-4 align-center">


            <v-text-field label="Search" prepend-inner-icon="mdi-magnify"
                          variant="outlined" max-width="500"
                          density="compact"
                          hide-details single-line rounded="lg"
                          class="outlineTextField" base-color="surface">
            </v-text-field>

            <v-btn size="small" class="ml-6" @click="expandFilter = !expandFilter" icon="mdi-filter-multiple"></v-btn>


            <v-btn @click="onBtnAddClick()" color="primary" class="ml-auto">
                Add User
            </v-btn>


            <!--<v-expand-transition>
            <div v-show="expandFilter" class="w-100">
                <div class="d-flex w-100 align-center justify-start ga-4 mt-4">

                    <span>Filter by: </span>

                    <v-autocomplete :items="possibleUsers" v-model="selectedUsers" label="Created By:"
                                    item-title="name" item-value="id" max-width="200" base-color="surface"
                                    multiple clearable @update:modelValue="applyFilter"
                                    density="compact" hide-details rounded="lg" variant="outlined">
                    </v-autocomplete>

                    <v-autocomplete :items="possibleUseCases" v-model="selectedUseCases" label="Use Case:"
                                    item-title="name" item-value="id" max-width="200" base-color="surface"
                                    multiple clearable @update:modelValue="applyFilter"
                                    density="compact" hide-details rounded="lg" variant="outlined">
                    </v-autocomplete>

                    <v-btn size="small" @click="removeFilter" :v-if="selectedUseCases.length !== 0 || selectedUsers.length !== 0"
                           prepend-icon="mdi-close-circle-outline" variant="plain" style="margin-right: 12rem;">
                        Clear
                    </v-btn>
                </div>
            </div>
        </v-expand-transition>-->
        </v-row>



        <!--    === Table Section ===   -->
        <v-row no-gutters class="d-flex flex-column h-100">
            <v-card class="elevation-0 w-100  flex-grow-1 d-flex flex-column" rounded="lg" color="transparent">
                <v-data-table-server v-model:items-per-page="itemsPerPage"
                                     :headers="headers"
                                     :items-length="totalItems"
                                     :items="userList"
                                     style="height: 100% !important"
                                     :loading="loading"
                                     loading-text="Loading, please wait..."
                                     class="elevation-0 dataTable"
                                     height="100%" width="100%"
                                     @click:row="openContent"
                                     @update:options="refeshUserList" color="surface1"
                                     :items-per-page-options="paginatorOptions">



                    <template v-slot:item.creationTime="{ item }">
                        {{ getDate(item.creationTime) }}
                    </template>

                    <template v-slot:item.actions="{ item }">
                        <v-tooltip text="Delete User" location="bottom">
                            <template v-slot:activator="{ props }">
                                <v-btn icon="mdi-delete"
                                       size="x-small"
                                       v-bind="props"
                                       class=" ml-3"
                                       @click.stop="onBtnDeleteClick(item)">
                                </v-btn>
                            </template>
                        </v-tooltip>
                    </template>
                </v-data-table-server>
            </v-card>
        </v-row>




        <!--    === Dialogs ===   -->
        <dialog-delete v-model="showDeleteDialog" :item="selectedUser" @close="onDialogDeleteClose"></dialog-delete>
        <dialog-add-user v-model="showAddDialog" :policyList="policyList" @close="onDialogAddClose"></dialog-add-user>


        <!--    === Snackbar ===   -->
        <snackbar :option="snackbarOpt"></snackbar>

    </v-container>
</template>

<style scoped>
</style>


<script>
    import { services } from "../../Scripts/Services/serviceBuilder";
    import { useTheme } from 'vuetify';

    export default {
        name: "user-page",
        data: function () {
            const theme = useTheme();
            return {
                userList: [],
                policyList: [],


                //state property
                loadingState: false,
                selectedUser: {},

                //dialog property
                showDeleteDialog: false,
                showAddDialog: false,

                //   === table properties
                loading: true,
                totalItems: 0,
                itemsPerPage: 10,
                headers: [
                    {
                        title: "Username",
                        align: 'center',
                        sortable: true,
                        key: 'username',
                        headerProps: {
                            class: 'isSortable' // Add custom CSS class
                        },
                    },
                    {
                        title: "Name",
                        align: 'center',
                        sortable: true,
                        key: 'name',
                        headerProps: {
                            class: 'isSortable' // Add custom CSS class
                        },
                    },
                    {
                        title: "Surname",
                        align: 'center',
                        sortable: true,
                        key: 'surname',
                        headerProps: {
                            class: 'isSortable' // Add custom CSS class
                        },
                    },
                    {
                        title: "Email",
                        align: 'center',
                        sortable: true,
                        key: 'email',
                        headerProps: {
                            class: 'isSortable' // Add custom CSS class
                        },
                    },
                    //{
                    //    title: "Create Time",
                    //    align: 'center',
                    //    sortable: false,
                    //    key: 'creationTime'
                    //},
                    {
                        title: "Actions",
                        align: 'center',
                        sortable: false,
                        key: 'actions'
                    }
                ],
                paginatorOptions: [
                    { value: 10, title: '10' },
                    { value: 20, title: '20' },
                    { value: 50, title: '50' },
                    { value: 100, title: '100' }
                ],



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

        },
        computed: {

        },
        created() {
            let that = this;

            //chiedo la lista dei device e riempio la userList
            // that.refeshuserList();

            //get policyList
            services.ApiCallerPolicies
                .getPolicyList().then(res => {
                    //load featureList
                    that.policyList = res.data.items;

                    console.log("userList : ", that.policyList);

                });
        },
        methods: {


            //    === table methods
            refeshUserList() {
                console.log("ON getUsers");


                let that = this;

                //SET Loading State
                that.loading = true;

                //call to  getDevices
                services.ApiCallerUsers
                    .getUsers().then(res => {
                        //load userList
                        that.userList = res.data.items;
                        that.totalItems = res.data.totalCount;
                        console.log("userList: ", that.userList);
                        console.log("totalItems: ", that.totalItems);
                    }).finally(_ => {
                        //UNSET Loading State
                        that.loading = false;
                    });




            },


            openContent(value, row) {
                window.location.href = "users/" + row.item.id;
            },



            //btn methods
            onBtnDeleteClick(item) {
                let that = this;


                that.selectedUser = item;

                //apro il dialog
                that.showDeleteDialog = true;
                console.log("on onDeleteBtnClick, item: ", that.showDeleteDialog);

            },

            onBtnAddClick() {
                let that = this;

                //apro il dialog
                that.showAddDialog = true;
                console.log("on onBtnAddClick");

            },



            //dialog methods
            onDialogDeleteClose(e) {
                let that = this;
                console.log("onDialogDeleteClose, res: ", e);

                that.showDeleteDialog = false;


                if (e.state === false) return;





                //SET Loading State
                that.loadingState = true;

                //call to  getDevices
                services.ApiCallerUsers
                    .deleteUser(e.id)
                    .then((res) => {
                        console.log("on ApiCallerDevices.deleteDevice, res", res);

                        //preparo il messaggio per lo snackbar
                    })
                    .catch((err) => {
                        console.log("on ApiCallerDevices.deleteDevice, err", err);

                        //preparo il messaggio per lo snackbar
                    })
                    .finally(() => {
                        //UNSET Loading State
                        that.loading = false;


                        //launch snackbar
                        that.snackbarOpt = {
                            snackbar: true,
                            text: 'User deleted successfully!',
                            timeout: 2500,
                            color: 'green'
                        };

                        //chiedo la lista dei device e riempio la userList
                        that.refeshUserList();

                        
                    });
            },

            onDialogAddClose(e) {
                let that = this;
                console.log("onDialogAddClose, res: ", e);

                that.showAddDialog = false;


                if (e.state === false) return;

                let snackOpt = {};

                //SET Loading State
                that.loadingState = true;

                //call to  addDevice
                services.ApiCallerUsers
                    .createUser(e.data)
                    .then((res) => {
                        console.log("on ApiCallerDevices.createUser, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'User added successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    })
                    .catch((err) => {
                        console.log("on ApiCallerDevices.createUser, err", err);

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

            //    === other mothods
            getDate(dateString) {
                return services.formatDateTime(dateString);
            },
        },

    };
</script>

