using BaCon;
using R3;
using System.Linq;

public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        var gameState = gameStateProvider.GameState;
        var settingsProvider = container.Resolve<ISettingsProvider>();
        var gameSettings = settingsProvider.GameSettings;

        container.RegisterInstance(AppConstants.EXIT_SCENE_REQUEST_TAG, new Subject<Unit>());

        var cmd = new CommandProcessor(gameStateProvider);
        cmd.RegisterHandler(new CmdPlaceEntityHandler(gameState));
        cmd.RegisterHandler(new CmdCreateMapHandler(gameState, gameSettings));
        cmd.RegisterHandler(new CmdResourcesAddHandler(gameState));
        cmd.RegisterHandler(new CmdResourcesSpendHandler(gameState));
        container.RegisterInstance<ICommandProcessor>(cmd);

        var loadingMapId = gameplayEnterParams.MapId;
        var loadingMap = gameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);
        if (loadingMap == null)
        {
            var command = new CmdCreateMap(loadingMapId);
            var success = cmd.Process(command);
            if (!success)
            {
                throw new System.Exception($"Couldn't create map state with id: {loadingMapId}");
            }

            loadingMap = gameState.Maps.First(m => m.Id == loadingMapId);
        }

        container.RegisterFactory(_ => new BuildingsService(loadingMap.Entities, gameSettings.BuildingsSettings, cmd)).AsSingle();
        container.RegisterFactory(_ => new ResourcesService(gameState.Resources, cmd)).AsSingle();

        // TODO: create definitions #
        container.RegisterFactory(c => new CheatsService(c.Resolve<BuildingsService>())).AsSingle();
    }
}
