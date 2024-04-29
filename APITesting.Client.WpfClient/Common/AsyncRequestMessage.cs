using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace APITesting.Client.WpfClient.Common;

public interface IAsyncRequestMessageBase
{
    public Task Response { get; }
    public bool HasReceivedResponse { get; }
}
public interface IAsyncRequestMessage : IAsyncRequestMessageBase
{
    public void Reply(Task replyResponse);
    [EditorBrowsable(EditorBrowsableState.Never)]
    public TaskAwaiter GetAwaiter();
}

public interface IAsyncRequestMessage<T> : IAsyncRequestMessageBase
{
    public new Task<T> Response { get; }
    Task IAsyncRequestMessageBase.Response => this.Response;
    public void Reply(T replyResponse);
    public void Reply(Task<T> replyResponse);
    [EditorBrowsable(EditorBrowsableState.Never)]
    public TaskAwaiter<T> GetAwaiter();
}

/// <summary>
/// A <see langword="class"/> for async request messages, which can either be used directly or through derived classes.
/// </summary>
/// <typeparam name="T">The type of request to make.</typeparam>
public abstract record AsyncRecordRequestMessage<T> : IAsyncRequestMessage<T>
{
    private Task<T>? response;

    /// <summary>
    /// Gets the message response.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="HasReceivedResponse"/> is <see langword="false"/>.</exception>
    public Task<T> Response
    {
        get
        {
            if (!this.HasReceivedResponse)
            {
                ThrowInvalidOperationExceptionForNoResponseReceived();
            }

            return this.response!;
        }
    }

    /// <summary>
    /// Gets a value indicating whether a response has already been assigned to this instance.
    /// </summary>
    public bool HasReceivedResponse { get; private set; }

    /// <summary>
    /// Replies to the current request message.
    /// </summary>
    /// <param name="replyResponse">The response to use to reply to the request message.</param>
    /// <exception cref="InvalidOperationException">Thrown if it <see cref="Response"/> has already been set.</exception>
    public void Reply(T replyResponse)
    {
        this.Reply(Task.FromResult(replyResponse));
    }

    /// <summary>
    /// Replies to the current request message.
    /// </summary>
    /// <param name="replyResponse">The response to use to reply to the request message.</param>
    /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="replyResponse"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if it <see cref="Response"/> has already been set.</exception>
    public void Reply(Task<T> replyResponse)
    {
        ArgumentNullException.ThrowIfNull(replyResponse);

        if (this.HasReceivedResponse)
        {
            ThrowInvalidOperationExceptionForDuplicateReply();
        }

        this.HasReceivedResponse = true;

        this.response = replyResponse;
    }

    /// <inheritdoc cref="Task{T}.GetAwaiter"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public TaskAwaiter<T> GetAwaiter()
    {
        return this.Response.GetAwaiter();
    }

    /// <summary>
    /// Throws a <see cref="InvalidOperationException"/> when a response is not available.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowInvalidOperationExceptionForNoResponseReceived()
    {
        throw new InvalidOperationException("No response was received for the given request message.");
    }

    /// <summary>
    /// Throws a <see cref="InvalidOperationException"/> when <see cref="Reply(T)"/> or <see cref="Reply(Task{T})"/> are called twice.
    /// </summary>
    [DoesNotReturn]
    private static void ThrowInvalidOperationExceptionForDuplicateReply()
    {
        throw new InvalidOperationException("A response has already been issued for the current message.");
    }
}


public abstract record AsyncRecordRequestMessage : IAsyncRequestMessage
{
    private Task? response;
    public Task Response
    {
        get
        {
            if (!this.HasReceivedResponse)
            {
                ThrowInvalidOperationExceptionForNoResponseReceived();
            }

            return this.response!;
        }
    }

    public bool HasReceivedResponse { get; private set; }

    public void Reply(Task replyResponse)
    {
        ArgumentNullException.ThrowIfNull(replyResponse);

        if (this.HasReceivedResponse)
        {
            ThrowInvalidOperationExceptionForDuplicateReply();
        }

        this.HasReceivedResponse = true;

        this.response = replyResponse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] [EditorBrowsable(EditorBrowsableState.Never)]
    public TaskAwaiter GetAwaiter()
    {
        return this.Response.GetAwaiter();
    }

    [DoesNotReturn]
    private static void ThrowInvalidOperationExceptionForNoResponseReceived()
    {
        throw new InvalidOperationException("No response was received for the given request message.");
    }

    [DoesNotReturn]
    private static void ThrowInvalidOperationExceptionForDuplicateReply()
    {
        throw new InvalidOperationException("A response has already been issued for the current message.");
    }
}