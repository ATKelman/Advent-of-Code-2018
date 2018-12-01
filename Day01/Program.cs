using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Day 1 Part 1 result : " + PartOne());
            Console.WriteLine("Day 1 Part 2 result : " + PartTwo());

            Console.ReadKey();
        }

        private static int PartOne()
        {
            return File.ReadAllLines("input.txt").ToList().Select(x => Convert.ToInt32(x)).Sum();
        }

        private static int PartTwo()
        {
            var inputs = File.ReadAllLines("input.txt").Select(x => Convert.ToInt32(x));
            var seen = new HashSet<int>();

            var result = 0;
            while (true)
            {
                foreach (var input in inputs)
                {
                    result += input;
                    if (seen.Contains(result))
                        return result;
                    else
                        seen.Add(result);
                }
            }
        }
    }
}
