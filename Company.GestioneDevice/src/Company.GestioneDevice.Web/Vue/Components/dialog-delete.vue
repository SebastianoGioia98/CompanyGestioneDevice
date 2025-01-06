<template>
    <v-dialog min-width="350" max-width="400"
             :modelValue="modelValue"
         persistent>
        <v-card prepend-icon="mdi-delete-alter" title="Delete"
                text="are you sure you want to delete this items?"
               >
            <template v-slot:actions>
                <v-spacer></v-spacer>

                <v-btn @click="onClick(false)">
                    Cancel
                </v-btn>

                <v-btn @click="onClick(true)">
                    Delete
                </v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script>

    export default {
        name: "dialog-delete",
        data: function () {
            return {
  
            };
        },
        props: {
            modelValue: {
                type: Boolean,
                required: true,
            },
            item: {
                type: Object,
                default: false,
            },
        },
        emits: ['close'],
        computed: {
        },
        created() {
            let that = this;

            //log di item
            console.log("On delete-dialog created, item is: ", that.item);
        },
        methods: {
            onClick(btnState) {
                let that = this;

                //prepare the res
                let res = {
                    'state': btnState,
                    'id': that.item?.id
                };
                console.log("On delete-dialog OnClick, res:", res);
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