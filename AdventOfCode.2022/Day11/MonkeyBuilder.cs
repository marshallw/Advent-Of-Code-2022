using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace AdventOfCode._2022.Day11
{
    public class MonkeyBuilder
    {
        private int _trueOperation { get; set; }
        private int _falseOperation { get; set; }
        private int _test { get; set; }
        private Queue<long> _items { get; set; }
        private delegate int op(int value);
        Func<long, long> opFunc;
        public MonkeyBuilder()
        {
        }

        public MonkeyBuilder SetTrueOperation(int trueOperation)
        {
            _trueOperation = trueOperation;
            return this;
        }

        public MonkeyBuilder SetFalseOperation(int falseOperation)
        {
            _falseOperation = falseOperation;
            return this;
        }
        public MonkeyBuilder SetTest(int test)
        {
            _test = test;
            return this;
        }

        public MonkeyBuilder SetItems(IEnumerable<long> items)
        {
            _items = new Queue<long>(items.ToArray());
            return this;
        }

        public MonkeyBuilder setOperation(string operation, string value)
        {
            if (operation == "*" && value == "old")
                opFunc = x => x * x;
            else if (operation == "+")
                opFunc = x => Int32.Parse(value) + x;
            else if (operation == "*")
                opFunc = x => x * Int32.Parse(value);

            return this;
        }

        public Monkey Build()
        {
            return new Monkey(_items, _trueOperation, _falseOperation, _test, opFunc);
        }
    }
}
