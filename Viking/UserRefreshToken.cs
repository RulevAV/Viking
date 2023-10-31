using System;
using System.Collections.Generic;

namespace Viking;

public partial class UserRefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public string RefreshTokenExpiryTime { get; set; } = null!;

    public string RefreshTokenCreatedTime { get; set; } = null!;
}
