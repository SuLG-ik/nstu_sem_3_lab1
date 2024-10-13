using ConsoleApp1.Model;

namespace ConsoleApp1.Screen.NewEquipment;

public class NewLaptopScreen : Screen
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
        var builder = new Laptop.Builder();
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
        _console.WriteLine("Ноутбук успешно добавлен");
    }

    private void ReadBrand(Laptop.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write("Введите бренд ноутбука (обязательное поле): ");
            var brand = _console.ReadString("Бренд ноутбука");
            builder.SetBrand(brand);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadCpu(Laptop.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write("Введите модель процессора ноутбука (обязательное поле): ");
            var brand = _console.ReadString("Процессор ноутбука");
            builder.SetCpu(brand);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadRam(Laptop.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write(
                "Введите объём оперативной ноутбука в мегабайтах (обязательное поле, число больше 0): ");
            var ram = _console.ReadInt("Оперативная память ноутбука");
            builder.SetRam(ram);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadCost(Laptop.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write(
                "Введите стоимость ноутбука в рублях (обязательное поле, неотрицательное число): ");
            var ram = _console.ReadInt("Стоимость ноутбука");
            builder.SetCost(ram);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ShowIllegalInputMessage()
    {
        _console.WriteLine("Неправлиный ввод! Попробуйте ещё раз!");
    }
}