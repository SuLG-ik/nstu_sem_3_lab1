using System;
using ConsoleApp1.Screen.EquipmentTypeSelection;
using ConsoleApp1.Screen.NewEquipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApp1.Tests;

[TestClass]
public class EquipmentTypeSelectionScreenTests
{
    private Mock<IConsole> _mockConsole;
    private EquipmentTypeSelectionScreen _screen;
    private Mock<INavigator<Screen.Screen>> _mockNavigator;

    [TestInitialize]
    public void Setup()
    {
        _mockConsole = new Mock<IConsole>();

        // Register mock IConsole in ServiceLocator
        ServiceLocator.Register(_mockConsole.Object);

        _mockNavigator = new Mock<INavigator<Screen.Screen>>();
        _screen = new EquipmentTypeSelectionScreen();
        _screen.Create(_mockNavigator.Object);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        ServiceLocator.Reset();
        _screen.Destroy();
    }

    [TestMethod]
    public void Display_ShouldNavigateToNewBaseEquipmentScreen_WhenEquipmentTypeSelected()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadEnumUntilValid<EquipmentType>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns(EquipmentType.Equipment);

        // Act
        _screen.Display();

        // Assert
        _mockNavigator.Verify(n => n.ReplaceCurrentOrNavigate(It.IsAny<NewBaseEquipmentScreen>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldNavigateToNewPcScreen_WhenPcTypeSelected()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadEnumUntilValid<EquipmentType>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns(EquipmentType.Pc);

        // Act
        _screen.Display();

        // Assert
        _mockNavigator.Verify(n => n.ReplaceCurrentOrNavigate(It.IsAny<NewPcScreen>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldNavigateToNewLaptopScreen_WhenLaptopTypeSelected()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadEnumUntilValid<EquipmentType>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns(EquipmentType.Laptop);

        // Act
        _screen.Display();

        // Assert
        _mockNavigator.Verify(n => n.ReplaceCurrentOrNavigate(It.IsAny<NewLaptopScreen>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldShowRetryMessage_WhenInvalidTypeSelected()
    {
        // Arrange
        _mockConsole.Setup(c => c.ReadEnumUntilValid<EquipmentType>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns((EquipmentType)(-1));

        // Act
        _screen.Display();

        // Assert
        _mockConsole.Verify(c => c.Write("Такого утсройства нет! Попробуйте ещё раз: "), Times.Once);
    }
}