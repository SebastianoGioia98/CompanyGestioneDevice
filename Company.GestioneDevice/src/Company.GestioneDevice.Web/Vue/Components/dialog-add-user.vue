<template>
    <v-dialog min-width="350" max-width="800"
              :modelValue="modelValue"
              persistent>
        <v-card prepend-icon="mdi-plus-box" title="Add New User"
                text="Fill the fields and press add">

            <v-card-text>
                <v-form v-model="addUserFormValid" ref="addUserForm">
                    <v-container>
                        <v-row>
                            <v-col cols="6">
                                <!--Username-->
                                <v-text-field label="Username"
                                              v-model="newUser.username"
                                              :counter="30"
                                              :rules="[v => !!v || 'Username is required']"
                                              @input="validateAddUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols="6">
                                <!--Name-->
                                <v-text-field label="Name"
                                              v-model="newUser.name"
                                              :counter="30"
                                              :rules="[v => !!v || 'Name is required']"
                                              @input="validateAddUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--Surname-->
                                <v-text-field label="Surname"
                                              v-model="newUser.surname"
                                              :counter="30"
                                              :rules="[v => !!v || 'Surname is required']"
                                              @input="validateAddUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col cols="6">
                                <!--Email-->
                                <v-text-field v-model="newUser.email"
                                              :counter="30"
                                              label="Email"
                                              :rules="[v => !!v || 'Email is required']"
                                              @input="validateAddUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>

                            <v-col cols="6">
                                <!--Telephone-->
                                <v-text-field v-model="newUser.telephone"
                                              :counter="30"
                                              label="Telephone"
                                              :rules="[v => !!v || 'Telephone is required']"
                                              @input="validateAddUserForm"
                                              required
                                              variant="solo-filled">
                                </v-text-field>
                            </v-col>


                        </v-row>

                        <v-row>
                            <v-col cols="12">
                                <!--Policies-->
                                <v-select label="Features"
                                          v-model="newUser.policyIds"
                                          item-title="name" item-value="id"
                                          :items="policyList"
                                          :rules="[v => Array.isArray(v) && v.length > 0 || 'At least one feature is required']"
                                          multiple
                                          @blur="validateAddUserForm"
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

                <v-btn @click="onClick(true)" :disabled="!addUserFormValid">
                    Add
                </v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script>

    export default {
        name: "dialog-add-user",
        data: function () {
            return {

                newUser: {
                    username: '',
                    name: '',
                    surname: '',
                    email: '',
                    telephone: '',
                    policyIds: []
                },

                addUserFormValid: false,

            };
        },
        props: {
            modelValue: {
                type: Boolean,
                required: true,
            },
            policyList: {
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
            // console.log("On dialog-add-device created, policyList is: ", that.policyList);
        },
        methods: {
            onClick(btnState) {
                let that = this;

                //prepare the res
                let res = {
                    'state': btnState,
                    'data': that.newUser
                };
                console.log("On dialog-add OnClick, res:", res);
                this.$emit("close", res);

                //svuoto il form
                newUser = {
                    username: '',
                    name: '',
                    surname: '',
                    email: '',
                    telephone: '',
                    policyIds: []
                };
            },



            //forms Methods
            validateAddUserForm() {
                this.$refs.addUserForm.validate();
                //console.log("validateAddUserForm", this.addUserFormValid)
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