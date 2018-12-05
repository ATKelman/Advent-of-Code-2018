using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
			var input = System.IO.File.ReadAllText("input.txt");

			Console.WriteLine("Day 5 Part 1:" + PartOne(input));
            Console.WriteLine("Day 5 Part 2:" + PartTwo(input));

            Console.ReadKey();
		}

        private static int PartOne(string input)
        {
			return CalculatePolymerLength(input);
        }

		private static int CalculatePolymerLength(string polymer)
		{
			var input = polymer;
			var foundCaseToRemove = true;

			while (foundCaseToRemove)
			{
				foundCaseToRemove = false;
				for (var i = 0; i < input.Length - 1; i++)
				{
					if (string.Equals(input[i].ToString(), input[i+1].ToString(), StringComparison.OrdinalIgnoreCase))
					{
						if (input[i] != input[i+1])
						{
							foundCaseToRemove = true;
							input = input.Remove(i, 2);
							break;
						}
					}
				}
			}
			return input.Length;
		}

		private static int PartTwo(string input)
		{
			var shortest = int.MaxValue;

			var usedCharacters = input.Select(x => char.ToUpper(x)).Distinct().ToArray();

			foreach (var character in usedCharacters)
			{
				var removedCharacterInput = input.Replace(character.ToString().ToUpper(), "").Replace(character.ToString().ToLower(), "").Trim();
				var length = CalculatePolymerLength(removedCharacterInput);

				if (length < shortest)
				{
					shortest = length;
				}
			}

			return shortest;
		}
    }
}
