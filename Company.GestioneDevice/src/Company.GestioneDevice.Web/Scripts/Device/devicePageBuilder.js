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
import devicePage from "../../Vue/Pages/device-page.vue";
import dialogDelete from "../../Vue/Components/dialog-delete.vue";
import dialogAdd from "../../Vue/Components/dialog-add.vue";

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
app.component("device-page", devicePage);
app.component("dialog-delete", dialogDelete);
app.component("dialog-add", dialogAdd);
app.mount("#myApp");