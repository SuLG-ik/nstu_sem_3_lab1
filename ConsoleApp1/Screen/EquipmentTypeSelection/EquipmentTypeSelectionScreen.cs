using ConsoleApp1.Screen.NewEquipment;

namespace ConsoleApp1.Screen.EquipmentTypeSelection;

public class EquipmentTypeSelectionScreen : Screen
{
    private IConsole _console;

    private readonly Dictionary<EquipmentType, string> _availableEquipments = new()
    {
        { EquipmentType.Equipment, "Компьютерная техника" },
        { EquipmentType.Pc, "Персональный компьютер" },
        { EquipmentType.Laptop, "Ноутбук" },
    };

    public override void Create(INavigator<Screen> navigator)
    {
        base.Create(navigator);
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        ShowInfo();
        switch (ReadType())
        {
            case EquipmentType.Equipment:
                Navigator?.ReplaceCurrentOrNavigate(new NewBaseEquipmentScreen());
                break;
            case EquipmentType.Pc:
                Navigator?.ReplaceCurrentOrNavigate(new NewPcScreen());
                break;
            case EquipmentType.Laptop:
                Navigator?.ReplaceCurrentOrNavigate(new NewLaptopScreen());
                break;
            default:
                ShowRetryMessage();
                break;
        }
    }

    private void ShowInfo()
    {
        _console.WriteLine("Доступные типы устройств:");
        foreach (var keyValuePair in _availableEquipments)
        {
            _console.WriteLine($"{(int)keyValuePair.Key}. {keyValuePair.Value}");
        }
    }

    private EquipmentType ReadType()
    {
        _console.Write("Выберите тип: ");
        return _console.ReadEnumUntilValid<EquipmentType>(onRetry: ShowRetryMessage);
    }

    private void ShowRetryMessage()
    {
        _console.Write("Такого утсройства нет! Попробуйте ещё раз: ");
    }
}