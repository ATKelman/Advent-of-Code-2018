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
            //Console.WriteLine("Day 7 Part 1: " + PartOne());
            //Console.WriteLine("Day 7 Part 2: " + PartTwo());
            Day7Part2.Solve();


            Console.ReadKey();
        }

        public class Step
        {
            public char Name { get; set; }

            public int Time { get; set; }
            public List<char> Before { get; set; } = new List<char>();
        }

        public static class Day7Part2
        {
            private static string[] InputLines => System.IO.File.ReadAllLines("input.txt");



            public static void Solve()
            {
                List<Step> steps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select((c, i) => new Step { Name = c, Time = 61 + i }).ToList();
                List<Step> next;
                char before = '\0', after = '\0';
                int workers = 5;
                int sec = 0;
                foreach (string line in InputLines)
                {
                    MatchCollection matches = Regex.Matches(line, @"\b[A-Z]\b");
                    before = char.Parse(matches[0].Value);
                    after = char.Parse(matches[1].Value);

                    steps.First(s => s.Name == after).Before.Add(before);
                }

                while (steps.Count > 0)
                {
                    next = steps.Where(s => s.Before.Count == 0).OrderBy(s => s.Time).Take(workers).ToList();
                    next.ForEach(s => s.Time--);
                    steps.ForEach(s => s.Before.RemoveAll(n => steps.Where(p => p.Time == 0).Select(p => p.Name).Contains(n)));
                    steps.RemoveAll(s => s.Time == 0);
                    sec++;
                }

                Console.WriteLine($"It will take { sec }s to complete all the steps.");
            }
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

            var time = 0;
            var result = "";
            var workers = new Worker[5] { new Worker(), new Worker(), new Worker(), new Worker(), new Worker() };

            while (result.Length < words.Keys.Count())
            {
                observedNodes = observedNodes.OrderBy(x => x.Name).ToList();
                foreach (var worker in workers)
                {
                    if (!worker.IsWorking && observedNodes.Any())
                    {
                        worker.CurrentNode = observedNodes[0] ?? null;
                        observedNodes.Remove(worker.CurrentNode);

                        foreach (var child in worker.CurrentNode.Children)
                        {
                            child.Parents.Remove(worker.CurrentNode);
                            if (child.Parents.Count == 0)
                            {
                                observedNodes.Add(child);
                            }
                        }

                        worker.CurrentWorkTime = 0;
                        worker.CompletionTime = (worker.CurrentNode.Name[0] - 'A') + 61;

                        worker.IsWorking = true;
                    }

                    if (worker.IsWorking)
                    {
                        worker.CurrentWorkTime += 1;
                        if (worker.CurrentWorkTime == worker.CompletionTime)
                        {
                            result += worker.CurrentNode.Name;
                          
                            worker.IsWorking = false;
                        }
                    }
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

        public class Worker
        {
            public Node CurrentNode { get; set; }
            public bool IsWorking { get; set; }
            public int CurrentWorkTime { get; set; }
            public int CompletionTime { get; set; }

            public Worker()
            {
                CurrentNode = null;
                IsWorking = false;
            }
        }
    }
}
