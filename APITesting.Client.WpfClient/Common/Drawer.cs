using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace APITesting.Client.WpfClient.Common;

public partial class Drawer : ObservableObject
{ 
    [ObservableProperty] 
    private object? content;
    [ObservableProperty] 
    private bool isOpen;

    public Drawer()
    {
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
    }
}

public sealed record CloseDrawerMessage();

public class OpenDrawerMessage : AsyncRequestMessage
{
    public OpenDrawerMessage(object content) 
    {
        this.Content = content;
    } 
    public object Content { get; } 
}