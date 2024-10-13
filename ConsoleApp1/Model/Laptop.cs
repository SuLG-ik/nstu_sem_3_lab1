namespace ConsoleApp1.Model;

public class Laptop : Equipment
{
    private Laptop(string brand, string cpu, int ram, int cost) : base(brand, cpu, ram, cost)
    {
    }

    public override string ToString()
    {
        return $"Ноутбук: бренд: {Brand}, CPU: {Cpu}, RAM: {Ram}, стоимость: {Cost}";
    }

    
    public new class Builder
    {
        private string? _brand;
        private string? _cpu;
        private int? _ram;
        private int? _cost;

        public Builder SetBrand(string brand)
        {
            _brand = Validator.RequireNotBlank(brand, "Бренд ноутбука");
            return this;
        }

        public Builder SetCpu(string cpu)
        {
            _cpu = Validator.RequireNotBlank(cpu, "Процессор ноутбука");
            return this;
        }

        public Builder SetRam(int ram)
        {
            _ram = Validator.RequireGreaterThan(ram, 0, "Оперативная память ноутбука");
            return this;
        }

        public Builder SetCost(int cost)
        {
            _cost = Validator.RequireGreaterOrEqualsThan(cost, 0, "Стоимость ноутбука");
            return this;
        }

        public Equipment Build()
        {
            var brand = Validator.RequireNotNull(_brand, "Бренд ноутбука");
            var cpu = Validator.RequireNotNull(_cpu, "Процессор ноутбука");
            var ram = Validator.RequireNotNull(_ram, "Оперативная память ноутбука");
            var cost = Validator.RequireNotNull(_cost, "Стоимость ноутбука");
            return new Laptop(brand, cpu, ram, cost);
        }
    }
}