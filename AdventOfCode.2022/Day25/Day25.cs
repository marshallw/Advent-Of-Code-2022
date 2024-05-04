using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day25
{
    public class Day25 : IDay<string, long>
    {
        public string[] _input { get; }

        public Day25()
        {
            _input = File.ReadAllLines(@"Day25\input.txt");
        }
        public string Part1()
        {
            long result = 0;
            foreach(string s in _input)
            {
                result += SnafuInterpreter(s);
            }
            return DoubleToSnafu(result);
        }

        public long SnafuInterpreter(string snafu)
        {
            int length = snafu.Length - 1;
            double result = 0;
            foreach(char digit in snafu)
            {
                if (char.IsDigit(digit))
                {
                    int workingNumber = Int32.Parse(new String(new char[] { digit }));
                    result += workingNumber * (Math.Pow(5, length));
                }
                else
                {
                    switch (digit)
                    {
                        case '-':
                            result += -1 * (Math.Pow(5, length));
                            break;

                        case '=':
                            result += -2 * (Math.Pow(5, length));
                            break;
                    }
                }
                length--;
            }
            return Convert.ToInt64(result);
        }

        public string DoubleToSnafu(long digit)
        {
            long dividend = digit / 5;
            long remainder = digit % 5;
            string result = $"{remainder}";

            if (remainder == 3)
            {
                dividend++;
                result = "=";
            }
            else if (remainder == 4)
            {
                dividend++;
                result = "-";
            }

            if (dividend == 0 && remainder <= 2)
                return $"{remainder}";

            return DoubleToSnafu(dividend) + result;
        }

        public long Part2()
        {
            throw new NotImplementedException();
        }
    }
}
