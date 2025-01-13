export default class ApiCallerPolicies {

	url = `https://localhost:44357/api/app/policy/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}


	getPolicyList() {
		const urlToCall = this.url;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}

}