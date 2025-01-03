<template>

    <v-container fluid class="mainContainer d-flex flex-column">



        <!--Title section-->
        <v-row no-gutters class="align-center mb-8">
            <h1 class="pageTitle">Devices</h1>
        </v-row>






        <!--Filter section-->
        <v-row no-gutters class="mb-4 align-center">


            <!--<v-text-field label="Search" prepend-inner-icon="mdi-magnify"
                      variant="outlined" max-width="500"
                      density="compact"
                      @input="filteringByName"
                      v-model="filterObject.name"
                      hide-details single-line rounded="lg"
                      class="outlineTextField" base-color="surface">
        </v-text-field>

        <v-btn size="small" class="ml-6" @click="expandFilter = !expandFilter" icon="mdi-filter-multiple"></v-btn>


        <v-btn @click="addDeviceDialog = true" color="primary" class="ml-auto">
            Add Device
        </v-btn>-->
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











        <!--Table section-->
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





                    <!-- Slot per personalizzare gli header -->
                    <!--<template v-slot:headers="{ item }">
                        <tr>
                            <th :class="isOredered">
                                {{ header.text }}
                            </th>
                        </tr>
                    </template>-->





       


                    <template v-slot:item.type="{ item }">
                        {{item.type}}
                    </template>


                    <!--<template v-slot:item.model="{ item }">
                        {{item.model}}
                    </template>-->






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
                                       @click.stop="deleteItem(item)">
                                </v-btn>
                            </template>
                        </v-tooltip>
                    </template>
                </v-data-table-server>
            </v-card>
        </v-row>



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
                loading: false,

                //   === table properties
                totalItems : 0,
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
                    { value: 10, title: '10' },
                    { value: 20, title: '20' },
                    { value: 50, title: '50' },
                    { value: 100, title: '100' }
                ],
            };
        },
        props: {

        },
        computed: {

        },
        created() {
            let that = this;

            //chiedo la lista dei device e riempio la deviceList
            that.refeshDeviceList();
        },
        methods: {


            //    === table methods
            refeshDeviceList() {
                console.log("ON getDevices");


                let that = this;
                that.loading = true;
                services.ApiCallerDevices
                    .getDevices().then(res => {
                        
                        that.deviceList = res.data;
                        that.totalItems = that.deviceList.length;
                        console.log("deviceList: ", that.deviceList);
                        console.log("totalItems: ", that.totalItems);
                    }).finally(_ => {
                        that.loading = false;
                    });


                

            },

            openContent() {

            },

            deleteItem(item) {
                let that = this;
                console.log("on DeleteItem, item: ", item);

                //that.itemToDelete = item;
                //that.showDelete = true;
                
            },


            //    === other mothods
            getDate(dateString) {
                return services.formatDateTime(dateString);
            },
        },

    };
</script>

