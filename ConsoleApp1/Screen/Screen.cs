namespace ConsoleApp1.Screen;

public abstract class Screen
{
    protected INavigator<Screen>? Navigator { get; private set; }

    public virtual void Create(INavigator<Screen> navigator)
    {
        Navigator = navigator;
    }

    public abstract void Display();

    public virtual void Destroy()
    {
        Navigator = null;
    }
}