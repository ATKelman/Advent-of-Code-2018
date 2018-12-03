using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");

            Console.WriteLine("Day 3 Part 1: " + PartOne(input));
            Console.WriteLine("Day 3 Part 2: " + PartTwo(input));

            Console.ReadKey();
        }

        private static int[,] GenerateGrid(string[] input)
        {
            var grid = new int[1000, 1000];

            foreach (var fabric in input)
            {
                var info = fabric.Split(new[] { ' ', '@', ',', ':', 'x', '#' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
                var marginX = info[1];
                var marginY = info[2];
                var lengthX = info[3];
                var lengthY = info[4];

                for (int x = marginX; x < (lengthX + marginX); x++)
                {
                    for (int y = marginY; y < (lengthY + marginY); y++)
                    {
                        grid[x, y]++;
                    }
                }
            }
            return grid;
        }

        private static int PartOne(string[] input)
        {
            var grid = GenerateGrid(input);

            var overlap = 0;
            foreach (var tile in grid)
            {
                if (tile > 1)
                    overlap++;
            }

            return overlap;
        }

        // 1124
        private static int PartTwo(string[] input)
        {
            var grid = GenerateGrid(input);
            var reply = 0;

            foreach (var fabric in input)
            {
                var info = fabric.Split(new[] { ' ', '@', ',', ':', 'x', '#' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
                var marginX = info[1];
                var marginY = info[2];
                var lengthX = info[3];
                var lengthY = info[4];

                var overlaps = false;
                for (int x = marginX; x < (lengthX + marginX); x++)
                {
                    for (int y = marginY; y < (lengthY + marginY); y++)
                    {
                        if (grid[x, y] > 1)
                        {
                            overlaps = true;
                        }                           
                    }
                }
                if (!overlaps)
                {
                    reply = info[0];
                }
            }

            return reply;
        }
    }
}
