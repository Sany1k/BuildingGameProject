using System.Collections;
using BaCon;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private Coroutines coroutines;
    private UIRootView uiRoot;
    private readonly DIContainer rootContainer = new();
    private DIContainer cashedSceneContainer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        instance = new GameEntryPoint();
        instance.RunGame();
    }

    private GameEntryPoint()
    {
        coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(uiRoot.gameObject);
        rootContainer.RegisterInstance(uiRoot);

        var settingsProvider = new SettingsProvider();
        rootContainer.RegisterInstance<ISettingsProvider>(settingsProvider);

        var gameStateProvider = new PlayerPrefsGameStateProvider();
        gameStateProvider.LoadSettingsState();
        rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);

        rootContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();
    }

    private async void RunGame()
    {
        await rootContainer.Resolve<ISettingsProvider>().LoadGameSettings();

#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == Scenes.GAMEPLAY)
        {
            var enterParams = new GameplayEnterParams(0);
            coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
            return;
        }

        if (sceneName == Scenes.MAIN_MENU)
            coroutines.StartCoroutine(LoadAndStartMainMenu());

        if (sceneName != Scenes.BOOT)
            return;
#endif
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
    {
        uiRoot.ShowLoadingScreen();
        cashedSceneContainer?.Dispose();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAMEPLAY);

        yield return new WaitForSeconds(1);

        var isGameStateLoaded = false;
        rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
        yield return new WaitUntil(() => isGameStateLoaded);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        var gameplayContainer = cashedSceneContainer = new DIContainer(rootContainer);
        sceneEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParams => 
        { 
            coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams)); 
        });

        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
    {
        uiRoot.ShowLoadingScreen();
        cashedSceneContainer?.Dispose();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForSeconds(1);

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        var mainMenuContainer = cashedSceneContainer = new DIContainer(rootContainer);
        sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams =>
        {
            var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

            if (targetSceneName == Scenes.GAMEPLAY)
            {
                coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
            }
        });

        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
