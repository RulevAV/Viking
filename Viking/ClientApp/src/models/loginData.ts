import RefreshToken from "./RefreshToken";
import AccessToken from "./AccessToken";

export default interface LoginData {
  accessToken: AccessToken,
  flag: boolean,
  refreshToken: RefreshToken
}
