using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day11
{
    public class Day11 : IDay<long, long>
    {
        private List<string> _input { get; }
        private Stack<string> instructions { get; set; }

        public Day11()
        {
            _input = File.ReadAllLines("Day11/input.txt").ToList();
        }

        public long Part1()
        {
            Monkey[] monkeys = new Monkey[8];
            instructions = new Stack<string>(_input.Select(x => x.Trim()).Reverse().ToArray());
            int monkeyId = 0;

            monkeys = GetMonkeys();

            for (int i = 0; i < 20; i++)
            {
                Monkeybusiness(monkeys);
            }
            return monkeys.Select(x => x.ItemsHandled).OrderByDescending(x => x).Take(2).Aggregate((x, y) => x * y);
        }

        public void Monkeybusiness(Monkey[] monkeys)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.HasItems())
                {
                    var result = monkey.ProcessItem();
                    monkeys[result.Item1].GetItem(result.Item2);
                }
            }
        }

        public void Monkeybusiness2(Monkey[] monkeys)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.HasItems())
                {
                    var result = monkey.ProcessItem2();
                    monkeys[result.Item1].GetItem(result.Item2);
                }
            }
        }

        public Monkey[] GetMonkeys()
        {
            MonkeyBuilder builder = new MonkeyBuilder();
            List<Monkey> monkeys = new List<Monkey>();
            while (instructions.Count > 0)
            {
                while (instructions.Count > 0 && instructions.Peek() != "")
                {
                    string line = instructions.Pop();
                    if (line.StartsWith("Starting items:"))
                    {
                        builder.SetItems(line.Replace("Starting items: ", "").Replace(",", "").Split(' ').Select(long.Parse));
                    }
                    else if (line.StartsWith("Operation:"))
                    {
                        builder.setOperation(line.Split(' ').Reverse().Skip(1).First(), line.Split(' ').Last());
                    }
                    else if (line.StartsWith("Test: "))
                    {
                        builder.SetTest(line.Split(' ').Select(Int32.Parse).Last());
                    }
                    else if (line.StartsWith("If true: "))
                    {
                        builder.SetTrueOperation(line.Split(' ').Select(Int32.Parse).Last());
                    }
                    else if (line.StartsWith("If false: "))
                    {
                        builder.SetFalseOperation(line.Split(' ').Select(Int32.Parse).Last());
                    }
                }
                monkeys.Add(builder.Build());
                if (instructions.Count > 0)
                    instructions.Pop();
            }
            return monkeys.ToArray();
        }

        public long Part2()
        {
            Monkey[] monkeys = new Monkey[8];
            instructions = new Stack<string>(_input.Select(x => x.Trim()).Reverse().ToArray());
            int monkeyId = 0;

            monkeys = GetMonkeys();

            for (int i = 0; i < 10000; i++)
            {
                Monkeybusiness2(monkeys);

                if (monkeys.Any(x => x.AllItemsAboveLCM()))
                    foreach (var monkey in monkeys)
                        monkey.ReduceByLCD();

                if (i + 1 == 20 || i + 1 == 1000 || i + 1 == 2000)
                    foreach(var monkey in monkeys)
                    {
                        Console.WriteLine($"Monkey {i} inspected items {monkey.ItemsHandled}");
                    }
            }
            return monkeys.Select(x => x.ItemsHandled).OrderByDescending(x => x).Take(2).Aggregate((x, y) => x * y);
        }
    }
}
