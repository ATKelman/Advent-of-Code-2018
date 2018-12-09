using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    class Program
    {
        const int PLAYERS = 10;
        const int MARBLES = 1618 * 100;

        static void Main(string[] args)
        {
            Console.WriteLine("Day 9 Part 1: " + PartOne());

            Console.ReadKey();
        }

        // Last marble = 71646 
        private static long PartOne()
        {
            var scores = new long[PLAYERS];
            var circle = new LinkedList<long>();
            var current = circle.AddFirst(0);

            var answerFound = false;
            for (int i = 1; i < MARBLES; i++)
            {
                if (answerFound)
                {
                    break;
                }
                if (i % 23 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        current = current.Previous ?? circle.Last;
                        scores[i % PLAYERS] += (i + current.Value);
                        if ((i + current.Value) == (MARBLES / 100))
                        {
                            answerFound = true;
                            break;
                        }


                        var tmp = current;
                        current = current.Previous ?? circle.Last;
                        circle.Remove(tmp);
                    }
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
