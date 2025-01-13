<template>
    <v-dialog min-width="350" max-width="800"
              :modelValue="modelValue"
              persistent>
        <v-card prepend-icon="mdi-pencil" title="Edit Device"
                text="Fill the fields and press Edit">

            <v-card-text>
                <v-form v-model="editDeviceFormValid" ref="editDeviceForm">
                    <v-container>
                        <v-row>
                            <v-col cols="6">
                                <!--Name-->
                                <v-text-field label="Name"
                                              v-model="device.name"
                                              :counter="30"
                                              :rules="[v => !!v || 'Name is required']"
                                              @input="validateEditDeviceForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--Type-->
                                <v-select label="Type"
                                          v-model="device.type"
                                          item-title="name" item-value="id"
                                          :items="typeList"
                                          :rules="[v => v !== null && v !== undefined || 'Type is required']"
                                          @blur="validateEditDeviceForm"
                                          required
                                          variant="solo-filled">
                                </v-select>
                            </v-col>


                        </v-row>
                        <v-row>
                            <v-col cols="6">
                                <!--model-->
                                <v-text-field v-model="device.model"
                                              :counter="30"
                                              label="Model"
                                              :rules="[v => !!v || 'Model is required']"
                                              @input="validateEditDeviceForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                        </v-row>
                       
                        <v-row>
                            <v-col cols="12">
                                <!--Features-->
                                <v-select label="Features"
                                          v-model="device.deviceFeaturesIds"
                                          item-title="name" item-value="id"
                                          :items="featureList"
                                          :rules="[v => Array.isArray(v) && v.length > 0 || 'At least one feature is required']"
                                          multiple
                                          @blur="validateEditDeviceForm"
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

                <v-btn @click="onClick(true)" :disabled="!editDeviceFormValid">
                    Edit
                </v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script>

    export default {
        name: "dialog-edit-device",
        data: function () {
            return {

               

                editDeviceFormValid: false,
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
            device: {
                type: Object,
                required: true,
            }
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
                    'data': that.device
                };
                console.log("On dialog-add OnClick, res:", res);
                this.$emit("close", res);

               
            },



            //forms Methods
            validateEditDeviceForm() {
                this.$refs.editDeviceForm.validate();
                //console.log("validateUpdateSoftwareForm", this.editDeviceFormValid)
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