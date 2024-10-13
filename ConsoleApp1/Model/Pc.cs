namespace ConsoleApp1.Model;

public class Pc : Equipment
{
    private Pc(string brand, string cpu, int ram, int cost) : base(brand, cpu, ram, cost)
    {
    }

    public override string ToString()
    {
        return $"Персональный компьютер: бренд: {Brand}, CPU: {Cpu}, RAM: {Ram}, стоимость: {Cost}";
    }

    
    public new class Builder
    {
        private string? _brand;
        private string? _cpu;
        private int? _ram;
        private int? _cost;

        public Builder SetBrand(string brand)
        {
            _brand = Validator.RequireNotBlank(brand, "Бренд персонального копьютера");
            return this;
        }

        public Builder SetCpu(string cpu)
        {
            _cpu = Validator.RequireNotBlank(cpu, "Процессор персонального копьютера");
            return this;
        }

        public Builder SetRam(int ram)
        {
            _ram = Validator.RequireGreaterThan(ram, 0, "Оперативная память персонального копьютера");
            return this;
        }

        public Builder SetCost(int cost)
        {
            _cost = Validator.RequireGreaterOrEqualsThan(cost, 0, "Стоимость персонального копьютера");
            return this;
        }

        public Equipment Build()
        {
            var brand = Validator.RequireNotNull(_brand, "Бренд персонального копьютера");
            var cpu = Validator.RequireNotNull(_cpu, "Процессор персонального копьютера");
            var ram = Validator.RequireNotNull(_ram, "Оперативная память персонального копьютера");
            var cost = Validator.RequireNotNull(_cost, "Стоимость персонального копьютера");
            return new Pc(brand, cpu, ram, cost);
        }
    }
}