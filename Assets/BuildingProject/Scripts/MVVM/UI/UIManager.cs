using BaCon;

public abstract class UIManager
{
    protected readonly DIContainer container;

    protected UIManager(DIContainer container)
    {
        this.container = container;
    }
}
