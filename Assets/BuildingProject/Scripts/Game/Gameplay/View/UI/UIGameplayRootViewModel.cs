public class UIGameplayRootViewModel : UIRootViewModel
{
    public readonly CheatPanelViewModel cheatPanelViewModel;

    public UIGameplayRootViewModel(CheatsService cheatsService)
    {
        cheatPanelViewModel = new(cheatsService);
    }
}
