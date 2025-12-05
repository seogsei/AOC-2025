
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode
{
    public class Day05 : BaseDay
    {
        private readonly string _input;

        public Day05()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var split =  _input.Split('\n', StringSplitOptions.TrimEntries);
            var ranges = split.TakeWhile(s => !string.IsNullOrEmpty(s)).Select(s => 
            {
                var first = long.Parse(s.Split('-').First());
                var second = long.Parse(s.Split('-').Last());
                return (first, second);
            });

            var ingridients = split.SkipWhile(s => !string.IsNullOrEmpty(s)).Skip(1).Select(long.Parse);

            var count = ingridients.Count(ingridient =>
            {
                foreach(var (first, second) in ranges)
                {
                    if (second < ingridient)
                        continue;
                    if (first > ingridient)
                        continue;
                    return true;
                }
                return false;
            });
            return new(count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var split = _input.Split('\n', StringSplitOptions.TrimEntries);
            var ranges = split.TakeWhile(s => !string.IsNullOrEmpty(s)).Select(s =>
            {
                var first = long.Parse(s.Split('-').First());
                var second = long.Parse(s.Split('-').Last());
                return (first, second);
            }).ToArray();

            Array.Sort(ranges);

            long start = 0;
            long end = 0;
            long sum = 0;

            foreach(var (first, second) in ranges)
            {
                if(first > end)
                {
                    //Include the end
                    sum += end - start + 1;
                    start = first;
                }

                end = Math.Max(second, end);
            }
            sum += end - start;
            return new(sum.ToString());
        }
    }
}
