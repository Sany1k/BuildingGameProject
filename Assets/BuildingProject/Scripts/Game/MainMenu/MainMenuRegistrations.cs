using BaCon;

public static class MainMenuRegistrations
{
    public static void Register(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
    {
        container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
    }
}
