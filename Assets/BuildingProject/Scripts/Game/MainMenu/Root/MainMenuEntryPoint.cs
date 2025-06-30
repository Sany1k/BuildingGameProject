using BaCon;
using R3;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder sceneUIRootPrefab;

    public Observable<MainMenuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)
    {
        MainMenuRegistrations.Register(mainMenuContainer, enterParams);
        var mainMenuViewModelsContainer = new DIContainer(mainMenuContainer);
        MainMenuViewModelRegistrations.Register(mainMenuViewModelsContainer);

        // For test:
        mainMenuViewModelsContainer.Resolve<UIMainMenuRootViewModel>();

        var uiRoot = mainMenuContainer.Resolve<UIRootView>();
        var uiScene = Instantiate(sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSignalSubj);

        Debug.Log($"MAIN MANU ENTRY POINT: Run main menu scene. Result: {enterParams?.Result}");

        var gameplayEnterParams = new GameplayEnterParams(0);
        var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
        var exitToGameplaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

        return exitToGameplaySceneSignal;
    }
}
