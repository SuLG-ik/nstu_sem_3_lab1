using ConsoleApp1.Model;

namespace ConsoleApp1.Screen.NewEquipment;

public class NewPcScreen: Screen
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
        var builder = new Pc.Builder();
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
        _console.WriteLine("Персональный компьютер успешно добавлен");
    }

    private void ReadBrand(Pc.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write("Введите бренд персонального компьютера (обязательное поле): ");
            var brand = _console.ReadString("Бренд Персональный компьютера");
            builder.SetBrand(brand);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadCpu(Pc.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write("Введите модель процессора персонального компьютера (обязательное поле): ");
            var brand = _console.ReadString("Процессор Персональный компьютера");
            builder.SetCpu(brand);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadRam(Pc.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write(
                "Введите объём оперативной персонального компьютера в мегабайтах (обязательное поле, число больше 0): ");
            var ram = _console.ReadInt("Оперативная память Персональный компьютера");
            builder.SetRam(ram);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ReadCost(Pc.Builder builder)
    {
        Validator.RunUntilValid(() =>
        {
            _console.Write(
                "Введите стоимость персонального компьютера в рублях (обязательное поле, неотрицательное число): ");
            var ram = _console.ReadInt("Стоимость персонального компьютера");
            builder.SetCost(ram);
        }, onRetry: ShowIllegalInputMessage);
    }

    private void ShowIllegalInputMessage()
    {
        _console.WriteLine("Неправлиный ввод! Попробуйте ещё раз!");
    }
}