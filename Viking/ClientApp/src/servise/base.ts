import axios from "axios";
const axiosRetry = require('axios-retry');

const instance = axios.create({
    baseURL: process.env.REACT_APP_API_ENDPOINT,
    //timeout: 100000,
    headers: {'X-Custom-Header': 'foobar'},
});
instance.interceptors.request.use(
    config => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    error => Promise.reject(error)
);
axiosRetry(instance, {
    retries: 1, // Number of retries
    retryCondition: async (error : any) => {
        switch (error.response.data.error) {
            case 'invalid_token':
                return await base.updateBearerToken();
            default:
                return false; // Do not retry the others
        }
    },
});
class Base {
    public async updateBearerToken(){
        return false;
    }
    public async get<T>(url: string) {
        return await instance.get<T>(url).then(res=>{
            return res.data
        }).catch(err =>{
            return ""
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
