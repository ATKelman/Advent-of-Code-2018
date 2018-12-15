using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        private const int INPUT = 084601;
        private static int[] INPUT2 = new int[]{ 0, 8, 4, 6, 0, 1 };

        static void Main(string[] args)
        {

            Console.WriteLine("Day 14 Part 1: " + PartOne());
            Console.WriteLine("Day 14 Part 2: " + PartTwo());

            Console.ReadKey();
        }

        private static string PartOne()
        {
            var recipes = new List<int>() { 3, 7 };
            var elfOne = 0;
            var elfTwo = 1;

            while ( recipes.Count() < (INPUT + 10))
            {
                var newRecipes = recipes[elfOne] + recipes[elfTwo];

                recipes.AddRange(newRecipes.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray());

                elfOne = (elfOne + recipes[elfOne] + 1) % recipes.Count();
                elfTwo = (elfTwo + recipes[elfTwo] + 1) % recipes.Count();
            }

            var result = "";
            recipes.Skip(INPUT).Take(10).ToList().ForEach(x =>
            {
                result += x;
            });

            return result;
        }

        private static int PartTwo()
        {
            var recipes = new List<int>() { 3, 7 };

            var elfOne = 0;
            var elfTwo = 1;

            var result = 0;
            var index = 0;

            var found = false;

            while (!found)
            {
                var newRecipes = recipes[elfOne] + recipes[elfTwo];
                recipes.AddRange(newRecipes.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray());

                elfOne = (elfOne + recipes[elfOne] + 1) % recipes.Count();
                elfTwo = (elfTwo + recipes[elfTwo] + 1) % recipes.Count();

                while ( result + index < recipes.Count())
                {
                    if (recipes[result + index] == INPUT2[index])
                    {
                        if (index == INPUT2.Count() - 1 )
                        {
                            found = true;
                            break;
                        }
                        index++;
                    }
                    else
                    {
                        index = 0;
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
