public class Equipment
{
    public string Brand { get; }
    public string CPU { get; }
    public int RAM { get; set; }
    public int Cost { get; set; }

    public Equipment(string brand, string cpu, int ram, int cost)
    {
        Brand = brand;
        CPU = cpu;
        RAM = ram;
        Cost = cost;
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"Бренд: {Brand}, Процессор: {CPU}, ОЗУ: {RAM} Гб, Цена: {Cost} руб.");
    }
}