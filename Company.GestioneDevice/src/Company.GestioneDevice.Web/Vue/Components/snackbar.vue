<template>
    <v-snackbar v-model="option.snackbar"
                :timeout="option.timeout"
                :color="option.color">
        {{ option.text }}
    </v-snackbar>
</template>

<script>

    export default {
        name: "snackbar",
        data: function () {
            return {

            };
        },
        props: {
            option: {
                type: Object,
                default: false,
            },

            //snackbar: false,
            //text: 'My timeout is set to 2000.',
            //timeout: 2000,
        },
        
        computed: {
        },
        created() {
            let that = this;
        },
        methods: {
            
        },
    };
</script>

<style scoped>
    /*.deleteCard {
        margin: 0 !important;
        padding: 0 !important;
    }*/
</style>