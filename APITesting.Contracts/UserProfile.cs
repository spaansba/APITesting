namespace APITesting;

public sealed record UserProfileResponse(
    int Id,
    string Username,
    string DisplayName
);

public sealed record UserProfileUpdateRequest(
    string? DisplayName
);