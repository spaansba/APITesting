using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace APITesting.Client.WpfClient.Common;

public class AsyncRequestMessage
{
    private Task? response;
    public Task Response
    {
        get
        {
            if (!HasReceivedResponse)
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

        if (HasReceivedResponse)
        {
            ThrowInvalidOperationExceptionForDuplicateReply();
        }

        HasReceivedResponse = true;

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