using APITesting.Client.WpfClient.Common;
using APITesting.Contracts;

namespace APITesting.Client.WpfClient.Data;

public sealed record RequestUserMessage(int Id)
    : ApiResultMessage<RequestUserMessage, int, UserProfileResponse>,
        ISelfApiResultMessage<RequestUserMessage, int, UserProfileResponse>
{
    protected override int Parameter => this.Id;
    public static RequestUserMessage Create(int id) => new (id);
}