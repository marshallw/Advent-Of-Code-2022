using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    public class ElfFile
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public ElfFile(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
