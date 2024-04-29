namespace APITesting.Client.WpfClient.Drawer;

public class OpenDrawerMessage : AsyncRequestMessage
{
    public OpenDrawerMessage(object content) 
    {
        this.Content = content;
    } 
    public object Content { get; } 
}