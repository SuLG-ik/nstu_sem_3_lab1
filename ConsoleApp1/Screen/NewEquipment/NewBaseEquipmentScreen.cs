using ConsoleApp1.Model;

namespace ConsoleApp1.Screen.NewEquipment;

public class NewBaseEquipmentScreen : Screen
{
    private IConsole _console;

    private IShopCatalogRepository _shopCatalogRepository;

    public override void Create(INavigator<Screen> navigator)
    {
        base.Create(navigator);
        _console = ServiceLocator.GetService<IConsole>();
        _shopCatalogRepository = ServiceLocator.GetService<IShopCatalogRepository>();
    }

    public override void Display()
    {
        var builder = new Equipment.Builder();
        ReadBrand(builder);
        ReadCpu(builder);
        ReadRam(builder);
        ReadCost(builder);
        _shopCatalogRepository.AddEquipment(builder.Build());
        ShowSuccessMessage();
        Navigator?.Back();
    }

    private void ShowSuccessMessage()
    {
        _console.WriteLine("Компьютерная техника успешно добавлена");
    }

    private void ReadBrand(Equipment.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write("Введите бренд компьютерной техники (обязательное поле): ");
            var brand = _console.ReadString("Бренд компьютерной техники");
            builder.SetBrand(brand);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadCpu(Equipment.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write("Введите модель процессора компьютерной техники (обязательное поле): ");
            var brand = _console.ReadString("Процессор компьютерной техники");
            builder.SetCpu(brand);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadRam(Equipment.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write(
                "Введите объём оперативной компьютерной техники в мегабайтах (обязательное поле, число больше 0): ");
            var ram = _console.ReadInt("Оперативная память компьютерной техники");
            builder.SetRam(ram);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadCost(Equipment.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write(
                "Введите стоимость компьютерной техники в рублях (обязательное поле, неотрицательное число): ");
            var ram = _console.ReadInt("Стоимость компьютерной техники");
            builder.SetCost(ram);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ShowIllegalInputMessage()
    {
        _console.WriteLine("Неправлиный ввод! Попробуйте ещё раз!");
    }
}