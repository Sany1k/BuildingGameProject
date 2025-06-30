using BaCon;
using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGameplayRootBinder sceneUIRootPrefab;
    [SerializeField] private WorldGameplayRootBinder worldRootBinder;

    public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
    {
        GameplayRegistrations.Register(gameplayContainer, enterParams);
        var gameplayViewModelsContainer = new DIContainer(gameplayContainer);
        GameplayViewModelRegistrations.Register(gameplayViewModelsContainer);

        InitWorld(gameplayViewModelsContainer);
        InitUI(gameplayViewModelsContainer);

        Debug.Log($"GAMEPLAY ENTRY POINT: level to load = {enterParams.MapId}");

        var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
        var exitParams = new GameplayExitParams(mainMenuEnterParams);
        var exitSceneRequest = gameplayContainer.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        var exitToMainMenuSceneSignal = exitSceneRequest.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }

    private void InitWorld(DIContainer viewsContainer)
    {
        worldRootBinder.Bind(viewsContainer.Resolve<WorldGameplayRootViewModel>());
    }

    private void InitUI(DIContainer viewsContainer)
    {
        var uiRoot = viewsContainer.Resolve<UIRootView>();
        var uiSceneRootBinder = Instantiate(sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

        var uiSceenRootViewModel = viewsContainer.Resolve<UIGameplayRootViewModel>();
        uiSceneRootBinder.Bind(uiSceenRootViewModel);

        var uiManager = viewsContainer.Resolve<GameplayUIManager>();
        uiManager.OpenScreenGameplay();
    }
}
