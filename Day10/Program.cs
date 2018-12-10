using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 10 Part 1 : " + PartOne());

            Console.ReadKey();
        }

        private static int PartOne()
        {
            var input = System.IO.File.ReadAllLines("input.txt").Select(x =>
            {
                var info = x.Split(new char[] { '=', '<', ',', '>' }, StringSplitOptions.RemoveEmptyEntries);
                return new Position
                {
                    X = Convert.ToInt32(info[1]),
                    Y = Convert.ToInt32(info[2]),
                    VelX = Convert.ToInt32(info[4]),
                    VelY = Convert.ToInt32(info[5])
                };
            }).ToArray();

            var minX = input.Select(x => x.X).Min();
            var maxX = input.Select(x => x.X).Max();

            var minY = input.Select(x => x.Y).Min();
            var maxY = input.Select(x => x.Y).Max();
            var loops = 0;
            while (true)
            {
                loops++;
                var temp = input.Select(x => x).ToList();
                for (int j = 0; j < input.Count(); j++)
                {
                    input[j].PrevX = input[j].X;
                    input[j].PrevY = input[j].Y;
                    input[j].X = input[j].X + input[j].VelX;
                    input[j].Y = input[j].Y + input[j].VelY;
                }

                var newMinX = input.Select(x => x.X).Min();
                var newMaxX = input.Select(x => x.X).Max();

                var newMinY = input.Select(x => x.Y).Min();
                var newMaxY = input.Select(x => x.Y).Max();

                if ( (newMaxX - newMinX) > (maxX - minX) || (newMaxY - newMinY) > (maxY - minY))
                {
                    for (int x = minY; x <= maxY; x++)
                    {
                        for (int y = minX; y <= maxX; y++)
                        {
                            Console.Write(input.Any(z => z.PrevX == y && z.PrevY == x) ? '#' : '.');
                        }
                        Console.WriteLine();
                    }
                    Console.Read();
                }

                minX = newMinX;
                maxX = newMaxX;
                minY = newMinY;
                maxY = newMaxY;
            }

            return 0;
        }

        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int PrevX { get; set; }
            public int PrevY { get; set; }
            public int VelX { get; set; }
            public int VelY { get; set; }
        }
    }
}
