using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Answer: " + Solve());

            Console.ReadKey();
        }

        private static int Solve()
        {
            var input = System.IO.File.ReadAllLines("input.txt").OrderBy(x => DateTime.Parse(x.Substring(1, 16)))
                .Select(x =>
                {
                    var information = x.Split(new[] { '[', ']', ' ', '#', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    return new
                    {
                        info = information,
                        date = DateTime.Parse(x.Substring(1, 16))
                    };
                });

            var entries = new Dictionary<int, Guard>();
            var currentId = -1;
            foreach (var line in input)
            {
                switch (line.info[3])
                {
                    case "Guard":
                        currentId = Convert.ToInt32(line.info[4]);
                        if (!entries.ContainsKey(currentId))
                            entries.Add(currentId, new Guard());

                        break;
                    case "falls":
                        entries[currentId].FellAsleep = line.date;
                        break;
                    case "wakes":
                        entries[currentId].WokeUp = line.date;
                        entries[currentId].AddSleep();
                        break;
                    default:
                        throw new ArgumentException("Invalid message");
                }
            }

            // ------------- Part 1 Solution ------------------
            // var sleptLongestID = entries.OrderByDescending(x => x.Value.TimeSlept).First();
            // return sleptLongestID.Key * Array.IndexOf(sleptLongestID.Value.Sleep, sleptLongestID.Value.Sleep.OrderByDescending(x => x).First());

            var sleptLongestSameTimeId = entries.OrderByDescending(x => x.Value.Sleep.Max()).First();
            return sleptLongestSameTimeId.Key * Array.IndexOf(sleptLongestSameTimeId.Value.Sleep, sleptLongestSameTimeId.Value.Sleep.OrderByDescending(x => x).First());
        }

        private class Guard
        {
            public DateTime FellAsleep { get; set; }
            public DateTime WokeUp { get; set; }
            public int[] Sleep { get; set; }
            public int TimeSlept { get; set; }

            public Guard()
            {
                Sleep = new int[60];
            }

            public void AddSleep()
            {
                var timeSlept = (WokeUp - FellAsleep).Minutes;
                for (int i = FellAsleep.Minute; i < (FellAsleep.Minute + timeSlept); i++)
                {
                    Sleep[(i%60)]++;
                }
                TimeSlept += timeSlept;
            }
        }
    }
}
