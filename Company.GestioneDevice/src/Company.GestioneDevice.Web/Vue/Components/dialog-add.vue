<template>
    <v-dialog min-width="350" max-width="400"
              :modelValue="modelValue"
              persistent>
        <v-card prepend-icon="mdi-delete-alter" title="Add New Device"
                text="Fill the fields and press add">
            <template v-slot:actions>
                <v-spacer></v-spacer>

                <v-btn @click="onClick(false)">
                    Cancel
                </v-btn>

                <v-btn @click="onClick(true)">
                    Add
                </v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script>

    export default {
        name: "dialog-add",
        data: function () {
            return {

            };
        },
        props: {
            modelValue: {
                type: Boolean,
                required: true,
            },
        },
        emits: ['close'],
        computed: {
        },
        created() {
            let that = this;

            //log di item
            console.log("On dialog-add created, item is: ", that.item);
        },
        methods: {
            onClick(btnState) {
                let that = this;

                //prepare the res
                let res = {
                    'state': btnState
                    
                };
                console.log("On dialog-add OnClick, res:", res);
                this.$emit("close", res);
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