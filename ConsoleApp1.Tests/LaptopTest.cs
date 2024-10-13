using ConsoleApp1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class LaptopTests
{
    [TestMethod]
    public void LaptopBuilder_ShouldBuildLaptop_WhenAllValuesAreValid()
    {
        // Act
        var laptop = new Laptop.Builder()
            .SetBrand("Asus")
            .SetCpu("Intel i5")
            .SetRam(8)
            .SetCost(1200)
            .Build();

        // Assert
        Assert.AreEqual("Asus", laptop.Brand);
        Assert.AreEqual("Intel i5", laptop.Cpu);
        Assert.AreEqual(8, laptop.Ram);
        Assert.AreEqual(1200, laptop.Cost);
    }

    [TestMethod]
    public void LaptopBuilder_ShouldThrowException_WhenBrandIsBlank()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => 
            new Laptop.Builder().SetBrand("").Build()
        );
    }

    [TestMethod]
    public void LaptopBuilder_ShouldThrowException_WhenCpuIsBlank()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => 
            new Laptop.Builder().SetCpu("").Build()
        );
    }

    [TestMethod]
    public void LaptopBuilder_ShouldThrowException_WhenRamIsNotPositive()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => 
            new Laptop.Builder().SetRam(0).Build()
        );
    }

    [TestMethod]
    public void LaptopBuilder_ShouldThrowException_WhenCostIsNegative()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => 
            new Laptop.Builder().SetCost(-100).Build()
        );
    }

    [TestMethod]
    public void LaptopToString_ShouldReturnFormattedString()
    {
        // Arrange
        var laptop = new Laptop.Builder()
            .SetBrand("HP")
            .SetCpu("AMD Ryzen 7")
            .SetRam(16)
            .SetCost(1500)
            .Build();

        // Act
        var result = laptop.ToString();

        // Assert
        Assert.AreEqual("Ноутбук: бренд: HP, CPU: AMD Ryzen 7, RAM: 16, стоимость: 1500", result);
    }
}