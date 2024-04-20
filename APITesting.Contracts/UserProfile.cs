namespace APITesting;

public sealed record UserProfileResponse(
    int Id,
    string Username,
    string FullName,
    string DisplayName
);

public sealed record UserProfileCreateRequest(
    string Username,
    string FullName,
    string DisplayName
);

public sealed record UserProfileUpdateRequest(
    string? FullName,
    string? DisplayName
);