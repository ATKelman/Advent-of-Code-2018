﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7 Part 1: " + PartOne());

            Console.ReadKey();
        }

		private static string PartOne()
		{
            var words = new Dictionary<string, Node>();
            System.IO.File.ReadAllLines("input.txt").Select(x =>
			{
				var splitLine = x.Split(' ');
				return new
				{
					Start = splitLine[1],
					End = splitLine[7]
				};
			}).ToList().ForEach(x =>
            {
                if (!words.ContainsKey(x.Start)) { words.Add(x.Start, new Node() { Name = x.Start }); }
                if (!words.ContainsKey(x.End)) { words.Add(x.End, new Node() { Name = x.End }); }

                words[x.Start].Children.Add(words[x.End]);
                words[x.End].Parents.Add(words[x.Start]);
            });
		
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