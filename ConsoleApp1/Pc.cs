class Pc : Equipment
{
    public Pc(string brand, string cpu, int ram, int cost)
        : base(brand, cpu, ram, cost)
    {
    }

    public override void PrintInfo()
    {
        Console.Write("Тип: Персональный компьютер, ");
        base.PrintInfo();
    }
}