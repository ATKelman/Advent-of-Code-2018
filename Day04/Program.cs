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
            Console.WriteLine("Part One : " + PartOne());

            Console.ReadKey();
        }

        private static int PartOne()
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var log = new Dictionary<DateTime, Entry>();

            var entries = new List<Entry>();

            foreach (var line in input)
            {
                var info = line.Split(new[] { '[', ']', ' ', '#', ':' }, StringSplitOptions.RemoveEmptyEntries);

                var date = DateTime.Parse(info[0]);
                var entry = new Entry();

                if (log.ContainsKey(date.Date))
                {
                    entry = log[date.Date];
                }
                else
                {
                    entry.Date = date;
                    log.Add(date, entry);
                }
                
                switch (info[3])
                {
                    case "falls":
                        entry.MinuteFallAsleep = Convert.ToInt32(info[2]);
                        break;
                    case "wakes":
                        entry.MinuteWakeUp = Convert.ToInt32(info[2]);
                        break;
                    case "Guard":
                        entry.GuardId = Convert.ToInt32(info[4]);
                        break;
                    default:

                        break;
                }
            }

            var highestOccuringId = log.OrderBy(x => x.Value.GuardId);
            


            return 0;
        }

        private class Entry
        {
            public DateTime Date { get; set; }
            public int GuardId { get; set; }
            public int MinuteFallAsleep { get; set; }
            public int MinuteWakeUp { get; set; }
        }
    }
}
