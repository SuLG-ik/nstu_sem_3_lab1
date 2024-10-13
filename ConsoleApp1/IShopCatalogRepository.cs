using ConsoleApp1.Model;

namespace ConsoleApp1;

public interface IShopCatalogRepository
{
    public List<Equipment> GetAllEquipments();
    public Equipment? GetByIndex(int index);
    public void AddEquipment(Equipment equipment);
    public void RemoteEquipment(Equipment equipment);
}