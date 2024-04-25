namespace APITesting.Client.WpfClient.Users.SidePanel;

public sealed class EditSidePanelViewModel : SidePanelViewModel
{
    public override string Header { get; } = "Edit";
    
    public EditSidePanelViewModel(AppViewModel appViewModel) : base(appViewModel)
    {
    }
}