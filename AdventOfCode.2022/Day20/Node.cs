using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day20
{
    public class Node
    {
        public long Value { get; }
        public int OriginalIndex { get; }
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public bool Root { get; set; }

        public Node(long value, int originalIndex, bool root)
        {
            Value = value;
            OriginalIndex = originalIndex;
            Root = root;
        }

        public void GoForward()
        {
            if (Root)
            {
                Next.Root = true;
                Root = false;
            }
            else if (Next.Root)
            {
                Next.Root = false;
                Root = true;
            }
            var next = Next.Next;
            Previous.Next = Next;
            Next.Previous = Previous;
            Next.Next.Previous = this;
            Next.Next = this;
            Previous = Next;
            this.Next = next;       
        }

        public void GoBackward()
        {
            if (Root)
            {
                Next.Root = true;
                Root = false;

            }
            else if (Previous.Root)
            {
                Previous.Root = false;
                Root = true;
            }
            var previous = Previous.Previous;
            Next.Previous = Previous;
            Previous.Next = Next;
            Previous.Previous.Next = this;
            Previous.Previous = this;
            Next = Previous;
            Previous = previous;
        }
    }
}
