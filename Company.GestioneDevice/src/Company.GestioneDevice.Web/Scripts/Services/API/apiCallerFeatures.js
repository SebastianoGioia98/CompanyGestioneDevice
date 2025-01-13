export default class ApiCallerFeatures {

	url = `https://localhost:44357/api/app/feature/`;


	constructor(helper) {
		this.restFulApiCallerHelper = helper;
	}


	getFeatureList() {
		const urlToCall = this.url;
		return this.restFulApiCallerHelper.callGet(urlToCall);
	}

}