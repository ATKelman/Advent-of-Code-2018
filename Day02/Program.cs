using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt").ToList();

            Console.WriteLine("Day 2 Part 1: " + PartOne(input));
            Console.WriteLine("Day 2 Part 2: " + PartTwo(input));

            Console.ReadKey();
        }

        private static int PartOne(List<string> input)
        {
            var countTwo = 0;
            var countThree = 0;

            foreach (var line in input)
            {
                countTwo += (line.ToCharArray().GroupBy(x => x).Select(y => new { Count = y.Count() }).Where(z => z.Count == 2).Count() > 0) ? 1 : 0;
                countThree +=  (line.ToCharArray().GroupBy(x => x).Select(y => new { Count = y.Count() }).Where(z => z.Count == 3).Count() > 0) ? 1 : 0;
            }

            return countTwo * countThree;
        }

        private static string PartTwo(List<string> input)
        {
            var diff = int.MaxValue;
            var wordOne = "";
            var wordTwo = "";

            foreach (var line in input)
            {
                foreach (var lineTwo in input)
                {
                    if (line == lineTwo)
                        continue;

                    var diffCount = line.Where((x, y) => x != lineTwo[y]).Count();

                    if (diffCount < diff)
                    {
                        diff = diffCount;
                        wordOne = line;
                        wordTwo = lineTwo;
                    }
                }
            }

            var result = wordOne.Where((x, y) => x == wordTwo[y]);
            return String.Join("", result.Select(x => x.ToString()).ToArray());
        }
    }
}
