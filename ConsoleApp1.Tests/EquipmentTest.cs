using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EquipmentTests
{
    [TestClass]
    public class EquipmentTests
    {
        [TestMethod]
        public void Equipment_Should_StoreCorrectInfo()
        {
            string brand = "Lenovo";
            string cpu = "Intel i5";
            int ram = 8;
            int cost = 50000;

            Equipment equipment = new Equipment(brand, cpu, ram, cost);

            Assert.AreEqual(brand, equipment.Brand);
            Assert.AreEqual(cpu, equipment.CPU);
            Assert.AreEqual(ram, equipment.RAM);
            Assert.AreEqual(cost, equipment.Cost);
        }

        [TestMethod]
        public void Equipment_Should_PrintCorrectInfo()
        {
            var pc = new Pc("Dell", "Intel i7", 16, 70000);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            pc.PrintInfo();
            var output = consoleOutput.ToString();

            StringAssert.Contains(output, "Бренд: Dell, Процессор: Intel i7, ОЗУ: 16 Гб, Цена: 70000 руб.");
            StringAssert.Contains(output, "");
        }
        
    }
}