using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace AdventOfCode._2022.Day11
{
    public class Monkey
    {
        private Queue<long> _worryItem { get; set; }
        private int _trueOperation { get; }
        private int _falseOperation { get; }
        private int _test { get; }
        private delegate int op(int value);
        Func<long, long> operation;
        public long ItemsHandled { get; private set; }

        public Monkey(Queue<long> worryItem, int trueOperation, int falseOperation, int test, Func<long, long> o)
        {
            ItemsHandled = 0;
            _trueOperation = trueOperation;
            _falseOperation = falseOperation;
            _test = test;
            operation = o;
            _worryItem = worryItem;
        }

        public bool HasItems()
        {
            return _worryItem.Any();
        }

        public void GetItem(long item) => _worryItem.Enqueue(item);


        public (int, long) ProcessItem()
        {
            ItemsHandled++;
            var item = operation(_worryItem.Dequeue());
            item = item / 3;

            if (item % _test == 0)
            {
                return (_trueOperation, item);
            }
            return (_falseOperation, item);
        }

        public (int, long) ProcessItem2()
        {
            ItemsHandled++;
            var item = operation(_worryItem.Dequeue());
            item = item % 9699690;
            if (item % _test == 0)
            {
                return (_trueOperation, item);
            }
            return (_falseOperation, item);
        }

        public bool AllItemsAboveLCM()
        {
            return _worryItem.All(x => x >= 9699690);
        }

        public void ReduceByLCD()
        {
            _worryItem = new Queue<long>(_worryItem.Select(x => x % 9699690));
        }
    }
}
