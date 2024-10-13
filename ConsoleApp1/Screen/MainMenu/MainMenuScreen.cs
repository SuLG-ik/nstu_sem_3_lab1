using ConsoleApp1.Screen.EquipmentsList;
using ConsoleApp1.Screen.EquipmentTypeSelection;

namespace ConsoleApp1.Screen.MainMenu;

public class MainMenuScreen : Screen
{
    private IConsole _console;

    public override void Create(INavigator<Screen> navigator)
    {
        base.Create(navigator);
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        ShowMenu();
        switch (ReadSelection())
        {
            case MainMenuNextScreenSelection.NewDevice:
                Navigator?.NavigateTo(new EquipmentTypeSelectionScreen());
                break;
            case MainMenuNextScreenSelection.PrintDevices:
                Navigator?.NavigateTo(new ShowEquipmentsListScreen());
                break;
            case MainMenuNextScreenSelection.Quit:
                Navigator?.Back();
                break;
            default:
                ShowRetryMessage();
                break;
        }
    }

    private void ShowMenu()
    {
        _console.WriteLine("Меню:");
        _console.WriteLine("1. Добавить устройство");
        _console.WriteLine("2. Печать списка");
        _console.WriteLine("3. Выход");
    }

    private MainMenuNextScreenSelection ReadSelection()
    {
        _console.Write("Выберите действие: ");
        return _console.ReadEnumUntilValid<MainMenuNextScreenSelection>(onRetry: ShowRetryMessage);
    }

    private void ShowRetryMessage()
    {
        _console.WriteLine("Такого пункта меню нет! Попробуйте снова: ");
    }
}