export default class ApiCallerProjects {

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
}