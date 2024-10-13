using ConsoleApp1.Model;
using ConsoleApp1.Screen.NewEquipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApp1.Tests;

[TestClass]
public class NewLaptopScreenTests
{
    private Mock<IConsole> _mockConsole;
    private Mock<IShopCatalogRepository> _mockShopCatalogRepository;
    private NewLaptopScreen _screen;
    private Mock<INavigator<Screen.Screen>> _mockNavigator;

    [TestInitialize]
    public void Setup()
    {
        _mockConsole = new Mock<IConsole>();
        _mockShopCatalogRepository = new Mock<IShopCatalogRepository>();

        // Register mocks in ServiceLocator
        ServiceLocator.Register(_mockConsole.Object);
        ServiceLocator.Register(_mockShopCatalogRepository.Object);

        _mockNavigator = new Mock<INavigator<Screen.Screen>>();
        _screen = new NewLaptopScreen();
        _screen.Create(_mockNavigator.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ServiceLocator.Reset();
        _screen.Destroy();
    }
    
    [TestMethod]
    public void Display_ShouldAddLaptopAndShowSuccessMessage()
    {
        // Arrange
        _mockConsole.SetupSequence(c => c.ReadString(It.IsAny<string>()))
            .Returns("HP")
            .Returns("Intel i5");
        _mockConsole.SetupSequence(c => c.ReadInt(It.IsAny<string>())).Returns(8).Returns(50000);

        // Act
        _screen.Display();

        // Assert
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Laptop>()), Times.Once);
        _mockConsole.Verify(c => c.WriteLine("Ноутбук успешно добавлен"), Times.Once);
        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldRetryOnInvalidInputForBrand()
    {
        // Arrange
        _mockConsole.SetupSequence(c => c.ReadString("Бренд ноутбука"))
            .Throws(new ValidationNotBlankException("", "Бренд ноутбука", "Неверный бренд"))
            .Returns("HP");
        _mockConsole.Setup(c => c.ReadString("Процессор ноутбука")).Returns("Intel i5");
        _mockConsole.Setup(c => c.ReadInt("Оперативная память ноутбука")).Returns(8);
        _mockConsole.Setup(c => c.ReadInt("Стоимость ноутбука")).Returns(50000);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Неправлиный ввод! Попробуйте ещё раз!"), Times.Once);
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Laptop>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldRetryOnInvalidInputForCpu()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadString("Бренд ноутбука")).Returns("HP");
        _mockConsole.SetupSequence(c => c.ReadString("Процессор ноутбука"))
            .Throws(new ValidationNotBlankException("", "Процессор ноутбука", "Неверный процессор"))
            .Returns("Intel i5");
        _mockConsole.Setup(c => c.ReadInt("Оперативная память ноутбука")).Returns(8);
        _mockConsole.Setup(c => c.ReadInt("Стоимость ноутбука")).Returns(50000);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Неправлиный ввод! Попробуйте ещё раз!"), Times.Once);
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Laptop>()), Times.Once);
    }
}