
namespace AdventOfCode
{
    public class Day07 : BaseDay
    {
        private readonly string _input;

        public Day07()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var lines = _input.Split("\r\n");

            int width = lines.First().Length;
            Span<char> beamPath = stackalloc char[width];

            int startPosition = lines.First().IndexOf('S');
            beamPath[startPosition] = '|';

            int splitCount = 0;
            for(int i = 2; i< lines.Length; i+=2)
            {
                for(int j = 0; j < beamPath.Length; j++)
                {
                    if (beamPath[j] is not '|')
                        continue;

                    if (lines[i][j] is '^')
                    {
                        beamPath[j] = ' ';
                        beamPath[j - 1] = beamPath[j + 1] = '|';
                        splitCount++;
                    }
                }
            }

            return new(splitCount.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var lines = _input.Split("\r\n");

            Span<long> beamPath = stackalloc long[lines.First().Length];
            beamPath[lines.First().IndexOf('S')] = 1;

            for (int i = 2; i < lines.Length; i += 2)
            {
                for (int j = 0; j < beamPath.Length; j++)
                {
                    if (beamPath[j] < 1)
                        continue;

                    if (lines[i][j] is '^')
                    {
                        beamPath[j - 1] += beamPath[j];
                        beamPath[j + 1] += beamPath[j];
                        beamPath[j] = 0;
                    }
                }
            }

            long total = 0L;
            foreach (var paths in beamPath)
                total += paths;

            return new(total.ToString());
        }
    }
}
