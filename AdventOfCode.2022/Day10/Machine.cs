using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day10
{
    public class Machine
    {
        public int Cycle { get; private set; }
        public int X { get; private set; }
        private Instruction _instruction { get; set; }
        public char[] _display { get; private set; }
        private int _displaySize { get; }

        public Machine()
        {
            Cycle = 1;
            X = 1;
        }

        public Machine(int displaySize) : this()
        {
            _displaySize = displaySize;
            _display = new char[displaySize];
            ClearDisplay();
        }

        public void EnterInstruction(string instruction, int value = 0)
        {
            if (instruction == "addx")
                _instruction = new Instruction(value, 2);
            else if (instruction == "noop")
                _instruction = new Instruction(value, 1);
        }

        private bool CanDrawDisplay() =>_displaySize != 0 && Cycle % _displaySize == 0;

        private void DrawDisplay()
        {
            Console.WriteLine(GetDisplay());
            ClearDisplay();
        }

        public void Tick()
        {
            // if I cared enough I'd break display responsibilities into a new class, rename "Machine" to something like "processor" and Machine would handle timing everything.
            DrawToDisplay();

            if (CanDrawDisplay())
                DrawDisplay();

            _instruction.IncrementCycle();
            if (_instruction.IsOpComplete())
            {
                X += _instruction.Value;
                _instruction = null;
            }
            Cycle++;
        }

        public bool ReadyForInstruction() => _instruction == null;

        private void DrawToDisplay()
        {
            int position = (Cycle - 1) % _displaySize;
            if (Math.Abs(X - position) <= 1)
                _display[position] = '#';
        }

        public string GetDisplay() => new string(_display);

        public void ClearDisplay() => Array.Fill(_display, '.');
    }
}
