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
import userContentPage from "../../Vue/Pages/user-content-page.vue";
import snackbar from "../../Vue/Components/snackbar.vue";
import dialogEditUser from "../../Vue/Components/dialog-edit-user.vue";

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
app.component("user-content-page", userContentPage);
app.component("snackbar", snackbar);
app.component("dialog-edit-user", dialogEditUser);
app.mount("#myApp");