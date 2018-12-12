using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        const string PLANTS = "#.#####.#.#.####.####.#.#...#.......##..##.#.#.#.###..#.....#.####..#.#######.#....####.#....##....#";
        static void Main(string[] args)
        {
            

            Console.WriteLine("Day 12 Part 1 : " + PartOne());

            Console.ReadKey();
        }

        private static int PartOne()
        {
            //var plants = "...." + "#.#####.#.#.####.####.#.#...#.......##..##.#.#.#.###..#.....#.####..#.#######.#....####.#....##....#";
            var plants = "...#..#.#..##......###...###...........";

            var rules = System.IO.File.ReadAllLines("input.txt").Select(x =>
            {
                var split = x.Split(new[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                return new
                {
                    Rule = split[0],
                    Plant = split[1]
                };
            }).ToList();

            var sum = 0;
            for (int gen = 0; gen < 20; gen++)
            {
                var newPlants = "";

                var firstHash = plants.IndexOf('#');
                for (int start = 0; start < 4 - firstHash; start++)
                {
                    newPlants.Insert(0, ".");
                }

                newPlants += plants;

                var lastHash = newPlants.LastIndexOf('#');
                for (int end = 0; end < 5 - (newPlants.Length - lastHash); end++)
                {
                    newPlants += ".";
                }

                var nextPlants = "";
                for (int i = 0; i < newPlants.Length - 5; i++)
                {
                    var currentPlantCombo = newPlants.ToString().Substring(i, 5);
                    var ruleFound = false;
                    rules.ForEach(x =>
                    {
                        if (x.Rule == currentPlantCombo)
                        {
                            nextPlants += x.Plant;
                            ruleFound = true;
                        }
                    });
                    if (!ruleFound)
                    {
                        nextPlants += ".";
                    }
                }

                sum += nextPlants.ToCharArray().Where(x => x == '#').Count();
                plants = nextPlants;
            }

            return sum;
        }
    }
}
