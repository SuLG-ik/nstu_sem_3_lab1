class Laptop : Equipment
{
    public Laptop(string brand, string cpu, int ram, int cost)
        : base(brand, cpu, ram, cost)
    {
    }

    public override void PrintInfo()
    {
        Console.Write("Тип: Ноутбук, ");
        base.PrintInfo();
    }
}