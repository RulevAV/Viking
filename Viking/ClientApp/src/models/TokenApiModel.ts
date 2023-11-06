import RefreshToken from "./RefreshToken";
import AccessToken from "./AccessToken";

export default interface TokenApiModel{
    accessToken: AccessToken,
    refreshToken: RefreshToken
}
