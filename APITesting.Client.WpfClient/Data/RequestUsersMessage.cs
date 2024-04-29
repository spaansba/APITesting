using APITesting.Client.WpfClient.Common;
using APITesting.Contracts;

namespace APITesting.Client.WpfClient.Data;

// Custom AsyncRequestMessage for User Messages that return ApiResult<T> and have no required parameters
public sealed record RequestUsersMessage : ApiResultMessage<RequestUsersMessage, IEnumerable<UserProfileResponse>>;

