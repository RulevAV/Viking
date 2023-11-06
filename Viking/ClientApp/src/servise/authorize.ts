import Base from "./base";
import LoginData from "../models/loginData";
import TokenApiModel from "../models/TokenApiModel";

class AuthorizeServise {
    controller = "Authorize/"
    constructor() {
        Base.updateBearerToken = async () => {
            return await Base.post<TokenApiModel>(`${this.controller + "UpdateRefreshToken"}`, this.getRefreshToken()).then((res) => {
                const asd = res as TokenApiModel
                localStorage.setItem('token', asd.accessToken.token);
                localStorage.setItem('refreshToken', asd.refreshToken.token);
                return true;
            }).catch(err => {
                return false;
            })
        }
    }
    getRefreshToken(){
        const refreshToken = localStorage.getItem('refreshToken');
        return {refreshToken: {token: refreshToken}} as TokenApiModel;
    }
    isRefreshToken(){
        const refreshToken = localStorage.getItem('refreshToken');
        return !!refreshToken;
    }
    async Register() {
        const data = await Base.get<LoginData>(this.controller + "Register") as LoginData;
        localStorage.setItem('token', data.accessToken.token);
        localStorage.setItem('refreshToken', data.refreshToken.token);
        return data;
    }
    async LogIn(form: any) {
        const data = await Base.post<LoginData>(this.controller + "LogIn", form) as LoginData;
        if (form.remember) {
            localStorage.setItem('token', data.accessToken.token);
            localStorage.setItem('refreshToken', data.refreshToken.token);
        }
        return data;
    }
    async checkAuthorize() {
        const data = await Base.get<boolean>(this.controller + "AuthorizeCheck") as boolean;
        if(data) return true;
        if (this.isRefreshToken()){
            await Base.updateBearerToken();
            return await Base.get<boolean>(this.controller + "AuthorizeCheck") as boolean;
        }
        return false;
    }
    async getUser() {
        return await Base.get<any>(this.controller + "GetUser") as any;
    }
    async logOut() {
        await Base.post<any>(this.controller + "LogOut",  this.getRefreshToken()) as any;
        return localStorage.clear();
    }
}

const authorizeServise = new AuthorizeServise();
export default authorizeServise;
