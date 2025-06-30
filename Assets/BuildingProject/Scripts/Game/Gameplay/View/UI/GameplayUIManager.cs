using BaCon;
using R3;

public class GameplayUIManager : UIManager
{
    private readonly Subject<Unit> exitScreenRequest;

    public GameplayUIManager(DIContainer container) : base(container) 
    {
        exitScreenRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
    }

    public ScreenGameplayViewModel OpenScreenGameplay()
    {
        var viewModel = new ScreenGameplayViewModel(this, exitScreenRequest);
        var rootUI = container.Resolve<UIGameplayRootViewModel>();
        rootUI.OpenScreen(viewModel);

        return viewModel;
    }

    public PopupAViewModel OpenPopupA()
    {
        var a = new PopupAViewModel();
        var rootUI = container.Resolve<UIGameplayRootViewModel>();
        rootUI.OpenPopup(a);

        return a;
    }

    public PopupBViewModel OpenPopupB()
    {
        var b = new PopupBViewModel();
        var rootUI = container.Resolve<UIGameplayRootViewModel>();
        rootUI.OpenPopup(b);

        return b;
    }
}