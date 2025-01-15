<template>
    <div>
        <v-navigation-drawer app v-model="drawer" :rail="rail" :width="220" :rail-width="100" permanent
                             style="overflow-y: hidden !important;" :class="rail? 'closedMenu': 'expandedMenu'">

            <div class="menuContent">
                <v-list :class="rail? 'pt-0': ''">
                    <v-list-item title="" @click="openCloseMenu" align="center" class="pt-6 pb-2">
                        <img v-if="rail" src="/images/logo/logo_short.png" width="100%" />
                        <img v-else src="/images/logo/logo_large.png" width="80%" />
                    </v-list-item>
                </v-list>

                <v-divider v-if="rail"
                           :thickness="2"
                           class="border-opacity-75"
                           color="surface-light"
                           :length="'80%'">
                </v-divider>

                <v-list density="compact" nav class="pages" :width="'90%'">
                    <v-list-item v-for="(voice, index) in pages" :key="index"
                                 :class="isCurrentPage(voice) ? 'selectedMenuVoice' : ''"
                                 @mousedown.middle="openNewTab(voice.redirect)" slim rounded="lg"
                                 :prepend-icon="voice.icon" :title="rail? '': voice.title" @click="changePage(voice.redirect)">
                    </v-list-item>
                </v-list>

                <v-list density="compact" nav class="mt-auto" :width="'90%'">
                    <v-list-item v-for="(voice, index) in pages_bottom" :key="index"
                                 :class="isCurrentPage(voice) ? 'selectedMenuVoice' : ''"
                                 @mousedown.middle="openNewTab(voice.redirect)" slim rounded="lg"
                                 :prepend-icon="voice.icon" :title="rail? '': voice.title" @click="changePage(voice.redirect)">
                    </v-list-item>
                </v-list>
            </div>
        </v-navigation-drawer>


    </div>
</template>

<script>
    import { services } from "../../Scripts/Services/serviceBuilder";
    import { useTheme } from 'vuetify';

    export default {
        name: "left-menu",
        data: function () {
            const theme = useTheme();
            return {
                drawer: true,
                rail: true,
                pages: [
                    {
                        title: "Home",
                        icon: "mdi-home",
                        redirect: "/"
                    },
                    {
                        title: "Devices",
                        icon: "mdi-devices",
                        redirect: "/devices/"
                    },
                    {
                        title: "Users",
                        icon: "mdi-account",
                        redirect: "/users/"
                    }
                ],
                pages_bottom: [
                    {
                        //title: "Logout",
                        //icon: "mdi-logout",
                        //redirect: "/home/logout/"
                    },
                ],



                currentConnectionHub: null,
            };
        },
        props: {

        },
        computed: {

        },
        methods: {
            openCloseMenu() {
                let that = this;
                that.rail = !that.rail;
       
                let main = document.getElementById("myApp");
                if (!that.rail)
                    main.querySelector(".vMain").classList.add("expandedMenu");
                else
                    main.querySelector(".vMain").classList.remove("expandedMenu");
            },
            isCurrentPage: function (voice) {
                if (window.location.pathname.length == 1)
                    return (window.location.pathname == voice.redirect);
                else
                    return (window.location.pathname.includes(voice.redirect) && voice.redirect.length > 1);
            },
            changePage: function (page) {
                if (!page) return;
                
                
                //Closing existing connection and switching page
                if (this.currentConnectionHub != null) {
                    this.currentConnectionHub.stop().then(() => {
                        window.location.href = page;
                    });
                } else {
                    window.location.href = page;
                }

            },
            openNewTab(page) {
                if (!page) return;

                if (page === "/home/logout/") {
                    window.location.href = page;
                    this.userAuthStore.$reset();
                }
                window.open(page, "_blank");
            },
        },

    };
</script>

<style scoped>

</style>
