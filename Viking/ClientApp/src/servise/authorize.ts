import Base from "./base";
import LoginData from "../models/loginData";

class AuthorizeServise {
    controller = "Authorize/"

    constructor() {
        let token = localStorage.getItem('token');
        Base.setBearerToken(token);
    }
async test(){
    let asd = await Base.get("WeatherForecast");
    return asd
}

   async LogIn(data: any){
        const temp = await Base.post<LoginData>(this.controller + "LogIn", data) as LoginData;
       if(data.remember){
           localStorage.setItem('token', temp.token);
           localStorage.setItem('refreshToken', temp.refreshToken.refreshToken);
           Base.setBearerToken(temp.token);
       }
        return temp;
    }
    async checkAuthorize(){
        return await Base.get<boolean>(this.controller + "AuthorizeCheck") as boolean;
    }
}
const authorizeServise = new AuthorizeServise();
export default authorizeServise;
