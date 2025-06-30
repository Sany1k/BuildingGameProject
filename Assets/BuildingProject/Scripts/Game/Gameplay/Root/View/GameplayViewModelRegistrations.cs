using BaCon;

public static class GameplayViewModelRegistrations
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new GameplayUIManager(container)).AsSingle();
        container.RegisterFactory(c => new UIGameplayRootViewModel(c.Resolve<CheatsService>())).AsSingle();
        container.RegisterFactory(c => new WorldGameplayRootViewModel(
                c.Resolve<BuildingsService>(),
                c.Resolve<ResourcesService>()
                )).AsSingle();
    }
}
