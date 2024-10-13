using System;
using ConsoleApp1.Screen.EquipmentsList;
using ConsoleApp1.Screen.EquipmentTypeSelection;
using ConsoleApp1.Screen.MainMenu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApp1.Tests;

[TestClass]
public class MainMenuScreenTests
{
    private Mock<IConsole> _mockConsole;
    private MainMenuScreen _mainMenuScreen;
    private Mock<INavigator<Screen.Screen>> _mockNavigator;

    [TestInitialize]
    public void Setup()
    {
        _mockConsole = new Mock<IConsole>();
        ServiceLocator.Register(_mockConsole.Object);

        _mockNavigator = new Mock<INavigator<Screen.Screen>>();
        _mainMenuScreen = new MainMenuScreen();
        _mainMenuScreen.Create(_mockNavigator.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _mainMenuScreen.Destroy();
        ServiceLocator.Reset();
    }

    [TestMethod]
    public void Display_ShouldNavigateToEquipmentTypeSelectionScreen_WhenNewDeviceIsSelected()
    {
        _mockConsole.SetupSequence(c =>
                c.ReadEnumUntilValid<MainMenuNextScreenSelection>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns(MainMenuNextScreenSelection.NewDevice);

        _mainMenuScreen.Display();

        _mockNavigator.Verify(n => n.NavigateTo(It.IsAny<EquipmentTypeSelectionScreen>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldNavigateToShowEquipmentsListScreen_WhenPrintDevicesIsSelected()
    {
        _mockConsole.SetupSequence(c =>
                c.ReadEnumUntilValid<MainMenuNextScreenSelection>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns(MainMenuNextScreenSelection.PrintDevices);

        _mainMenuScreen.Display();

        _mockNavigator.Verify(n => n.NavigateTo(It.IsAny<ShowEquipmentsListScreen>()), Times.Once);
    }

    [TestMethod]
    public void Display_ShouldNavigateBack_WhenQuitIsSelected()
    {
        _mockConsole.SetupSequence(c =>
                c.ReadEnumUntilValid<MainMenuNextScreenSelection>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns(MainMenuNextScreenSelection.Quit);

        _mainMenuScreen.Display();

        _mockNavigator.Verify(n => n.Back(), Times.Once);
    }

    [TestMethod]
    public void ShowMenu_ShouldWriteMenuToConsole()
    {
        _mockConsole.SetupSequence(c =>
                c.ReadEnumUntilValid<MainMenuNextScreenSelection>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns((MainMenuNextScreenSelection)1);
        _mainMenuScreen.Display();

        _mockConsole.Verify(c => c.WriteLine("Меню:"), Times.Once);
        _mockConsole.Verify(c => c.WriteLine("1. Добавить устройство"), Times.Once);
        _mockConsole.Verify(c => c.WriteLine("2. Печать списка"), Times.Once);
        _mockConsole.Verify(c => c.WriteLine("3. Выход"), Times.Once);
        _mockConsole.Verify(c => c.Write("Выберите действие: "), Times.Once);
    }

    [TestMethod]
    public void ShowRetryMessage_ShouldWriteRetryMessageToConsole()
    {
        _mockConsole.SetupSequence(c =>
                c.ReadEnumUntilValid<MainMenuNextScreenSelection>(It.IsAny<string>(), It.IsAny<Action>()))
            .Returns((MainMenuNextScreenSelection)999);

        _mainMenuScreen.Display();

        _mockConsole.Verify(c => c.Write("Такого пункта меню нет! Попробуйте снова: "), Times.Never);
    }
}