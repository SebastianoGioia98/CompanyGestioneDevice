
import axios from "axios";
export default class ApiCallerHelper {

    //getBearerToken() {
    //	const tokenUrl = `${commonModule.webBaseUrl}/AuthHelper/GetAccessToken`;
    //	const token = window.axios.get(tokenUrl);
    //	return token;
    //}


    callGet(urlToCall) {
        return new Promise((resolve, reject) => {
            
                axios.get(urlToCall)
                .then(result => {
                    return resolve(result);
                })
                .catch(err => {
                    return reject(err);
                });
        });
    }


    callDelete(urlToCall) {
        return new Promise((resolve, reject) => {
            console.log('Chiamata DELETE a:', urlToCall);

            axios.delete(urlToCall)
                .then(result => {
                    console.log('Risultato:', result);
                    return resolve(result);
                })
                .catch(err => {
                    console.error('Risultato:', err.response || err);
                    return reject(err);
                });
        });
    }

}
