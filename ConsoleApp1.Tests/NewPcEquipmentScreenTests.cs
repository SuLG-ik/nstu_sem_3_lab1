using ConsoleApp1.Model;
using ConsoleApp1.Screen.NewEquipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApp1.Tests;

[TestClass]
public class NewPcScreenTests
{
    private Mock<IConsole> _mockConsole;
    private Mock<IShopCatalogRepository> _mockShopCatalogRepository;
    private NewPcScreen _screen;
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
        _screen = new NewPcScreen();
        _screen.Create(_mockNavigator.Object);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        ServiceLocator.Reset();
        _screen.Destroy();
    }

    [TestMethod]
    public void Display_ShouldAddPcAndShowSuccessMessage()
    {
        // Arrange
        _mockConsole.SetupSequence(c => c.ReadString(It.IsAny<string>()))
            .Returns("Dell")
            .Returns("Intel i7");
        _mockConsole.SetupSequence(c => c.ReadInt(It.IsAny<string>())).Returns(16).Returns(70000);

        // Act
        _screen.Display();

        // Assert
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Pc>()), Times.Once);
        _mockConsole.Verify(c => c.WriteLine("Персональный компьютер успешно добавлен"), Times.Once);
        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldRetryOnInvalidInputForBrand()
    {
        // Arrange
        _mockConsole.SetupSequence(c => c.ReadString("Бренд Персональный компьютера"))
            .Throws(new ValidationNotBlankException("", "Бренд персонального компьютера", "Неверный бренд"))
            .Returns("Dell");
        _mockConsole.Setup(c => c.ReadString("Процессор Персональный компьютера")).Returns("Intel i7");
        _mockConsole.Setup(c => c.ReadInt("Оперативная память Персональный компьютера")).Returns(16);
        _mockConsole.Setup(c => c.ReadInt("Стоимость персонального компьютера")).Returns(70000);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Неправлиный ввод! Попробуйте ещё раз!"), Times.Once);
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Pc>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldRetryOnInvalidInputForCpu()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadString("Бренд Персональный компьютера")).Returns("Dell");
        _mockConsole.SetupSequence(c => c.ReadString("Процессор Персональный компьютера"))
            .Throws(new ValidationNotBlankException("", "Процессор персонального компьютера", "Неверный процессор"))
            .Returns("Intel i7");
        _mockConsole.Setup(c => c.ReadInt("Оперативная память Персональный компьютера")).Returns(16);
        _mockConsole.Setup(c => c.ReadInt("Стоимость персонального компьютера")).Returns(70000);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Неправлиный ввод! Попробуйте ещё раз!"), Times.Once);
        _mockShopCatalogRepository.Verify(r => r.AddEquipment(It.IsAny<Pc>()), Times.Once);
    }
}