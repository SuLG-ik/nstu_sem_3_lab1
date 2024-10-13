using ConsoleApp1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class EquipmentTests
{
    [TestMethod]
    public void EquipmentBuilder_ShouldBuildEquipment_WhenAllValuesAreValid()
    {
        // Act
        var equipment = new Equipment.Builder()
            .SetBrand("Dell")
            .SetCpu("Intel i7")
            .SetRam(16)
            .SetCost(1000)
            .Build();

        // Assert
        Assert.AreEqual("Dell", equipment.Brand);
        Assert.AreEqual("Intel i7", equipment.Cpu);
        Assert.AreEqual(16, equipment.Ram);
        Assert.AreEqual(1000, equipment.Cost);
    }

    [TestMethod]
    public void EquipmentBuilder_ShouldThrowException_WhenBrandIsBlank()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => 
            new Equipment.Builder().SetBrand("").Build()
        );
    }

    [TestMethod]
    public void EquipmentBuilder_ShouldThrowException_WhenCpuIsBlank()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => 
            new Equipment.Builder().SetCpu("").Build()
        );
    }

    [TestMethod]
    public void EquipmentBuilder_ShouldThrowException_WhenRamIsNotPositive()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => 
            new Equipment.Builder().SetRam(0).Build()
        );
    }

    [TestMethod]
    public void EquipmentBuilder_ShouldThrowException_WhenCostIsNegative()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => 
            new Equipment.Builder().SetCost(-100).Build()
        );
    }

    [TestMethod]
    public void EquipmentToString_ShouldReturnFormattedString()
    {
        // Arrange
        var equipment = new Equipment.Builder()
            .SetBrand("HP")
            .SetCpu("AMD Ryzen 5")
            .SetRam(8)
            .SetCost(800)
            .Build();

        // Act
        var result = equipment.ToString();

        // Assert
        Assert.AreEqual("Компьютерная техника: бренд: HP, CPU: AMD Ryzen 5, RAM: 8, стоимость: 800", result);
    }
}