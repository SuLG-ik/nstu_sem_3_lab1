using ConsoleApp1.Model;
using ConsoleApp1.Screen.NewEquipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApp1.Tests;

[TestClass]
public class NewBaseEquipmentScreenTests
{
    private Mock<IConsole> _mockConsole;
    private Mock<IShopCatalogRepository> _mockShopCatalogRepository;
    private NewBaseEquipmentScreen _screen;
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
        _screen = new NewBaseEquipmentScreen();
        _screen.Create(_mockNavigator.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ServiceLocator.Reset();
        _screen.Destroy();
    }

    [TestMethod]
    public void Display_ShouldAddEquipmentAndShowSuccessMessage()
    {
        // Arrange
        _mockConsole.SetupSequence(c => c.ReadString(It.IsAny<string>()))
            .Returns("Dell")
            .Returns("Intel i7");
        _mockConsole.SetupSequence(c => c.ReadInt(It.IsAny<string>())).Returns(16).Returns(1000);

        // Act
        _screen.Display();

        // Assert
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Equipment>()), Times.Once);
        _mockConsole.Verify(c => c.WriteLine("Компьютерная техника успешно добавлена"), Times.Once);
        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldRetryOnInvalidInputForBrand()
    {
        // Arrange
        _mockConsole.SetupSequence(c => c.ReadString(It.IsAny<string>()))
            .Throws(new ValidationNotBlankException("", "Бренд компьютерной техники", "Неверный бренд"))
            .Returns("Dell");
        _mockConsole.Setup(c => c.ReadString("Процессор компьютерной техники")).Returns("Intel i7");
        _mockConsole.Setup(c => c.ReadInt("Оперативная память компьютерной техники")).Returns(16);
        _mockConsole.Setup(c => c.ReadInt("Стоимость компьютерной техники")).Returns(1000);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Неправлиный ввод! Попробуйте ещё раз!"), Times.Once);
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Equipment>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldRetryOnInvalidInputForCpu()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadString("Бренд компьютерной техники")).Returns("Dell");
        _mockConsole.SetupSequence(c => c.ReadString("Процессор компьютерной техники"))
            .Throws(new ValidationNotBlankException("", "Процессор компьютерной техники", "Неверный процессор"))
            .Returns("Intel i7");
        _mockConsole.Setup(c => c.ReadInt("Оперативная память компьютерной техники")).Returns(16);
        _mockConsole.Setup(c => c.ReadInt("Стоимость компьютерной техники")).Returns(1000);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Неправлиный ввод! Попробуйте ещё раз!"), Times.Once);
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Equipment>()), Times.Once);
    }
}