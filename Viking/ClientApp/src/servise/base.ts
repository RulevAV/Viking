import axios from "axios";
const axiosRetry = require('axios-retry');



const instance = axios.create({
    baseURL: process.env.REACT_APP_API_ENDPOINT,
    //timeout: 100000,
    headers: {'X-Custom-Header': 'foobar'},
});

axiosRetry(instance, {
    retries: 1, // Number of retries
    retryCondition(error) {
        console.log("12312321312312312312")
        // Conditional check the error status code
        switch (error.response.status) {
            case 405:
                return true;
            default:
                return false; // Do not retry the others
        }
    },
});

//@ts-ignore
instance.defaults.raxConfig = {
    instance: instance
};


class Base {
    public setBearerToken(token: string){
        instance.defaults.headers.common["Authorization"] = `bearer ${token}`;
    }

    requare = null;

    fnRequare(){
    }

    public async get<T>(url: string) {
        var fnGet = instance.get<T>;

        return await fnGet(url).then(res=>{
            return res.data
        }).catch(err =>{
            axios.post("")
        });
    }
    public async post<T>(url: string, data: any){
        return await instance.post<T>(url, data).then(res=>{
            return res.data
        }).catch(err =>{
            return ""
        });
    }
    public async delete(url: string){
        return await instance.delete(url).then(res=>{
            return res.data
        }).catch(err =>{
            return ""
        });
    }
    public async put(url: string, data: any){
        return await instance.put(url, data).then(res=>{
            return res.data
        }).catch(err =>{
            return ""
        });
    }
}
const base = new Base();
export default base;
