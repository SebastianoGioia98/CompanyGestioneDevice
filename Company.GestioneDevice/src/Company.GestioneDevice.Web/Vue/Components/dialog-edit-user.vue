<template>
    <v-dialog min-width="350" max-width="800"
              :modelValue="modelValue"
              persistent>
        <v-card prepend-icon="mdi-pencil" title="Edit User"
                text="Fill the fields and press Edit">

            <v-card-text>
                <v-form v-model="editUserFormValid" ref="editUserForm">
                    <v-container>
                        <v-row>
                            <v-col cols="6">
                                <!--userame-->
                                <v-text-field label="Username"
                                              v-model="user.username"
                                              :counter="30"
                                              :rules="[v => !!v || 'Username is required']"
                                              @input="validateEditUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>
                        </v-row>


                        <v-row>
                            <v-col cols="6">
                                <!--Name-->
                                <v-text-field label="Name"
                                              v-model="user.name"
                                              :counter="30"
                                              :rules="[v => !!v || 'Name is required']"
                                              @input="validateEditUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--Surname-->
                                <v-text-field label="Name"
                                              v-model="user.surname"
                                              :counter="30"
                                              :rules="[v => !!v || 'Surname is required']"
                                              @input="validateEditUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>


                        </v-row>


                        <v-row>
                            <v-col cols="6">
                                <!--Email-->
                                <v-text-field v-model="user.email"
                                              :counter="30"
                                              label="Email"
                                              :rules="[v => !!v || 'Email is required']"
                                              @input="validateEditUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--Telephone-->
                                <v-text-field v-model="user.telephone"
                                              :counter="30"
                                              label="Telephone"
                                              :rules="[v => !!v || 'Telephone is required']"
                                              @input="validateEditUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
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

                <v-btn @click="onClick(true)" :disabled="!editUserFormValid">
                    Edit
                </v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script>

    export default {
        name: "dialog-edit-user",
        data: function () {
            return {



                editUserFormValid: false,
           
            };
        },
        props: {
            modelValue: {
                type: Boolean,
                required: true,
            },
            user: {
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
                    'data': that.user
                };
                console.log("On dialog-edit OnClick, res:", res);
                this.$emit("close", res);


            },



            //forms Methods
            validateEditUserForm() {
                this.$refs.editUserForm.validate();
                //console.log("validateUpdateSoftwareForm", this.editUserFormValid)
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