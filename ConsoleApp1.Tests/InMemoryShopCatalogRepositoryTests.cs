using ConsoleApp1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class InMemoryShopCatalogRepositoryTests
{
    private InMemoryShopCatalogRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = new InMemoryShopCatalogRepository();
    }

    [TestMethod]
    public void GetAllEquipments_ShouldReturnEmptyList_WhenNoEquipmentsAdded()
    {
        var equipments = _repository.GetAllEquipments();

        Assert.AreEqual(0, equipments.Count);
    }

    [TestMethod]
    public void AddEquipment_ShouldAddEquipmentToRepository()
    {
        var equipment = MockEquipment("A");

        _repository.AddEquipment(equipment);
        var equipments = _repository.GetAllEquipments();

        Assert.AreEqual(1, equipments.Count);
        Assert.AreEqual(equipment, equipments[0]);
    }

    [TestMethod]
    public void GetByIndex_ShouldReturnCorrectEquipment_WhenIndexIsValid()
    {
        var equipment1 = MockEquipment("A");
        var equipment2 = MockEquipment("B");

        _repository.AddEquipment(equipment1);
        _repository.AddEquipment(equipment2);

        var result = _repository.GetByIndex(1);

        Assert.AreEqual(equipment2, result);
    }

    [TestMethod]
    public void GetByIndex_ShouldReturnNull_WhenIndexIsInvalid()
    {
        var equipment = MockEquipment("A");
        _repository.AddEquipment(equipment);

        var result = _repository.GetByIndex(10);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void RemoveEquipment_ShouldRemoveEquipmentFromRepository()
    {
        var equipment = MockEquipment("A");
        _repository.AddEquipment(equipment);

        _repository.RemoteEquipment(equipment);
        var equipments = _repository.GetAllEquipments();

        Assert.AreEqual(0, equipments.Count);
    }

    [TestMethod]
    public void RemoveEquipment_ShouldNotFail_WhenEquipmentDoesNotExist()
    {
        var equipment1 = MockEquipment("A");
        var equipment2 = MockEquipment("B");
        _repository.AddEquipment(equipment1);

        _repository.RemoteEquipment(equipment2);
        var equipments = _repository.GetAllEquipments();

        Assert.AreEqual(1, equipments.Count);
        Assert.AreEqual(equipment1, equipments[0]);
    }

    private static Equipment MockEquipment(string brand)
    {
        return new Equipment.Builder()
            .SetBrand(brand)
            .SetCpu("cpu")
            .SetCost(1)
            .SetRam(1)
            .Build();
    }
}