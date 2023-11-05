import {makeAutoObservable} from "mobx";
import AuthorizeServise from "../servise/authorize";
import LoginData from "../models/loginData";

export interface LoginType {
    login: string,
    password: string,
    remember: boolean
}
class Authorize {
    isAuthenticated = false;
    loginData = {} as LoginData;
    constructor() {
        makeAutoObservable(this);
    }
    async checkAuthorize(){


       return AuthorizeServise.checkAuthorize().then(res=>{
            this.isAuthenticated = res;
           console.log(res);
        });
    }
    async login(data: LoginType){
        return AuthorizeServise.LogIn(data).then(res=>{
            if (!res) return;
            this.loginData = res;
            this.isAuthenticated = true;
        });
    }

    async test(){
        return AuthorizeServise.test();
    }
}

const authorize = new Authorize();
export default authorize;
