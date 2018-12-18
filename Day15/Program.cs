using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{ 
    class Program
    {
        private static List<Agent> Agents = new List<Agent>();
        private static string[] CurrentMap;

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt").ToArray();
            for (int i = 4; ;i++)
            {
                RunGame(input, i);
                Console.ReadKey();
            }
        }

        public static void RunGame(string[] input, int elfAttack)
        {
            DetectAgents(input, elfAttack);

            var IsRunning = true;
            var rounds = 0;
            while (IsRunning)
            {
                Agents = Agents.OrderBy(x => x.Y).ThenBy(y => y.X).ToList();
                for (int i = 0; i < Agents.Count(); i++)
                {
                    var agent = Agents[i];

                    var enemies = Agents.Where(x =>  x.IsElf != agent.IsElf).ToList();
                    if (!enemies.Any())
                    {
                        Console.WriteLine("Game ended after " + rounds + " rounds with. Score : " + rounds * Agents.Sum(x => x.Health));
                        IsRunning = false;
                        break;
                    }

                    // Check if any enemies in range, otherwise move
                    if (!enemies.Any(x => IsNextTo(agent, x)))
                    {
                        Move(agent, enemies);
                    }

                    var nearestEnemy = enemies.Where(x => IsNextTo(agent, x)).OrderBy(x => x.Health).ThenBy(x => x.Y).ThenBy(x => x.X).FirstOrDefault();

                    if (nearestEnemy == null) { continue; }

                    nearestEnemy.Health -= 3;
                    if (nearestEnemy.Health > 0)
                    {
                        continue;
                    }

                    if (nearestEnemy.IsElf)
                    {
                        IsRunning = false;
                        Console.WriteLine("Game ended with elf death at round " + rounds);
                        break;
                    }

                    int index = Agents.IndexOf(nearestEnemy);
                    Agents.RemoveAt(index);
                    if (index < i)
                        i--;
                }
                rounds++;             
            }
        }

        private static readonly (int dx, int dy)[] s_neis = { (0, -1), (-1, 0), (1, 0), (0, 1) };
        private static void Move(Agent agent, List<Agent> enemies)
        {
            var targetDestinations = GetDestinations(agent, enemies);

            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
            Dictionary<(int x, int y), (int px, int py)> previous = new Dictionary<(int x, int y), (int px, int py)>();
            queue.Enqueue((agent.X, agent.Y));
            previous.Add((agent.X, agent.Y), (-1, -1));
            while (queue.Count > 0)
            {
                (int x, int y) = queue.Dequeue();
                foreach ((int dx, int dy) in s_neis)
                {
                    (int x, int y) nei = (x + dx, y + dy);
                    if (previous.ContainsKey(nei) || !IsWalkable(nei.x, nei.y))
                        continue;

                    queue.Enqueue(nei);
                    previous.Add(nei, (x, y));
                }
            }
        
            List<(int x, int y)> getPath(int destX, int destY)
            {
                if (!previous.ContainsKey((destX, destY)))
                    return null;

                var path = new List<(int x, int y)>();
                (int x, int y) = (destX, destY);
                while (x != agent.X || y != agent.Y)
                {
                    path.Add((x, y));
                    (x, y) = previous[(x, y)];
                }
                path.Reverse();
                return path;
            }

            List<(int tx, int ty, List<(int x, int y)> path)> paths =
                targetDestinations
                .Select(t => (t.x, t.y, path: getPath(t.x, t.y)))
                .Where(t => t.path != null)
                .OrderBy(t => t.path.Count)
                .ThenBy(t => t.y)
                .ThenBy(t => t.x)
                .ToList();

            List<(int x, int y)> bestPath = paths.FirstOrDefault().path;
            if (bestPath != null)
            {
                agent.X = bestPath[0].x;
                agent.Y = bestPath[0].y;
            }
        }

        // Get all Enemy nearby locations that are walkable
        private static HashSet<(int x, int y)> GetDestinations(Agent agent, List<Agent> enemies)
        {
            var destinations = new HashSet<(int x, int y)>();
            foreach (var enemy in enemies)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        // Dont check diagonals
                        if (Math.Abs(x) == Math.Abs(y))
                        {
                            continue;
                        }
                        var px = x + enemy.X;
                        var py = y + enemy.Y;
                        if (IsWalkable(px, py))
                            destinations.Add((px, py));
                    }
                }
            }
            return destinations;
        }
   
        private static void DetectAgents(string[] map, int elfAttack)
        {
            Agents = new List<Agent>();
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map.Length; col++)
                {
                    if (map[row][col] == 'G' || map[row][col] == 'E')
                    {
                        var instance = new Agent()
                        {
                            X = col,
                            Y = row,
                            IsElf = map[row][col] == 'G' ? false : true,
                            Health = 200,
                            Attack = map[row][col] == 'G' ? 3 : elfAttack
                        };
                        Agents.Add(instance);
                    }
                }
            }

            CurrentMap = map.Select(x => x.Replace('E', '.').Replace('G', '.')).ToArray();
        }

        private static int CalculateDistance((int sx, int sy) source, (int tx, int ty) target)
        {
            return Math.Abs(source.sx - target.tx) + Math.Abs(source.sy - target.ty);
        }

        private static bool IsWalkable(int x, int y)
        {
            return CurrentMap[y][x] == '.' && Agents.All(a => (a.X != x || a.Y != y));
        }

        private static bool IsNextTo(Agent source, Agent target)
        {
            return Math.Abs(source.X - target.X) + Math.Abs(source.Y - target.Y) == 1;
        }

        public class Agent
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsElf { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
        }
    }
}
