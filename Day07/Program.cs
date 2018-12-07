using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7 Part 1; " + PartOne());

            Console.ReadKey();
        }

        private static string PartOne()
        {
            var input = System.IO.File.ReadAllLines("input.txt").Select(x =>
            {
                var splitLine = x.Split(' ');
                return new
                {
                    Start = splitLine[1],
                    End = splitLine[7]
                };
            });

            var words = new Dictionary<string, Node>();

            foreach (var line in input)
            {
                if (!words.ContainsKey(line.Start))
                {
                    words.Add(line.Start, new Node()
                    {
                        Name = line.Start
                    });
                }

                if (!words.ContainsKey(line.End))
                {
                    words.Add(line.End, new Node()
                    {
                        Name = line.End
                    });
                }

                if (!)

            }

            //var order = words.OrderBy(x => x.Key).OrderByDescending(x => x.Value.Count());

            //var result = "";
            //foreach (var word in order)
            //{

            //}

            return result;
        }

        public class Node
        {
            public string Name { get; set; }
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }
        }
    }
}
