namespace AdventOfCode
{
    public class Day04 : BaseDay
    {
        private readonly string _input;
        public Day04() 
        {
            _input = File.ReadAllText(InputFilePath);
        }

        private static IEnumerable<T> GetNeighbours<T>(T[][] map, int x, int y)
        {
            if(y > 0)
            {
                if(x > 0)
                {
                    yield return map[y - 1][x - 1];
                }

                yield return map[y - 1][x];

                if(x < map[y- 1].Length - 1)
                {
                    yield return map[y - 1][x + 1];
                }
            }

            if (x > 0)
            {
                yield return map[y][x - 1];
            }

            if (x < map[y].Length - 1)
            {
                yield return map[y][x + 1];
            }

            if (y < map.Length - 1)
            {
                if (x > 0)
                {
                    yield return map[y + 1][x - 1];
                }

                yield return map[y + 1][x];

                if (x < map[y + 1].Length - 1)
                {
                    yield return map[y + 1][x + 1];
                }
            }
        }

        public override ValueTask<string> Solve_1()
        {
            //Create a two dimensional array from the input
            var map = _input.Split('\n', StringSplitOptions.TrimEntries).Select(s => s.ToCharArray()).ToArray();
            int counter = 0;
            for(int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map.First().Length; j++)
                {
                    if (map[i][j] != '@') 
                    {
                        continue;
                    } 

                    if(GetNeighbours(map, j, i).Count(x => x == '@') < 4)
                    {
                        counter++;
                    }
                }
            }

            return new(counter.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            //Create a two dimensional array from the input
            var map = _input.Split('\n', StringSplitOptions.TrimEntries).Select(s => s.ToCharArray()).ToArray();
            int counter = 0;
            var removeList = new List<(int,int)>();
            do
            {
                removeList.Clear();
                for (int i = 0; i < map.Length; i++)
                {
                    for (int j = 0; j < map.First().Length; j++)
                    {
                        if (map[i][j] != '@')
                        {
                            continue;
                        }

                        if (GetNeighbours(map, j, i).Count(x => x == '@') < 4)
                        {
                            removeList.Add((i, j));
                            counter++;
                        }
                    }
                }

                foreach(var coords in removeList)
                {
                    map[coords.Item1][coords.Item2] = 'x';
                }            
            } while (removeList.Count > 0);

            return new(counter.ToString());
        }
    }
}
