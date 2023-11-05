import axios from "axios";
const rax = require('retry-axios');

const instance = axios.create({
    baseURL: process.env.REACT_APP_API_ENDPOINT,
    //timeout: 100000,
    headers: {'X-Custom-Header': 'foobar'},
});
instance.interceptors.response.use((response) => {
    return response.data;
}, (error) => {
    // whatever you want to do with the error
    if (error.response.data.error === 'invalid_token') {
        console.log(error.response.data.error);
    }
    return ''

});


//@ts-ignore
instance.defaults.raxConfig = {
    instance: instance
};
const interceptorId = rax.attach(instance);

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
