using System;
using System.Collections.Generic;

namespace Viking.Models;

public partial class UserRefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public DateTime RefreshTokenCreatedTime { get; set; }
}
