using ConsoleApp1.Screen.MainMenu;

namespace ConsoleApp1;

public abstract class Program
{
    private static void Main()
    {
        var navigator = new StackNavigator<Screen.Screen>(new MainMenuScreen());
        Application application = new BaseApplication(navigator);
        application.Create();
        application.Run();
        application.Destroy();
    }
}