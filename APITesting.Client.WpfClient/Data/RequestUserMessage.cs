using APITesting.Client.WpfClient.Common;
using APITesting.Contracts;

namespace APITesting.Client.WpfClient.Data;

public sealed record RequestUserMessage(long Id)
    : ApiResultMessage<RequestUserMessage, long, UserProfileResponse>,
        ISelfApiResultMessage<RequestUserMessage, long, UserProfileResponse>
{
    protected override long Parameter => this.Id;
    public static RequestUserMessage Create(long id) => new (id);
}