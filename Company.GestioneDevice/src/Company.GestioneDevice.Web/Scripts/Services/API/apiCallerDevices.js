export default class ApiCallerProjects {

	url = `https://localhost:44357/api/app/device/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}

	getDevices() {
		const urlToCall = this.url + `device-list`;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}



}