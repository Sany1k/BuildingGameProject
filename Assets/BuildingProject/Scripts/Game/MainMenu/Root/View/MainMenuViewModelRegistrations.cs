using BaCon;

public static class MainMenuViewModelRegistrations
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(_ => new UIMainMenuRootViewModel()).AsSingle();
    }
}
