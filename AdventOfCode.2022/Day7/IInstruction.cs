using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    public interface IInstruction
    {
        string GetInstruction();
        string GetArgument();
    }
}
