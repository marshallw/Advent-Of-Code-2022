using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day22
{
    internal class Day22 : IDay<long, long>
    {
        public string[] _input { get; }

        public Day22()
        {
            _input = File.ReadAllLines(@"Day22\input.txt").ToArray();
        }

        public char[,] LoadMap(string[] input)
        {
            var list = input.ToList();
            list.RemoveAt(list.Count - 1);
            var length = list.OrderByDescending(x => x.Length).First().Length;
            var height = list.Count - 1;
            char[,] map = new char[length, height];
            var queue = new Queue<string>(input);
            for (int y2 = 0; y2 < height; y2++)
                for (int x = 0; x < length; x++)
                    map[x, y2] = ' ';

            string line = "";
            int y = 0;
            do
            {
                line = queue.Dequeue();
                if (line.Length > 0)
                {
                    for(int i = 0; i < line.Length; i++)
                    {
                        map[i, y] = line[i];
                    }
                }
                y++;
            } while (line.Trim().Length != 0);
            return map;
        }

        public int GetNextDistance(Queue<char> instructions)
        {
            string result = "";
            while(instructions.Any() && Int32.TryParse(new string(instructions.Peek(), 1), out int num))
            {
                result += instructions.Dequeue();
            }
            return Int32.Parse(result);
        }

        public int RotateCharacter(int startDirection, Queue<char> queue)
        {
            var rotate = queue.Dequeue();
            if (rotate == 'R')
            {
                startDirection = (startDirection + 1) % 4;
            }
            else if (rotate == 'L')
            {
                startDirection = startDirection - 1;
                if (startDirection == -1)
                    startDirection = 3;
            }
            return startDirection;
        }

        public long Part1()
        {
            var map = LoadMap(_input);
            var instructions = new Queue<char>(_input.Last());
            int facing = 0;
            (int x, int y) coordinates = (_input[0].Select((x, y) => (x, y)).Where(x => x.x == '.').First().y, 0);

            do
            {
                int distance = 0;
                if (instructions.Peek() == 'R' || instructions.Peek() == 'L')
                {
                    facing = (RotateCharacter(facing, instructions));
                }
                else
                {
                    distance = GetNextDistance(instructions);
                    coordinates = MoveCharacter(coordinates, facing, distance, map);
                }
            }while(instructions.Count > 0);

            return 1000*(coordinates.y + 1) + 4 * (coordinates.x + 1) + facing;
        }

        public (int x, int y) MoveCharacter((int x, int y) character, int facing, int distance, char[,] map)
        {
            for(int i = 0; i < distance; i++)
            {
                if (facing == 0)
                {
                    if (character.x + 1 >= map.GetLength(0) || map[character.x + 1, character.y] != '#')
                        character.x++;
                }
                else if (facing == 1)
                {
                    if (character.y + 1 >= map.GetLength(1) || map[character.x, character.y + 1] != '#')
                        character.y++;
                }
                else if (facing == 2)
                {
                    if (character.x - 1 < 0 || map[character.x - 1, character.y] != '#')
                        character.x--;
                }
                else if (facing == 3)
                {
                    if (character.y - 1 < 0 || map[character.x, character.y - 1] != '#')
                        character.y--;
                }

                if (character.x < 0 || character.y < 0 || character.x >= map.GetLength(0) || character.y >= map.GetLength(1) || map[character.x, character.y] == ' ')
                {
                    if (!CheckIfWrapAroundIsAWall(character, facing, map))
                    {
                        character = WrapCharacterAroundTheMap(character, facing, map);
                    }
                    else
                    {
                        switch (facing)
                        {
                            case 0:
                                character.x--;
                                break;
                            case 1:
                                character.y--;
                                break;
                            case 2:
                                character.x++;
                                break;
                            case 3:
                                character.y++;
                                break;
                        }
                    }
                }
            }
            return character;
        }

        private (int x, int y) WrapCharacterAroundTheMap((int x, int y) character, int facing, char[,] map)
        {
            do
            {
                switch (facing)
                {
                    case 0:
                        character.x--;
                        if (character.x < 0 || map[character.x, character.y] == ' ')
                        {
                            character.x++;
                            return character;
                        }
                        break;
                    case 1:
                        character.y--;
                        if (character.y < 0 || map[character.x, character.y] == ' ')
                        {
                            character.y++;
                            return character;
                        }
                        break;
                    case 2:
                        character.x++;
                        if (character.x >= map.GetLength(0) || map[character.x, character.y] == ' ')
                        {
                            character.x--;
                            return character;
                        }
                        break;
                    case 3:
                        character.y++;
                        if (character.y >= map.GetLength(1) || map[character.x, character.y] == ' ')
                        {
                            character.y--;
                            return character;
                        }
                        break;
                }
            } while (true);
            return character;
        }

        public bool CheckIfWrapAroundIsAWall((int x, int y) character, int facing, char[,] map)
        {
            do
            {
                switch (facing)
                {
                    case 0:
                        character.x--;
                        if (character.x < 0 || map[character.x, character.y] == ' ')
                        {
                            character.x++;
                            return map[character.x, character.y] == '#';
                        }
                        break;
                    case 1:
                        character.y--;
                        if (character.y < 0 || map[character.x, character.y] == ' ')
                        {
                            character.y++;
                            return map[character.x, character.y] == '#';
                        }
                        break;
                    case 2:
                        character.x++;
                        if (character.x >= map.GetLength(0) || map[character.x, character.y] == ' ')
                        {
                            character.x--;
                            return map[character.x, character.y] == '#';
                        }
                        break;
                    case 3:
                        character.y++;
                        if (character.y >= map.GetLength(0) || map[character.x, character.y] == ' ')
                        {
                            character.y--;
                            return map[character.x, character.y] == '#';
                        }
                        break;
                }
            } while (true);
            return map[character.x, character.y] == '#';
        }

        //public ((int x, int y), int) HandleEdgeOfCube((int x, int y) character, int facing, char[,] map)
        //{

        //}

        //public bool CheckIfEdgeOfCubeIsWall((int x, int y) character, int facing, char[,] map)
        //{
        //    (int x, int y) workingCoordinates = character;
        //    switch(facing)
        //    {
        //        case 0:
        //            do
        //            {
        //                workingCoordinates.y--;
        //                if (workingCoordinates.y >= 0 && map[workingCoordinates.x, workingCoordinates.y] != ' ')
        //                {

        //                }
        //            }while(character)
        //            break;
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //        case 3:
        //            break;
        //    }
        //}

        public long Part2()
        {
            var map = LoadMap(_input);
            var instructions = new Queue<char>(_input.Last());
            int facing = 0;
            (int x, int y) coordinates = (_input[0].Select((x, y) => (x, y)).Where(x => x.x == '.').First().y, 0);

            do
            {
                int distance = 0;
                if (instructions.Peek() == 'R' || instructions.Peek() == 'L')
                {
                    facing = (RotateCharacter(facing, instructions));
                }
                else
                {
                    distance = GetNextDistance(instructions);
                    coordinates = MoveCharacter(coordinates, facing, distance, map);
                }
            } while (instructions.Count > 0);

            return 1000 * (coordinates.y + 1) + 4 * (coordinates.x + 1) + facing;
        }
    }
}
