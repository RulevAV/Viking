import {makeAutoObservable} from "mobx";
import AuthorizeServise from "../servise/authorize";
import LoginData from "../models/loginData";

class Counter {
    count = 0;
    data: LoginData;
    constructor() {
        makeAutoObservable(this);
    }
    incriment(){
        this.count = this.count + 1;
    }
    decriment(){
        this.count = this.count - 1;
    }

    async login(){
        var data = {
            login:"Viking1",
            password:"Viking1"
        }
        return AuthorizeServise.LogIn(data).then(res=>{
            this.data = res;
        });
    }
}

export default new Counter()
