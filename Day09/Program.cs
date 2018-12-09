using System;
using System.Collections.Generic;
using System.Linq;

namespace Day09
{
    class Program
    {
        const int PLAYERS = 412;
        const int MARBLES = 71646;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 9 Part 1: " + CalculateScore(PLAYERS, MARBLES));
            Console.WriteLine("Day 9 Part 2: " + CalculateScore(PLAYERS, MARBLES * 100));

            Console.ReadKey();
        }

        private static long CalculateScore(int players, int marbles)
        {
            var scores = new long[players];
            var circle = new LinkedList<int>();
           
            var current = circle.AddFirst(0);

            for (int i = 1; i < marbles; i++)
            {
                if (i % 23 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? circle.Last;
                    }

                    scores[i % PLAYERS] += (i + current.Value);

                    var tmp = current;
                    current = current.Next ?? circle.First;
                    circle.Remove(tmp);
                }
                else
                {
                    current = circle.AddAfter(current.Next ?? circle.First, i);
                }
            }

            return scores.Max();
        }
    }
}
