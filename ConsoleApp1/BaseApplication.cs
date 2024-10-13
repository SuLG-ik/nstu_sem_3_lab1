namespace ConsoleApp1;

public class BaseApplication(INavigator<Screen.Screen> navigator) : Application
{
    public override INavigator<Screen.Screen>? Navigator { get; protected set; }

    public override void Create()
    {
        ServiceLocator.Register<IConsole>(new SystemConsole());
        ServiceLocator.Register<IShopCatalogRepository>(new InMemoryShopCatalogRepository());
        Navigator = navigator;
    }

    public override void Destroy()
    {
        base.Destroy();
        ServiceLocator.Reset();
    }
}