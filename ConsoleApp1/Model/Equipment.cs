namespace ConsoleApp1.Model;

public class Equipment
{
    protected Equipment(string brand, string cpu, int ram, int cost)
    {
        Brand = brand;
        Cpu = cpu;
        Ram = ram;
        Cost = cost;
    }

    public string Brand { get; }
    public string Cpu { get; }
    public int Ram { get; }
    public int Cost { get; }

    public override string ToString()
    {
        return $"Компьютерная техника: бренд: {Brand}, CPU: {Cpu}, RAM: {Ram}, стоимость: {Cost}";
    }

    public new class Builder
    {
        private string? _brand;
        private string? _cpu;
        private int? _ram;
        private int? _cost;

        public Builder SetBrand(string brand)
        {
            _brand = Validator.RequireNotBlank(brand, "Бренд компьютерной техники");
            return this;
        }

        public Builder SetCpu(string cpu)
        {
            _cpu = Validator.RequireNotBlank(cpu, "Процессор компьютерной техники");
            return this;
        }

        public Builder SetRam(int ram)
        {
            _ram = Validator.RequireGreaterThan(ram, 0, "Оперативная память компьютерной техники");
            return this;
        }

        public Builder SetCost(int cost)
        {
            _cost = Validator.RequireGreaterOrEqualsThan(cost, 0, "Стоимость компьютерной техники");
            return this;
        }

        public Equipment Build()
        {
            var brand = Validator.RequireNotNull(_brand, "Бренд компьютерной техники");
            var cpu = Validator.RequireNotNull(_cpu, "Процессор компьютерной техники");
            var ram = Validator.RequireNotNull(_ram, "Оперативная память компьютерной техники");
            var cost = Validator.RequireNotNull(_cost, "Стоимость компьютерной техники");
            return new Equipment(brand, cpu, ram, cost);
        }
    }
}