using System.Numerics;
using Spectre.Console;

namespace AdventOfCode
{
    public class Day08 : BaseDay
    {
        private readonly string _input;
        
        public Day08()
        {
            _input = File.ReadAllText(InputFilePath);
        }
        public override ValueTask<string> Solve_1()
        {
            const int numberOfConnections = 1000;

            var junctionBoxes = _input.Split("\r\n").Select(l => 
            {
                var parts = l.Split(',').Select(float.Parse).ToArray();
                return new Vector3(parts[0], parts[1], parts[2]);
            }).ToArray();

            var distances = new List<(int, int, float)>((junctionBoxes.Length - 1) * junctionBoxes.Length / 2);
            
            for(int i = 0; i < junctionBoxes.Length; i ++)
            {
                for(int j = i + 1; j < junctionBoxes.Length; j++)
                {
                    distances.Add(new(i, j, Vector3.DistanceSquared(junctionBoxes[i], junctionBoxes[j])));
                }
            }

            distances.Sort((lhs,rhs) => lhs.Item3.CompareTo(rhs.Item3));

            var set = new DisjointUnionSet(junctionBoxes.Length);

            foreach(var distance in distances.Take(numberOfConnections))
            {
                set.Union(distance.Item1, distance.Item2);
            }

            var sizes = new int[junctionBoxes.Length];
            for(int i = 0; i < sizes.Length; i++)
            {
                sizes[set.FindRoot(i)]++;
            }
            Array.Sort(sizes);

            var result = sizes.Reverse().Take(3).Aggregate(1, (x, y) => x * y);

            return new(result.ToString()); 
        }

        public override ValueTask<string> Solve_2()
        {
            var junctionBoxes = _input.Split("\r\n").Select(l =>
            {
                var parts = l.Split(',').Select(float.Parse).ToArray();
                return new Vector3(parts[0], parts[1], parts[2]);
            }).ToArray();

            var distances = new List<(int, int, float)>((junctionBoxes.Length - 1) * junctionBoxes.Length / 2);

            for (int i = 0; i < junctionBoxes.Length; i++)
            {
                for (int j = i + 1; j < junctionBoxes.Length; j++)
                {
                    distances.Add(new(i, j, Vector3.DistanceSquared(junctionBoxes[i], junctionBoxes[j])));
                }
            }

            distances.Sort((lhs, rhs) => lhs.Item3.CompareTo(rhs.Item3));

            var set = new DisjointUnionSet(junctionBoxes.Length);

            foreach (var distance in distances.Take(1000))
            {
                set.Union(distance.Item1, distance.Item2);
            }

            string result = string.Empty;
            foreach (var distance in distances.Skip(1000))
            {
                if(set.Union(distance.Item1, distance.Item2) && CheckCompleteCircuit(set))
                {
                    result = (junctionBoxes[distance.Item1].X * junctionBoxes[distance.Item2].X).ToString();
                    break;
                }
            }

            return new(result);
        }

        private static bool CheckCompleteCircuit(DisjointUnionSet set)
        {
            var root = set.FindRoot(0);
            for (int i = 1; i < set.Length; i++)
            {
                if (root != set.FindRoot(i))
                {
                    return false;
                }
            }
            return true;
        }

        private class DisjointUnionSet(int n)
        {
            private readonly int[] _parent = [.. Enumerable.Range(0, n)];
            public int Length => _parent.Length;

            public int FindRoot(int i)
            {
                int root = _parent[i];

                if (_parent[root] != root)
                {
                    return _parent[i] = FindRoot(root);
                }

                return root;
            }

            public bool Union(int x, int y)
            {
                int xRoot = FindRoot(x);
                int yRoot = FindRoot(y);

                if (xRoot == yRoot) 
                    return false;

                _parent[xRoot] = yRoot;
                return true;
            }
        }

        private static int FindRoot(int[] parents, int i)
        {
            int root = parents[i];

            if (parents[root] != root)
            {
                return parents[i] = FindRoot(parents, root);
            }

            return root;
        }
    }
}
