using ConsoleApp1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class PcTests
{
    [TestMethod]
    public void PcBuilder_ShouldBuildPc_WhenAllValuesAreValid()
    {
        // Act
        var pc = new Pc.Builder()
            .SetBrand("Acer")
            .SetCpu("Intel i9")
            .SetRam(32)
            .SetCost(2000)
            .Build();

        // Assert
        Assert.AreEqual("Acer", pc.Brand);
        Assert.AreEqual("Intel i9", pc.Cpu);
        Assert.AreEqual(32, pc.Ram);
        Assert.AreEqual(2000, pc.Cost);
    }

    [TestMethod]
    public void PcBuilder_ShouldThrowException_WhenBrandIsBlank()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => 
            new Pc.Builder().SetBrand("").Build()
        );
    }

    [TestMethod]
    public void PcBuilder_ShouldThrowException_WhenCpuIsBlank()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotBlankException>(() => 
            new Pc.Builder().SetCpu("").Build()
        );
    }

    [TestMethod]
    public void PcBuilder_ShouldThrowException_WhenRamIsNotPositive()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => 
            new Pc.Builder().SetRam(0).Build()
        );
    }

    [TestMethod]
    public void PcBuilder_ShouldThrowException_WhenCostIsNegative()
    {
        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => 
            new Pc.Builder().SetCost(-100).Build()
        );
    }

    [TestMethod]
    public void PcToString_ShouldReturnFormattedString()
    {
        // Arrange
        var pc = new Pc.Builder()
            .SetBrand("Dell")
            .SetCpu("AMD Ryzen 9")
            .SetRam(64)
            .SetCost(3000)
            .Build();

        // Act
        var result = pc.ToString();

        // Assert
        Assert.AreEqual("Персональный компьютер: бренд: Dell, CPU: AMD Ryzen 9, RAM: 64, стоимость: 3000", result);
    }
}