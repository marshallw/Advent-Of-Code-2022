using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day9
{
    public class Knot
    {
        private int _x { get; set; }
        private int _y { get; set; }
        // thought i may need to keep track of how many times each square was traveled, turns out this was unnecessary!
        private Dictionary<(int, int), int> _traveled { get; set; }
        private Knot _next { get; set; }

        public Knot(int x, int y, Knot next)
        {
            _x = x;
            _y = y;
            _traveled = new Dictionary<(int, int), int>();
            _traveled.Add((_x, _y), 1);
            _next = next;
        }

        public bool NextKnotExists() => _next != null;

        public int X => _x;
        public int Y => _y;

        public Knot GetHead()
        {
            if (_next.NextKnotExists())
                return _next.GetHead();
            return _next;
        }

        public void QueueChase()
        {
            if (_next != null)
            {
                _next.QueueChase();
                Chase(_next);
            }
        }

        public void Chase(Knot head)
        {
            //int pathX;
            //int pathY;

            while(IsNotNextTo(head))
            {
                //pathX = 0;
                //pathY = 0;

                _x += (head.X - _x != 0) ? (head.X - _x) / Math.Abs(head.X - _x): 0;
                _y += (head.Y - _y != 0) ? (head.Y - _y) / Math.Abs(head.Y - _y) : 0;

                // original code, keeping for legacy
                //if (head.X - _x != 0)
                //    pathX = (head.X - _x) / Math.Abs(head.X - _x);
                //if (head.Y - _y != 0)
                //    pathY = (head.Y - _y) / Math.Abs(head.Y - _y);
                //_x += pathX;
                //_y += pathY;
                if (!_traveled.ContainsKey((_x, _y)))
                    _traveled.Add((_x, _y), 1);
            }
        }
        public int GetNumTraveled() => _traveled.Count();

        public void FollowInstruction(Instruction instruction)
        {
            // kinda unnecessary and overkill...
            if (instruction.Dir.Dir == Direction.Up().Dir)
                _y -= instruction.Distance;
            if (instruction.Dir.Dir == Direction.Down().Dir)
                _y += instruction.Distance;
            if (instruction.Dir.Dir == Direction.Left().Dir)
                _x -= instruction.Distance;
            if (instruction.Dir.Dir == Direction.Right().Dir)
                _x += instruction.Distance;
        }

        public void FollowInstructionSlowly(Direction dir)
        {
            FollowInstruction(new Instruction(dir, 1));
        }

        private bool IsNotNextTo(Knot head)
        {
            bool result = false;
            if (Math.Abs(head.X - _x) > 1)
                result = true;
            if (Math.Abs(head.Y - _y) > 1)
                result = true;
            return result;
        }
    }
}
