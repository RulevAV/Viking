export default interface UserRefreshToken{
    id?: string,
    userId?: string,
    refreshToken: string,
    refreshTokenExpiryTime?: Date,
    refreshTokenCreatedTime?: Date
}
