using System;
using System.Collections.Generic;

class ConsoleApp1
{
    private static void Main()
    {
        var equipment = new List<Equipment>();

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить устройство:");
            Console.WriteLine("2. Печать списка");
            Console.WriteLine("3. Выход:");
            Console.Write("Выберите действие: ");
            var choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("\nВыберите тип устройства:");
                Console.WriteLine("1. Компьютерная техника");
                Console.WriteLine("2. Персональный компьютер");
                Console.WriteLine("3. Ноутбук");
                Console.Write("Введите номер типа: ");
                int type = int.Parse(Console.ReadLine());

                Equipment device = null;

                if (type == 1)
                {
                    Console.Write("\nВведите бренд компьютерной техники: ");
                    string brand = Console.ReadLine();
                    Console.Write("Введите бренд процессора: ");
                    string cpu = Console.ReadLine();
                    Console.Write("Введите объём оперативной памяти (Гб): ");
                    int ram = int.Parse(Console.ReadLine());
                    Console.Write("Введите цену (Руб.): ");
                    int cost = int.Parse(Console.ReadLine());
                    device = new Equipment(brand, cpu, ram, cost);
                }
                else if (type == 2)
                {
                    Console.Write("\nВведите бренд компьютерной техники: ");
                    string brand = Console.ReadLine();
                    Console.Write("Введите бренд процессора: ");
                    string cpu = Console.ReadLine();
                    Console.Write("Введите объём оперативной памяти (Гб): ");
                    int ram = int.Parse(Console.ReadLine());
                    Console.Write("Введите цену (Руб.): ");
                    int cost = int.Parse(Console.ReadLine());
                    device = new Pc(brand, cpu, ram, cost);
                }
                else if (type == 3)
                {
                    Console.Write("\nВведите бренд компьютерной техники: ");
                    string brand = Console.ReadLine();
                    Console.Write("Введите бренд процессора: ");
                    string cpu = Console.ReadLine();
                    Console.Write("Введите объём оперативной памяти (Гб): ");
                    int ram = int.Parse(Console.ReadLine());
                    Console.Write("Введите цену (Руб.): ");
                    int cost = int.Parse(Console.ReadLine());
                    device = new Laptop(brand, cpu, ram, cost);
                }

                if (device != null)
                {
                    equipment.Add(device);
                    Console.WriteLine("Устройство добавлено в список.");
                }
                else
                {
                    Console.WriteLine("Неверный тип устройства.");
                }
            }
            else if (choice == 2)
            {
                if (equipment.Count == 0)
                {
                    Console.WriteLine("\nСписок устройств пуст.");
                }
                else
                {
                    Console.WriteLine("\nСписок устройств:");
                    foreach (var device in equipment)
                    {
                        device.PrintInfo();
                        Console.WriteLine("--------------------");
                    }
                }
            }
            else if (choice == 3)
            {
                equipment.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
        }
    }
}