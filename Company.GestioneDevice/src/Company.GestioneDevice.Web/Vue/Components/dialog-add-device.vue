<template>
    <v-dialog min-width="350" max-width="800"
              :modelValue="modelValue"
              persistent>
        <v-card prepend-icon="mdi-plus-box" title="Add New Device"
                text="Fill the fields and press add">

            <v-card-text>
                <v-form v-model="addDeviceFormValid" ref="addDeviceForm">
                    <v-container>
                        <v-row>
                            <v-col cols="6">
                                <!--Name-->
                                <v-text-field label="Name"
                                              v-model="newDevice.name"
                                              :counter="30"
                                              :rules="[v => !!v || 'Name is required']"
                                              @input="validateAddDeviceForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--Type-->
                                <v-select label="Type"
                                          v-model="newDevice.type"
                                          item-title="name" item-value="id"
                                          :items="typeList"
                                          :rules="[v => v !== null && v !== undefined || 'Type is required']"
                                          @blur="validateAddDeviceForm"
                                          required
                                          variant="solo-filled">
                                </v-select>
                            </v-col>


                        </v-row>
                        <v-row>
                            <v-col cols="6">
                                <!--model-->
                                <v-text-field v-model="newDevice.model"
                                              :counter="30"
                                              label="Model"
                                              :rules="[v => !!v || 'Model is required']"
                                              @input="validateAddDeviceForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--User-->
                                <v-select label="User"
                                          v-model="newDevice.userId"
                                          item-title="username" item-value="id"
                                          :items="userList"
                                          :rules="[v => !!v || 'User is required']"
                                          @blur="validateAddDeviceForm"
                                          variant="solo-filled">
                                </v-select>
                            </v-col>


                        </v-row>
                        <v-row>
                            <v-col cols="6">
                                <!--firstSoftwareVersion Name-->
                                <v-text-field v-model="newDevice.firstSoftwareVersion.name"
                                              :counter="30"
                                              label="Software Name"
                                              :rules="[v => !!v || 'Name is required']"
                                              @input="validateAddDeviceForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--firstSoftwareVersion Version-->
                                <v-text-field v-model="newDevice.firstSoftwareVersion.version"
                                              :counter="3"
                                              label="Sofware Version"
                                              :rules="[v => !!v || 'Version is required']"
                                              @input="validateAddDeviceForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols="12">
                                <!--Features-->
                                <v-select label="Features"
                                          v-model="newDevice.deviceFeaturesIds"
                                          item-title="name" item-value="id"
                                          :items="featureList"
                                          :rules="[v => Array.isArray(v) && v.length > 0 || 'At least one feature is required']"
                                          multiple
                                          @blur="validateAddDeviceForm"
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

                <v-btn @click="onClick(false)">
                    Cancel
                </v-btn>

                <v-btn @click="onClick(true)" :disabled="!addDeviceFormValid">
                    Add
                </v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script>

    export default {
        name: "dialog-add-device",
        data: function () {
            return {

                newDevice: {
                    name: '',
                    type: null,
                    model: '',
                    userId: null,
                    firstSoftwareVersion: {
                        name: '',
                        version: ''
                    },
                    deviceFeaturesIds: []

                },

                addDeviceFormValid: false,
                typeList: [
                    { name: 'Laptop', id: 0 },
                    { name: 'Smartphone', id: 1 },
                    { name: 'Smartwatch', id: 2 },
                    { name: 'Tablet', id: 3 },
                ],
            };
        },
        props: {
            modelValue: {
                type: Boolean,
                required: true,
            },
            featureList: {
                type: Object,
                required: true,
            },
            userList: {
                type: Object,
                required: true,
            },
        },
        emits: ['close'],
        computed: {
        },
        created() {
            let that = this;

            //log di item
            // console.log("On dialog-add-device created, featureList is: ", that.featureList);
        },
        methods: {
            onClick(btnState) {
                let that = this;

                //prepare the res
                let res = {
                    'state': btnState,
                    'data': that.newDevice
                };
                console.log("On dialog-add OnClick, res:", res);
                this.$emit("close", res);

                //svuoto il form
                that.newDevice = {
                    name: '',
                    type: null,
                    model: '',
                    userId: null,
                    firstSoftwareVersion: {
                        name: '',
                        version: ''
                    },
                    deviceFeaturesIds: []
                };
            },



            //forms Methods
            validateAddDeviceForm() {
                this.$refs.addDeviceForm.validate();
                //console.log("validateUpdateSoftwareForm", this.addDeviceFormValid)
            },
        },
    };
</script>

<style scoped>
    /*.deleteCard {
        margin: 0 !important;
        padding: 0 !important;
    }*/
</style>