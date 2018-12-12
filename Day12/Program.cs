using System;
using System.Collections.Generic;
using System.IO;
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
            var plants = "#.#####.#.#.####.####.#.#...#.......##..##.#.#.#.###..#.....#.####..#.#######.#....####.#....##....#";

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
            var negativeSteps = 0;

            var sumPrev = 0;
            var sumCurrent = 0;
            var sumNext = 0;

            for (int gen = 1; gen <= 5000000000; gen++)
            {
                var newPlants = "";

                var firstHash = plants.IndexOf('#');
                for (int start = 0; start < 4 - firstHash; start++)
                {
                    newPlants += ".";
                    negativeSteps--;
                }

                newPlants += plants;

                var lastHash = newPlants.LastIndexOf('#');
                newPlants = newPlants.PadRight(lastHash + 5, '.');

                var nextPlants = "..";
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

                // Difference Calculation for part 2
                sumPrev = sumCurrent;
                sumCurrent = sumNext;
                sumNext = 0;
                for (int i = 0; i < nextPlants.Length; i++)
                {
                    if (nextPlants[i] == '#')
                    {
                        sumNext += i + negativeSteps;
                    }
                }

                if ((sumCurrent - sumPrev) == (sumNext - sumCurrent))
                {
                    PartTwo(gen, sumNext, (sumNext - sumCurrent));
                }

                plants = nextPlants;
            }
            
            for (int i = 0; i < plants.Length ; i++)
            {
                if (plants[i] == '#')
                {
                    sum += i + negativeSteps;
                }
            }

            return sum;
        }

        private static void PartTwo(int generationsPassed, int currentSum, int difference)
        {
            // Math done manually
            //var i = 50000000000;
            //var result = (423570 + (i - 7388) * 57);
            //var test = result;

            var i = 50000000000;
            var result = (currentSum + (i - generationsPassed) * difference);
            var test = result;

            Console.WriteLine("Day 12 Part 2 : " + result);
        }
    }
}
