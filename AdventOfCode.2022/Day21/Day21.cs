using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day21
{
    internal class Day21 : IDay<long, long>
    {
        public string[] _input { get; }

        public Day21()
        {
            _input = File.ReadAllLines(@"Day21\input.txt");
        }
        public long Part1()
        {
            var monkeys = Create(_input);

            do
            {
                foreach(KeyValuePair<string, string> pair in monkeys)
                {
                    if (long.TryParse(pair.Value, out var value))
                    {

                    }
                    else
                    {
                        var operation = Parse(pair.Value);
                        if (long.TryParse(monkeys[operation.monkey1], out var value1) && long.TryParse(monkeys[operation.monkey2], out var value2))
                        {
                            if (operation.op == '+')
                            {
                                monkeys[pair.Key] = (value1 + value2).ToString();
                            }
                            else if (operation.op == '-')
                            {
                                monkeys[pair.Key] = (value1 - value2).ToString();
                            }
                            else if (operation.op == '*')
                            {
                                monkeys[pair.Key] = (value1 * value2).ToString();
                            }
                            else if (operation.op == '/')
                            {
                                monkeys[pair.Key] = (value1 / value2).ToString();
                            }
                        }
                    }


                }
            } while (!long.TryParse(monkeys.Where(x => x.Key == "root").First().Value, out var result2));
            long.TryParse(monkeys.Where(x => x.Key == "root").First().Value, out var result);
            return result;
        }

        public (string monkey1, char op, string monkey2) Parse(string input)
        {
            var split = input.Split(" ");
            return (split[0], split[1][0], split[2]);
        }

        public Dictionary<string, string> Create(string[] input)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(var line in input)
            {
                var split = line.Split(" ");
                var value = line.Substring(6);
                result.Add(split[0].Replace(":", ""), value);
            }
            return result;
        }

        public long WorkBackwards(string monkey, Dictionary<string, string> monkeys)
        {
            var root = Parse(monkeys[monkey]);
            var monkey1 = monkeys[root.monkey1];
            var monkey2 = monkeys[root.monkey2];
            if (long.TryParse(monkey1, out var num))
            {
                return WorkBackwards(root.monkey2, num, root.op, monkeys);
            }
            else
            {
                long.TryParse(monkey2, out var num2);
                return WorkBackwards(root.monkey1, num2, root.op, monkeys);
            }
        }

        public long WorkBackwards(string monkey, long value, char op, Dictionary<string, string> monkeys)
        {
            if (monkey == "humn")
                return value;
            var root = Parse(monkeys[monkey]);
            var monkey1 = monkeys[root.monkey1];
            var monkey2 = monkeys[root.monkey2];
            long result = 0;

            if (long.TryParse(monkey1, out var num))
            {
                if (root.op == '+')
                {
                    result = value - num;
                }
                else if (root.op == '-')
                {
                    result = num - value;
                }
                else if (root.op == '*')
                {
                    result = value / num;
                }
                else if (root.op == '/')
                {
                    result = num / value;
                }
                return WorkBackwards(root.monkey2, result, root.op, monkeys);
            }
            else
            {
                long.TryParse(monkey2, out var num2);
                if (root.op == '+')
                {
                    result = value - num2;
                }
                else if (root.op == '-')
                {
                    result = num2 + value;
                }
                else if (root.op == '*')
                {
                    result = value / num2;
                }
                else if (root.op == '/')
                {
                    result = value * num2;
                }
                return WorkBackwards(root.monkey1, result, root.op, monkeys);
            }
        }

        public long Part2()
        {
            var monkeys = Create(_input);
            int entriesModified = 0;
            monkeys["humn"] = "i have no idea what i'm doing";

            do
            {
                entriesModified = 0;
                foreach (KeyValuePair<string, string> pair in monkeys)
                {
                    if (pair.Key == "root")
                    {

                    }
                    else if (pair.Key == "humn")
                    {

                    }
                    else
                    {

                        if (long.TryParse(pair.Value, out var value))
                        {

                        }
                        else
                        {
                            var operation = Parse(pair.Value);
                            if (long.TryParse(monkeys[operation.monkey1], out var value1) && long.TryParse(monkeys[operation.monkey2], out var value2))
                            {
                                if (operation.op == '+')
                                {
                                    monkeys[pair.Key] = (value1 + value2).ToString();
                                }
                                else if (operation.op == '-')
                                {
                                    monkeys[pair.Key] = (value1 - value2).ToString();
                                }
                                else if (operation.op == '*')
                                {
                                    monkeys[pair.Key] = (value1 * value2).ToString();
                                }
                                else if (operation.op == '/')
                                {
                                    monkeys[pair.Key] = (value1 / value2).ToString();
                                }
                                entriesModified++;
                            }
                        }
                    }


                }
            } while (entriesModified > 0);

            var root = Parse(monkeys["root"]);
            var monkey1 = monkeys[root.monkey1];
            var monkey2 = monkeys[root.monkey2];



            return WorkBackwards("root", monkeys);
        }
    }
}
