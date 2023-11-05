import UserRefreshToken from "./UserRefreshToken";

export default interface LoginData {
  token: string,
  flag: boolean,
  refreshToken: UserRefreshToken
}
