namespace AdventOfCode
{
    public class Day02 : BaseDay
    {
        private readonly string _input;

        public Day02()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        static IEnumerable<long> RangeInt64(long start, long count)
        {
            for (long i = 0; i < count; i++)
                yield return start + i;
        }

        public override ValueTask<string> Solve_1()
        {

            long sum = 0;
            var nums = _input.Split(',').Select(s =>
            {
                var strings = s.Split('-');
                var start = long.Parse(strings[0]);
                var end = long.Parse(strings[1]);

                return RangeInt64(start, end - start);
            }).SelectMany(range => range);

            foreach (var num in nums)
            {
                var numString = num.ToString().AsSpan();
                var halfLength = numString.Length / 2;

                if (numString.Length % 2 != 0) continue;

                if (numString[..halfLength].SequenceEqual(numString.Slice(halfLength, halfLength)))
                {
                    sum += long.Parse(numString);
                }
            }
            
            return new(sum.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            long sum = 0;
            var nums = _input.Split(',').Select(s =>
                        {
                            var strings = s.Split('-');
                            var start = long.Parse(strings[0]);
                            var end = long.Parse(strings[1]);

                            return RangeInt64(start, end - start);
                        }).SelectMany(range => range);

            foreach (var num in nums)
            {
                var numString = num.ToString().AsSpan();
                var halfLength = numString.Length / 2;
                for (int groupSize = 1; groupSize <= halfLength; groupSize++)
                {
                    if (numString.Length % groupSize != 0) continue;

                    if (Loop(numString, groupSize))
                    {
                        sum += num;
                        break;
                    }
                }
            }

            static bool Loop(ReadOnlySpan<char> numString, int groupSize)
            {
                var sub = numString[..groupSize];
                for (int i = 1; i < numString.Length / groupSize; i++)
                {
                    if (!sub.SequenceEqual(numString.Slice(i * groupSize, groupSize)))
                        return false;
                }
                return true;
            }

            return new(sum.ToString());
        }
    }
}
