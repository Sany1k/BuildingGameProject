using R3;

public class ScreenGameplayViewModel : WindowViewModel
{
    private readonly GameplayUIManager uiManager;
    private readonly Subject<Unit> exitSceenRequest;

    public override string Id => "ScreenGameplay";

    public ScreenGameplayViewModel(GameplayUIManager uiManager, Subject<Unit> exitSceenRequest)
    {
        this.uiManager = uiManager;
        this.exitSceenRequest = exitSceenRequest;
    }

    public void RequestOpenPopupA()
    {
        uiManager.OpenPopupA();
    }

    public void RequestOpenPopupB()
    {
        uiManager.OpenPopupB();
    }

    public void RequestGoToMainMenu()
    {
        exitSceenRequest.OnNext(Unit.Default);
    }
}
