using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7 Part 1: " + PartOne());
            Console.WriteLine("Day 7 Part 2: " + PartTwo());

            Console.ReadKey();
        }

        private static string PartOne()
		{
            var words = GetWords();
		
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

        private static int PartTwo()
        {
            var words = GetWords();

            var observedNodes = new List<Node>();
            observedNodes.AddRange(words.Values.Where(x => x.Parents.Count() == 0));
            var timers = new (char Letter, int Timer)[26];

            for (int i = 0; i < timers.Length; i++)
            {
                timers[i].Letter = (char)(65 + i);
                timers[i].Timer = 61 + i;
            }

            var workers = new char?[5] { null, null, null, null, null };
            var time = 0;
            while (timers.Any(x => x.Timer > 0))
            {
                observedNodes = observedNodes.OrderBy(x => x.Name).ToList();

                for (int i = 0; i < workers.Length; i++)
                {
                    if (workers[i] == null && observedNodes.Any())
                    {
                        workers[i] = observedNodes[0].Name[0];
                        observedNodes.RemoveAt(0);
                    }

                    if (workers[i] != null)
                    {
                        var timer = timers.Where(x => x.Letter.Equals(workers[i])).Single();
                        timers[timer.Letter - 65] = (timer.Letter, timer.Timer - 1);
                    }         
                }

                if (timers.Any(x => x.Timer == 0))
                {
                    timers.Where(x => x.Timer == 0).ToList().ForEach(x =>
                    {
                        for (int i = 0; i < workers.Length; i++)
                        {
                            if (workers[i].HasValue)
                            {
                                if (workers[i].Value.Equals(x.Letter))
                                {
                                    words.Where(y => y.Value.Name == workers[i].Value.ToString()).Single().Value.Children.ForEach(c =>
                                    {
                                        c.Parents.Remove(words.Where(y => y.Value.Name == workers[i].Value.ToString()).Single().Value);
                                        if (c.Parents.Count() == 0) { observedNodes.Add(c); }
                                    });

                                    workers[i] = null;
                                }
                            }
                        }
                       
                        timers[x.Letter - 65] = (x.Letter, x.Timer - 1);
                    });
                }

                time++;
            }

            return time;
        }

        private static Dictionary<string, Node> GetWords()
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
            return words;
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
