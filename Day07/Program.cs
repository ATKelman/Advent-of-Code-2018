using System;
using System.Collections.Generic;
using System.Linq;

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

				words[line.Start].Children.Add(words[line.End]);
				words[line.End].Parents.Add(words[line.Start]);
			}

			
			var observedNodes = new List<Node>();
			observedNodes.AddRange(words.Values.Where(x => x.Parents.Count() == 0));

			var result = "";
			while (result.Length < words.Keys.Count())
			{
				observedNodes = observedNodes.OrderBy(x => x.Name).ToList();
				var current = observedNodes[0];
				observedNodes.Remove(current);
				result += current.Name;

				foreach (var child in current.Children)
				{
					child.Parents.Remove(current);
					if (child.Parents.Count == 0)
					{
						observedNodes.Add(child);
					}
				}
			}

			return result;
		}

		public class Node
        {
            public string Name { get; set; }
            public List<Node> Parents { get; set; }
            public List<Node> Children { get; set; }

			public Node()
			{
				Parents = new List<Node>();
				Children = new List<Node>();
			}
        }
    }
}
