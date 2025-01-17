<template>

    <v-container fluid class="mainContainer d-flex flex-column">

        <!--    === Title Section ===   -->
        <v-row no-gutters class="align-center mb-8">
            <h1 class="pageTitle">Devices</h1>
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
                Add Device
            </v-btn>


            <v-expand-transition>
                <div v-show="expandFilter" class="w-100">
                    <div class="d-flex w-100 align-start justify-start ga-4 mt-4">

                        <span class="align-self-center">Filter by: </span>

                        <v-autocomplete :items="typeList" v-model="types" label="Type:"
                                        item-title="name" item-value="value" max-width="250"
                                        multiple clearable @blur="applyFilter" @click:clear="onFilterClear"
                                        density="compact" hide-details rounded="lg" variant="solo-filled"
                                        ref="typeAutocomplete">
                        </v-autocomplete>

                        <v-autocomplete :items="userList" v-model="userIds" label="Owner:"
                                        item-title="username" item-value="id" max-width="250"
                                        multiple clearable @blur="applyFilter" @click:clear="onFilterClear"
                                        density="compact" hide-details rounded="lg" variant="solo-filled"
                                        ref="userAutocomplete">
                        </v-autocomplete>

                        <v-btn size="small" @click="removeFilter" :v-if="types.length !== 0 || userIds.length !== 0"
                               prepend-icon="mdi-close-circle-outline" variant="plain" style="margin-right: 12rem;"
                               class="align-self-center">
                            Clear
                        </v-btn>
                    </div>
                </div>
            </v-expand-transition>
        </v-row>



        <!--    === Table Section ===   -->
        <v-row no-gutters class="d-flex flex-column h-100">
            <v-card class="elevation-0 w-100  flex-grow-1 d-flex flex-column" rounded="lg" color="transparent">
                <v-data-table-server v-model:items-per-page="itemsPerPage"
                                     :headers="headers"
                                     :items-length="totalItems"
                                     :items="deviceList"
                                     style="height: 100% !important"
                                     :loading="loading"
                                     loading-text="Loading, please wait..."
                                     class="elevation-0 dataTable"
                                     height="100%" width="100%"
                                     @click:row="openContent"
                                     @update:options="refeshDeviceList" color="surface1"
                                     :items-per-page-options="paginatorOptions">


                    <template v-slot:item.type="{ item }">
                        {{getDeviceType(item.type)}}
                    </template>

                    <template v-slot:item.creationTime="{ item }">
                        {{ getDate(item.creationTime) }}
                    </template>

                    <template v-slot:item.actions="{ item }">
                        <v-tooltip text="Delete Device" location="bottom">
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
        <dialog-delete v-model="showDeleteDialog" :item="selectedDevice" @close="onDialogDeleteClose"></dialog-delete>
        <dialog-add-device v-model="showAddDialog" :featureList="featureList" :userList="userList" @close="onDialogAddClose"></dialog-add-device>



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
        name: "device-page",
        data: function () {
            const theme = useTheme();
            return {
                deviceList: [],
                featureList: [],
                userList: [],
                typeList: [
                    {
                        name: 'Laptop',
                        value: 0
                    },
                    {
                        name: 'Smartphone',
                        value: 1
                    },
                    {
                        name: 'Smartwatch',
                        value: 2
                    },
                    {
                        name: 'Tablet',
                        value: 3
                    }
                ],



                //state property
                loadingState: false,
                selectedDevice: {},



                //dialog property
                showDeleteDialog: false,
                showAddDialog: false,



                //   === filter properties
                expandFilter: false,
                types: [],
                userIds: [],
                itemsPerPage: 10,



                //   === table properties
                loading: true,
                totalItems: 0,
                headers: [
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
                        title: "Type",
                        align: 'center',
                        sortable: true,
                        key: 'type',
                        headerProps: {
                            class: 'isSortable' // Add custom CSS class
                        },
                    },
                    {
                        title: "Model",
                        align: 'center',
                        sortable: true,
                        key: 'model',
                        headerProps: {
                            class: 'isSortable' // Add custom CSS class
                        },
                    },
                    {
                        title: "Owner",
                        align: 'center',
                        sortable: false,
                        key: 'user.username'
                    },
                    {
                        title: "Create Time",
                        align: 'center',
                        sortable: false,
                        key: 'creationTime'
                    },
                    {
                        title: "Actions",
                        align: 'center',
                        sortable: false,
                        key: 'actions'
                    }
                ],
                paginatorOptions: [
                    { value: 5, title: '5' },
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
            console.log("on devicePage Created");
            //chiedo la lista dei device e riempio la deviceList
            // that.refeshDeviceList();

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
        methods: {


            //    === table methods
            refeshDeviceList(options) {
                console.log("ON getDevices");
                console.log('Options:', options);

                let that = this;

                //reset itemsPerPage
                if (!options) {
                    that.itemsPerPage = 10;
                }

                //SET Loading State
                that.loading = true;

                //call to  getDevices
                services.ApiCallerDevices
                    .getDevices(
                        that.types,
                        that.userIds,
                        options ? options.itemsPerPage : 10,
                        options ? options.page : 1)
                    .then(res => {
                        //load deviceList
                        that.deviceList = res.data.items;
                        that.totalItems = res.data.totalItems;
                        console.log("deviceList: ", that.deviceList);
                        console.log("totalItems: ", that.totalItems);
                    }).finally(_ => {
                        //UNSET Loading State
                        that.loading = false;
                    });




            },

            openContent(value, row) {
                window.location.href = "devices/" + row.item.id;
            },




            //    === filter methods
            removeFilter() {
                let that = this;

                //reset filters
                that.types = [];
                that.userIds = [];
                that.itemsPerPage = 10;

                that.refeshDeviceList();
            },

            applyFilter() {
                let that = this;

                console.log("On ApplyFilter: ");
                console.log("types: ", that.types);
                console.log("userIds: ", that.userIds);

                that.refeshDeviceList();
            },

            onFilterClear() {
                console.log("on Filter Clear");

                // Attiva manualmente il blur
                this.$nextTick(() => {
                    if (this.$refs.typeAutocomplete) {
                        this.$refs.typeAutocomplete.blur();
                    }
                    if (this.$refs.userAutocomplete) {
                        this.$refs.userAutocomplete.blur();
                    }
                });
            },

            //btn methods
            onBtnDeleteClick(item) {
                let that = this;


                that.selectedDevice = item;

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


                let snackOpt = {};

                //SET Loading State
                that.loadingState = true;

                //call to  getDevices
                services.ApiCallerDevices
                    .deleteDevice(e.id)
                    .then((res) => {
                        console.log("on ApiCallerDevices.deleteDevice, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'Device deleted successfully!',
                            timeout: 2500,
                            color: 'green'
                        }
                    })
                    .catch((err) => {
                        console.log("on ApiCallerDevices.deleteDevice, err", err);

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


                        //lancio la snackbar
                        that.snackbarOpt = snackOpt;

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
                services.ApiCallerDevices
                    .createDevice(e.data)
                    .then((res) => {
                        console.log("on ApiCallerDevices.createDevice, res", res);

                        //preparo il messaggio per lo snackbar
                        snackOpt = {
                            snackbar: true,
                            text: 'Device added successfully!',
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



            //    === other mothods
            getDate(dateString) {
                return services.formatDateTime(dateString);
            },

            getDeviceType(type) {
                return services.getDeviceType(type);
            }
        },

    };
</script>

