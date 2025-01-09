import { createApp } from "vue";

import { createVuetify } from "vuetify";
import * as components from "vuetify/components";
import * as directives from "vuetify/directives";
import { mdi } from "vuetify/iconsets/mdi";
import "@mdi/font/css/materialdesignicons.css";
import "vuetify/styles";


//Utility imports
import { pinia } from "../Services/storeManager.js";
import { VTreeview } from 'vuetify/labs/VTreeview';


//Style imports
import darkTheme from "../../Styles/darkTheme.js";

//component imports
import leftMenu from "../../Vue/Components/left-menu.vue";
import deviceContentPage from "../../Vue/Pages/device-content-page.vue";


const app = createApp();


const vuetify = createVuetify({
	components: {
		...components,
		VTreeview
	},
	directives,
	icons: {
		defaultSet: "mdi",
		sets: {
			mdi,
		},
	},
	theme: {
		defaultTheme: 'darkTheme',
		themes: {
			darkTheme
		},
		options: {
			customProperties: true,
		},
	},

});


app.use(vuetify);
app.use(pinia);
app.component("left-menu", leftMenu);
app.component("device-content-page", deviceContentPage);
app.mount("#myApp");