using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EquipmentTests
{
    [TestClass]
    public class PcTests
    {
        [TestMethod]
        public void Pc_Should_StoreCorrectInfo()
        {
            var brand = "Lenovo";
            var cpu = "Intel i5";
            var ram = 8;
            var cost = 50000;

            var equipment = new Pc(brand, cpu, ram, cost);

            Assert.AreEqual(brand, equipment.Brand);
            Assert.AreEqual(cpu, equipment.CPU);
            Assert.AreEqual(ram, equipment.RAM);
            Assert.AreEqual(cost, equipment.Cost);
        }


        [TestMethod]
        public void Pc_Should_PrintCorrectInfo()
        {
            var laptop = new Pc("HP", "Intel i5", 8, 60000);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            laptop.PrintInfo();
            var output = consoleOutput.ToString();

            StringAssert.Contains(output, "Тип: Персональный компьютер, Бренд: HP, Процессор: Intel i5, ОЗУ: 8 Гб, Цена: 60000 руб.");
            StringAssert.Contains(output, "");
        }
    }
}