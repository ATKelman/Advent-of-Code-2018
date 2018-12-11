using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        private const int INPUT = 1718;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 11 Part 1 : " + PartOne());


            Console.ReadKey();
        }

        private static int PartOne()
        {
            var grid = new int[301, 301];

            for (int x = 1; x <= 300; x++ )
            {
                for (int y = 1; y <= 300; y++)
                {
                    grid[x, y] = CalculatePowerLevel(x, y);
                }
            }

            var highest = 0;
            var coord = new int[2];
            var s = 0;
            for (int size = 1; size <= 300; size++)
            {
                for (int x = 0; x < 300; x++)
                {
                    for (int y = 0; y < 300; y++)
                    {
                        if ((x + size) >= 300 || (y + size) >= 300)
                        {
                            break;
                        }

                        var score = 0;
                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                score += grid[x + i, y + j];
                            }
                        }

                        if (score > highest)
                        {
                            highest = score;
                            coord[0] = x;
                            coord[1] = y;
                            s = size;
                        }
                    }
                }
            }
            

            return 0;
        }

        private static int CalculatePowerLevel(int x, int y)
        {
            var result = x + 10;
            result = result * y;
            result = result + INPUT;
            result = result * (x + 10);
            result = (result / 100) % 10;
            result = result - 5;
            return result;
        }
    }
}
