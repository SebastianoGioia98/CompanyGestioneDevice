import ApiCallerHelper from "./apiCallerHelper.js";



export default class Services {

	constructor() {

		
	}

	get apiCallerHelper() {
		return new ApiCallerHelper();
	}




	getDialogWidthVuetify(breakpoint) {
		switch (breakpoint) {
			case "xs":
				return 350;
			case "sm":
				return 550;
			case "md":
				return 650;
			case "lg":
				return 750;
			case "xl":
				return 1000;
		}
	}

	//Format C# DateTime to formatted date with time (italian format)
	formatDateTime(dateString) {
		const date = new Date(dateString);
		const day = String(date.getDate()).padStart(2, '0');
		const month = String(date.getMonth() + 1).padStart(2, '0');
		const year = date.getFullYear();
		const hours = String(date.getHours()).padStart(2, '0');
		const minutes = String(date.getMinutes()).padStart(2, '0');
		const seconds = String(date.getSeconds()).padStart(2, '0');

		return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
	}

	//Format C# DateTime to formatted date (italian format)
	formatDate(dateString) {
		const date = new Date(dateString);
		const day = String(date.getDate()).padStart(2, '0');
		const month = String(date.getMonth() + 1).padStart(2, '0');
		const year = date.getFullYear();

		return `${day}/${month}/${year}`;
	}


	getHubUrl() {
		return commonModule.signalRHubUrl;
	}

}

export const services = new Services();
