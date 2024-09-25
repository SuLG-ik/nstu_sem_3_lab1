using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EquipmentTests
{
    [TestClass]
    public class LaptopTests
    {
        [TestMethod]
        public void Laptop_Should_StoreCorrectInfo()
        {
            string brand = "Lenovo";
            string cpu = "Intel i5";
            int ram = 8;
            int cost = 50000;

            var equipment = new Laptop(brand, cpu, ram, cost);

            Assert.AreEqual(brand, equipment.Brand);
            Assert.AreEqual(cpu, equipment.CPU);
            Assert.AreEqual(ram, equipment.RAM);
            Assert.AreEqual(cost, equipment.Cost);
        }


        [TestMethod]
        public void Laptop_Should_PrintCorrectInfo()
        {
            var laptop = new Laptop("HP", "Intel i5", 8, 60000);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            laptop.PrintInfo();
            var output = consoleOutput.ToString();

            StringAssert.Contains(output, "Тип: Ноутбук, Бренд: HP, Процессор: Intel i5, ОЗУ: 8 Гб, Цена: 60000 руб.");
            StringAssert.Contains(output, "");
        }
    }
}