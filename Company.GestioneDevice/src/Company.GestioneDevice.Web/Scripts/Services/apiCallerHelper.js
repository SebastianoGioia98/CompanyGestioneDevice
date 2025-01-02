export default class ApiCallerHelper {

    //getBearerToken() {
    //	const tokenUrl = `${commonModule.webBaseUrl}/AuthHelper/GetAccessToken`;
    //	const token = window.axios.get(tokenUrl);
    //	return token;
    //}

    callGet(urlToCall) {
        return new Promise((resolve, reject) => {
            window
                .axios({
                    method: "get",
                    headers: {},
                    url: urlToCall,
                })
                .then(result => {
                    return resolve(result);
                })
                .catch(err => {
                    return reject(err);
                });
        });
    }




}
