using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 6 Part 1: " + PartOne());

            Console.ReadKey();
        }

        private static int PartOne()
        {
            var count = 0;
            var input = System.IO.File.ReadAllLines("input.txt").Select(x =>
            {
                var info = x.Split(',');
                count++;

                return new
                {
                    X = Convert.ToInt32(info[0]),
                    Y = Convert.ToInt32(info[1]), 
                    LocationName = count
                };
            }).ToList();

            var arraySizeX = input.Select(x => x.X).Max() + 1;
            var arraySizeY = input.Select(y => y.Y).Max() + 1;

            var grid = new int[arraySizeX, arraySizeY];

			var countHasLess = 0;
            for (int x = 0; x < arraySizeX; x++)
            {
                for (int y = 0; y < arraySizeY; y++)
                {
                    var shortestDistance = int.MaxValue;
                    var locationValue = 0;
					var maxDistanceIsLess = 0;
                    foreach (var location in input)
                    {
						var distance = Math.Abs(x - location.X) + Math.Abs(y - location.Y);

                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            locationValue = location.LocationName;
                        }
                        else if( distance == shortestDistance)
                        {
                            locationValue = 0;
                        }
						maxDistanceIsLess += distance;
                    }

					if (maxDistanceIsLess < 10000)
					{
						countHasLess++;
					}

                    grid[x, y] = locationValue;
                }            
            }

            var occurance = new Dictionary<int, int>();
            var infinite = new List<int>();
            for (int x = 0; x < arraySizeX; x++)
            {
                for (int y = 0; y < arraySizeY; y++)
                {
                    var location = grid[x, y];

                    if (x == 0 || y == 0 || y == arraySizeY  || x == arraySizeX)
                    {
                        if (!infinite.Contains(location))
                            infinite.Add(location);
                    }
                    if (!occurance.ContainsKey(location))
                        occurance.Add(location, 0);
                    occurance[location]++;
                }
            }

			var order = occurance.Where(x=> !infinite.Contains(x.Key)).OrderByDescending(x => x.Value);

			var test = countHasLess;

			return order.First().Value;
        }
    }
}
