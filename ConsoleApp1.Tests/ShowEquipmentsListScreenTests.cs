using System.Collections.Generic;
using ConsoleApp1.Model;
using ConsoleApp1.Screen.EquipmentsList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApp1.Tests;

[TestClass]
public class ShowEquipmentsListScreenTests
{
    private Mock<IConsole> _mockConsole;
    private Mock<IShopCatalogRepository> _mockShopCatalogRepository;
    private ShowEquipmentsListScreen _screen;
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
        _screen = new ShowEquipmentsListScreen();
        _screen.Create(_mockNavigator.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        ServiceLocator.Reset();
        _screen.Destroy();
    }

    [TestMethod]
    public void Display_ShouldShowEmptyMessage_WhenNoEquipmentsAreAvailable()
    {
        // Arrange
        _mockShopCatalogRepository.Setup(r => r.GetAllEquipments()).Returns(new List<Equipment>());

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Список устройств пуст"), Times.Once);
        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldShowEquipments_WhenEquipmentsAreAvailable()
    {
        // Arrange
        var equipments = new List<Equipment>
        {
            new Equipment.Builder().SetBrand("Dell").SetCpu("Intel i7").SetRam(16).SetCost(1000).Build(),
            new Equipment.Builder().SetBrand("HP").SetCpu("AMD Ryzen 5").SetRam(8).SetCost(800).Build(),
        };
        _mockShopCatalogRepository.Setup(r => r.GetAllEquipments()).Returns(equipments);

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.WriteLine("Список устройств:"), Times.Once);
        _mockConsole.Verify(
            c => c.WriteLine("1. Компьютерная техника: бренд: Dell, CPU: Intel i7, RAM: 16, стоимость: 1000"),
            Times.Once);
        _mockConsole.Verify(
            c => c.WriteLine("2. Компьютерная техника: бренд: HP, CPU: AMD Ryzen 5, RAM: 8, стоимость: 800"),
            Times.Once);
        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldNavigateBack_AfterShowingEquipments()
    {
        _mockShopCatalogRepository.Setup(r => r.GetAllEquipments()).Returns(new List<Equipment>());
        // Act
        _screen.Display();

        // Assert
        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }
}