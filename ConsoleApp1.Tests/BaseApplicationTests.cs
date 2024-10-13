using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class BaseApplicationTests
{
    private BaseApplication _application;
    private INavigator<Screen.Screen> _navigator;
    private TestScreen _screen;

    [TestInitialize]
    public void SetUp()
    {
        _screen = new TestScreen();
        _navigator = new StackNavigator<Screen.Screen>(_screen);
        _application = new BaseApplication(_navigator);
    }

    [TestCleanup]
    public void CleanUp()
    {
        _application.Destroy();
    }

    [TestMethod]
    public void Application_Should_InitializeNavigator_WhenCreated()
    {
        _application.Create();

        Assert.IsTrue(ServiceLocator.Contains<IConsole>());
        Assert.IsTrue(ServiceLocator.Contains<IShopCatalogRepository>());
        Assert.IsNotNull(_application.Navigator);
    }

    [TestMethod]
    public void Application_Should_RunWithValidNavigator()
    {
        // Arrange
        _application.Create();
        var screen = (TestScreen)_navigator.CurrentScreen;

        // Act
        _application.Run();
        
        Assert.IsTrue(screen.IsCreated);
        Assert.IsTrue(screen.IsDisplayed);
        Assert.IsTrue(screen.IsDestroyed);
    }

    [TestMethod]
    public void Application_Should_NotRun_WhenNavigatorIsNull()
    {
        // Arrange
        _application.Create();
        _application.Destroy();

        _application.Run();

        // Assert
        Assert.IsFalse(_screen.IsCreated);
        Assert.IsFalse(_screen.IsDisplayed);
        Assert.IsFalse(_screen.IsDestroyed);
    }

    [TestMethod]
    public void Application_Should_CreateDisplayDestroyNewScreen_WhenRun()
    {
        // Arrange
        _application.Create();

        // Act
        _application.Run();

        // Assert
        var currentScreen = _navigator.CurrentScreen;
        Assert.IsNull(currentScreen);
    }

    private class TestScreen : Screen.Screen
    {
        public bool IsCreated { get; private set; }
        public bool IsDisplayed { get; private set; }

        public bool IsDestroyed { get; private set; }

        public override void Create(INavigator<Screen.Screen> navigator)
        {
            base.Create(navigator);
            IsCreated = true;
        }

        public override void Display()
        {
            IsDisplayed = true;
            Navigator?.Back();
        }

        public override void Destroy()
        {
            base.Destroy();
            IsDestroyed = true;
        }
    }
}