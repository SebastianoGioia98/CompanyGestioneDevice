export default class ApiCallerDevices {

	url = `https://localhost:44357/api/app/device/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}


	getDevices(types = [], userIds = [], itemsPerPage = 10, pageNumber = 1, sortByKey = null, sortByOrder = null) {

		if (sortByKey == 'user.username') {
			sortByKey = 'userId'
		}

		// Costruzione dei query params
		const params = new URLSearchParams();

		// Aggiungi i parametri solo se sono presenti
		if (types.length > 0) {
			types.forEach(type => params.append('types', type));
		}
		if (userIds.length > 0) {
			userIds.forEach(userId => params.append('userIds', userId));
		}
		params.append('itemsPerPage', itemsPerPage);
		params.append('pageNumber', pageNumber);

		if (sortByKey != null && sortByOrder != null) {
			params.append('sortByKey', sortByKey);
			params.append('sortByOrder', sortByOrder);
		}

		// Costruzione dell'URL completo
		const urlToCall = `${this.url}device-list?${params.toString()}`;

		console.log("on getDevices url:")

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

	assignUser(data) {
		const urlToCall = this.url + `assign-user-device`;
		return this.restFulApiCallerHelper.callPut(urlToCall, data);
	}

	getSoftwareVersionList(id) {
		const urlToCall = this.url + `software-versions/` + id;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}
}