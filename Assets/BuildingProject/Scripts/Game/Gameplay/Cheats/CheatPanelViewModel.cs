public class CheatPanelViewModel
{
    private readonly CheatsService cheatsService;

    public CheatPanelViewModel(CheatsService cheatsService)
    {
        this.cheatsService = cheatsService;
    }

    public void HandleCheatApplying(string cheatText)
    {
        cheatsService.TryApplyCheat(cheatText);
    }
}
