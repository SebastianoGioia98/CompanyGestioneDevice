export default class ApiCallerDevices {

	url = `https://localhost:44357/api/app/device/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}


	getDevices() {
		const urlToCall = this.url + `device-list`;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}
	

	deleteDevice(id) {
		const urlToCall = this.url + id;
		return this.restFulApiCallerHelper.callDelete(urlToCall);
	}

	getDeviceDetails(id) {
		const urlToCall = this.url + `device-by-id/` + id;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}


	getDeviceGeolocalization(id) {
		const urlToCall = this.url + `geolocalization/` + id;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}


	updateSoftareDevice(data) {
		const urlToCall = this.url + `update-device-software`;
		return this.restFulApiCallerHelper.callPost(urlToCall, data);
	}


	createDevice(data) {
		const urlToCall = this.url + `device-with-details`;
		return this.restFulApiCallerHelper.callPost(urlToCall, data);
	}

	editDevice(data) {
		const urlToCall = this.url + `device-with-details`;
		return this.restFulApiCallerHelper.callPut(urlToCall, data);
	}
}