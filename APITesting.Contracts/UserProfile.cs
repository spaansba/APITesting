namespace APITesting.Contracts
{
    public sealed record UserProfileResponse(
        long Id,
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
}