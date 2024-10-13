namespace ConsoleApp1.Screen.EquipmentsList;

public class ShowEquipmentsListScreen : Screen
{
    private IConsole _console;
    private IShopCatalogRepository _shopCatalogRepository;

    public override void Create(INavigator<Screen> navigator)
    {
        base.Create(navigator);
        _shopCatalogRepository = ServiceLocator.GetService<IShopCatalogRepository>();
        _console = ServiceLocator.GetService<IConsole>();
    }

    public override void Display()
    {
        ShowEquipments();
        Navigator?.Back();
    }

    private void ShowEquipments()
    {
        var equipments = _shopCatalogRepository.GetAllEquipments();
        if (equipments.Count == 0)
        {
            _console?.WriteLine("Список устройств пуст");
        }
        else
        {
            _console.WriteLine("Список устройств:");
            for (var i = 0; i < equipments.Count; i++)
            {
                _console.WriteLine($"{i + 1}. {equipments[i]}");
            }
        }
    }
}