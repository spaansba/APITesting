using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace APITesting.Client.WpfClient.Common;

public partial class Drawer : ObservableObject
{ 
    [ObservableProperty] 
    private object? content;

    // That task's IsCompleted property is either true (drawer has been opened, and is now closed) or false.
    private TaskCompletionSource drawerClosedCompletionSource = new();
    private Task DrawerClosed => drawerClosedCompletionSource.Task;
    
    private bool isOpen;
    public bool IsOpen
    {
        get => this.isOpen;
        set 
        {
            if (!this.SetProperty(ref this.isOpen, value))
            {
                return;
            }

            if (value) // If now open and closed before
            {
                // Create a new TaskCompletionSource (which will create a new sync Task)
                drawerClosedCompletionSource = new();
            }
            else // We are now closed (we were open before)
            {
                // This will mark the task completed
                drawerClosedCompletionSource.TrySetResult();
            }
        }
    }


    public Drawer()
    {
        // Everytime a OpenDrawerMessage or CloseDrawerMessage message is send to the WeakReferenceMessenger
        // The drawer class will listen to this message and call either method
        WeakReferenceMessenger.Default.Register<Drawer, OpenDrawerMessage>(this, OpenDrawer);
        WeakReferenceMessenger.Default.Register<Drawer, CloseDrawerMessage>(this, CloseDrawer);
    }
    
    private static void CloseDrawer(Drawer recipient, CloseDrawerMessage message)
    {
        recipient.Content = null;
        recipient.IsOpen = false;
    }


    private static void OpenDrawer(Drawer recipient, OpenDrawerMessage message)
    {
        recipient.Content = message.Content;
        recipient.IsOpen = true;
        message.Reply(recipient.DrawerClosed);
    }
}

public sealed record CloseDrawerMessage();


public record OpenDrawerMessage(object Content) : AsyncRecordRequestMessage;