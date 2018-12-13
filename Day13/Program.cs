using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();

            Console.ReadKey();
        }


        private static void PartOne()
        {
            var input = System.IO.File.ReadAllLines("input.txt");

            var gridX = input[0].Length;
            var gridY = input.Count();

            var grid = new char[gridX, gridY];

            // find carts
            var cart = new char[] { '<', '>', 'v', '^' };
            var carts = new List<Cart>();

            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    grid[x, y] = input[x][y];

                    if (cart.Contains(grid[x, y]))
                    {
                        var xvel = 0;
                        var yvel = 0;
                        var tile = ' ';
                        switch (grid[x, y].ToString())
                        {
                            case "<":
                                xvel = -1;
                                tile = '-';
                                break;
                            case ">":
                                xvel = 1;
                                tile = '-';
                                break;
                            case "^":
                                yvel = 1;
                                tile = '|';
                                break;
                            case "v":
                                yvel = -1;
                                tile = '|';
                                break;
                            default:
                                break;
                        }

                        carts.Add(new Cart()
                        {
                            CurrentDirection = grid[x, y].ToString(),
                            XVel = xvel,
                            YVel = yvel,
                            X = x,
                            Y = y,
                            CurrentIntersectionStep = 0,
                            OldTile = tile
                        });
                    }
                }
            }

            var hasCrashed = false;
            var crashPosition = new int[2];
            while (!hasCrashed)
            {
                foreach (var c in carts)
                {
                    var nextPos = new int[2];
                    nextPos[0] = c.XVel + c.X;
                    nextPos[1] = c.YVel + c.Y;

                    grid[c.X, c.Y] = c.OldTile;
                    var nextTile = grid[nextPos[0], nextPos[1]].ToString();
                    c.OldTile = nextTile[0];
                    c.X = nextPos[0];
                    c.Y = nextPos[1];

                    switch (nextTile)
                    {
                        case "-":
                            break;
                        case "|":
                            break;
                        case "\\":
                            var tmp = -1 * c.XVel;
                            c.XVel = -1 * c.YVel;
                            c.YVel = tmp;
                            break;
                        case "/":
                            var tmp2 = c.XVel;
                            c.XVel = c.YVel;
                            c.YVel = tmp2;
                            break;                       
                        case "+":
                            if (c.CurrentIntersectionStep == 0)
                            {
                                if (c.XVel == 1)
                                {                                  
                                    c.XVel = 0;
                                    c.YVel = 1;
                                }
                                else if (c.XVel == -1)
                                {
                                    c.XVel = 0;
                                    c.YVel = -1;
                                }
                                else if (c.YVel == 1)
                                {
                                    c.XVel = -1;
                                    c.YVel = 0;
                                }
                                else if (c.YVel == -1)
                                {
                                    c.XVel = 1;
                                    c.YVel = 0;
                                }
                            }
                            else if (c.CurrentIntersectionStep == 1)
                            {
                                // straight
                            }
                            else if (c.CurrentIntersectionStep == 2)
                            {
                                if (c.XVel == 1)
                                {
                                    c.XVel = 0;
                                    c.YVel = -1;
                                }
                                else if (c.XVel == -1)
                                {
                                    c.XVel = 0;
                                    c.YVel = 1;
                                }
                                else if (c.YVel == 1)
                                {
                                    c.XVel = 1;
                                    c.YVel = 0;
                                }
                                else if (c.YVel == -1)
                                {
                                    c.XVel = -1;
                                    c.YVel = 0;
                                }
                            }

                            c.CurrentIntersectionStep = (c.CurrentIntersectionStep + 1) % 3;
                            break;
                        case "<":
                        case ">":
                        case "^":
                        case "v":
                            hasCrashed = true;
                            crashPosition = nextPos;
                            break;
                    }

                }
            }

            Console.WriteLine("Day 13 Part 1: " + crashPosition[0] + " " + crashPosition[1]);
        }

        public class Cart
        {
            public string CurrentDirection { get; set; }
            public int XVel { get; set; }
            public int YVel { get; set; }

            // 0 = Left, 1 = Straight, 2 = Right
            public int CurrentIntersectionStep { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public char OldTile { get; set; }
        }
    }
}
