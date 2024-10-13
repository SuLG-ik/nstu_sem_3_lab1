using ConsoleApp1.Model;

namespace ConsoleApp1;

public class InMemoryShopCatalogRepository : IShopCatalogRepository
{
    private readonly List<Equipment> _equipments = [];

    public List<Equipment> GetAllEquipments()
    {
        return _equipments;
    }

    public Equipment? GetByIndex(int index)
    {
        return index < _equipments.Count ? _equipments[index] : null;
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipments.Add(equipment);
    }

    public void RemoteEquipment(Equipment equipment)
    {
        _equipments.Remove(equipment);
    }
}