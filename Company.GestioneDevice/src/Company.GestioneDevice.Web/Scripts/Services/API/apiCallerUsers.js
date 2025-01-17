export default class ApiCallerUsers {

	url = `https://localhost:44357/api/app/utente/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}

	getUsers(userName = null, itemsPerPage = 10, pageNumber = 1, sortByKey = null, sortByOrder = null) {

		// Costruzione dei query params
		const params = new URLSearchParams();

		// Aggiungi i parametri solo se sono presenti
		params.append('itemsPerPage', itemsPerPage);
		params.append('pageNumber', pageNumber);

		if (sortByKey != null && sortByOrder != null) {
			params.append('sortByKey', sortByKey);
			params.append('sortByOrder', sortByOrder);
		}

		if (userName != null) {
			params.append('userName', userName);
		}

		// Costruzione dell'URL completo
		const urlToCall = `${this.url}user-list?${params.toString()}`;

		//console.log("on getDevices url:")

		return this.restFulApiCallerHelper.callGet(urlToCall);
	}

	deleteUser(id) {
		const urlToCall = this.url + id;
		return this.restFulApiCallerHelper.callDelete(urlToCall);
	}


	getUserDetails(id) {
		const urlToCall = this.url + `user-by-id/` + id;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}


	createUser(data) {
		const urlToCall = this.url + `user-with-details`;
		return this.restFulApiCallerHelper.callPost(urlToCall, data);
	}


	assignUserPolicies(data) {
		const urlToCall = this.url + `assign-user-policies`;
		return this.restFulApiCallerHelper.callPut(urlToCall, data);
	}


	editUser(data) {
		const urlToCall = this.url + `user-with-details`;
		return this.restFulApiCallerHelper.callPut(urlToCall, data);
	}
}