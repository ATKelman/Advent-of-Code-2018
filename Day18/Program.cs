using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 18 Part 1 : " + PartOne());

            Console.ReadKey();
        }

        // open = . , trees = | , lumberyard = #
        private static int PartOne()
        {
            var input = System.IO.File.ReadAllLines("input.txt").ToArray();

            var forest = new char[50, 50];

            for (int y = 0; y < input.Count(); y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    forest[x, y] = input[y][x];
                }
            }

            for (var minutes = 0; minutes < 10; minutes++)
            {
                var newForest = new char[50, 50];
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 50; y++)
                    {
                        int countOpen = 0;
                        var countTrees = 0;
                        var countLumberyard = 0;

                        for (int checkX = -1; checkX < 2; checkX++)
                        {
                            for (int checkY = -1; checkY < 2; checkY++)
                            {
                                if ((checkX + x) < 0 || (x + checkX) >= 50 || ( y + checkY) < 0 || ( y + checkY) >= 50 || (checkX == 0 && checkY == 0))
                                {
                                    continue;
                                }

                                if (forest[(checkX + x), (y + checkY)] == '.')
                                    countOpen++;
                                else if (forest[(checkX + x), (y + checkY)] == '|')
                                    countTrees++;
                                else if (forest[(checkX + x), (y + checkY)] == '#')
                                    countLumberyard++;
                            }
                        }

                        if (forest[x, y] == '.')
                        {
                            if (countTrees >= 3)
                                newForest[x, y] = '|';
                            else
                                newForest[x, y] = '.';
                        }
                        else if (forest[x, y] == '|' )
                        {
                            if (countLumberyard >= 3)
                                newForest[x, y] = '#';
                            else
                                newForest[x, y] = '|';
                        }
                        else if (forest[x, y] == '#' )
                        {
                            if(countLumberyard >= 1 && countTrees >= 1)
                                newForest[x, y] = '#';
                            else
                                newForest[x, y] = '.';
                        }
               
                    }
                }

                forest = newForest;
            }

            var countForest = 0;
            var countyard = 0;
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    if (forest[x, y] == '|')
                    {
                        countForest++;
                    }
                    else if (forest[x, y] == '#')
                    {
                        countyard++;
                    }
                }
            }

            return countForest * countyard;
        }
    }
}
