export default class ApiCallerUsers {

	url = `https://localhost:44357/api/app/utente/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}

	getUsers() {
		const urlToCall = this.url;
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

}