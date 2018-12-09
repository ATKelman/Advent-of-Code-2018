using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 8 Part 1: " + PartOne());
            Console.WriteLine("Day 8 Part 2: " + PartTwo());

            Console.ReadKey();
        }

        private static int PartOne()
        {
            var input = System.IO.File.ReadAllText("input.txt").Split(' ').Select(x => int.Parse(x)).ToList();
            var root = HandleNode(input);

            return root.GetSum();
        }

        private static int PartTwo()
        {
            var input = System.IO.File.ReadAllText("input.txt").Split(' ').Select(x => int.Parse(x)).ToList();
            var root = HandleNode(input);

            return root.GetValueOfNode();
        }

        private static Node HandleNode(List<int> input)
        {
            var current = new Node();

            current.Children = new Node[input[0]];
            input.RemoveAt(0);

            current.Entries = new int[input[0]];
            input.RemoveAt(0);

            for ( int i = 0; i < current.Children.Count(); i++)
            {
                current.Children[i] = HandleNode(input);
            }

            for ( int i = 0; i < current.Entries.Count(); i++)
            {
                current.Entries[i] = input[0];
                input.RemoveAt(0);
            }

            return current;
        }

        public class Node
        {
            public int[] Entries { get; set; }
            public Node[] Children { get; set; }

            public int GetSum() => Entries.Sum() + Children.Select(x => x.GetSum()).Sum();
            public int GetValueOfNode() => Children.Any() ? Entries.Select(x => x > Children.Length ? 0 : Children[x - 1].GetValueOfNode()).Sum() : GetSum();

        }
    }
}
