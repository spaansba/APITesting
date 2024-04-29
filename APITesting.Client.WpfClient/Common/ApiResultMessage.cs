using APITesting.Client.Result;
using CommunityToolkit.Mvvm.Messaging;

namespace APITesting.Client.WpfClient.Common;

public abstract record ApiResultMessage<TSelf, TResult> : AsyncRecordRequestMessage<ApiResult<TResult>>
    where TSelf : ApiResultMessage<TSelf, TResult>, new()
    where TResult : notnull
{
    public static async Task<TResult> SendOrThrow()
        => (await WeakReferenceMessenger.Default.Send(new TSelf())).GetValueOrThrow();
    
    // Making the register process simpler
    public static void Register<TRecipient>(
        TRecipient recipient,
        Func<TRecipient, ITestClient> getTestClient,
        Func<ITestClient, Task<ApiResult<TResult>>> getResult) where TRecipient : class
    {

        WeakReferenceMessenger.Default.Register<TRecipient, TSelf>(
            recipient,
            (localRecipient, message) => message.Reply(getResult(getTestClient(localRecipient)))
        );
    }
}

public interface IApiResultMessage<out TParameter, TResult> : IAsyncRequestMessage<ApiResult<TResult>>
    where TResult : notnull
{
    public TParameter Parameter { get; }
}

public interface ISelfApiResultMessage<out TSelf, in TParameter, TResult> : IAsyncRequestMessage<ApiResult<TResult>>
    where TSelf : ISelfApiResultMessage<TSelf, TParameter, TResult>
    where TResult : notnull
{
    public static abstract TSelf Create(TParameter parameter);
}

public abstract record ApiResultMessage<TSelf, TParameter, TResult> : AsyncRecordRequestMessage<ApiResult<TResult>>, IApiResultMessage<TParameter, TResult>
    where TSelf : ApiResultMessage<TSelf, TParameter, TResult>, IApiResultMessage<TParameter, TResult>, ISelfApiResultMessage<TSelf, TParameter, TResult>
    where TResult : notnull
{
    protected abstract TParameter Parameter { get; }
    TParameter IApiResultMessage<TParameter, TResult>.Parameter => this.Parameter;

    public static async Task<TResult> SendOrThrow(TParameter parameter)
        => (await WeakReferenceMessenger.Default.Send(TSelf.Create(parameter))).GetValueOrThrow();

    public static void Register<TRecipient>(
        TRecipient recipient,
        Func<TRecipient, ITestClient> getTestClient,
        Func<ITestClient, TParameter, Task<ApiResult<TResult>>> getResult
    )
        where TRecipient : class
        => WeakReferenceMessenger.Default.Register<TRecipient, TSelf>(
            recipient,
            (localRecipient, message) => message.Reply(getResult(getTestClient(localRecipient), message.Parameter))
        );
}
