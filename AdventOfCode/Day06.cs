
namespace AdventOfCode
{
    public class Day06 : BaseDay
    {
        private readonly string _input;
        public Day06() 
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var map = _input.Split("\r\n").Reverse().Select(l => l.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)).ToArray();
            long grandTotal = 0;

            for(int i = 0; i < map.First().Length; i++)
            {
                var equation = map.Select(s => s[i]);
                var operand = equation.First();
                var nums = equation.Skip(1).Select(long.Parse);

                var result = operand is "+" ? nums.Sum() : nums.Aggregate(1L, (x, y) => x * y);

                grandTotal += result;
            }

            return new(grandTotal.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var map = _input.Split("\r\n");

            long grandTotal = 0;
            var numList = new List<long>();
            for(int column = map.First().Length - 1; column >= 0; column--)
            {
                int num = 0;
                for(int row = 0; row < map.Length - 1; row++)
                {
                    if (char.IsNumber(map[row][column]))
                    {
                        num *= 10;
                        num += map[row][column] - '0';
                    }
                }

                numList.Add(num);

                char operandChar = map[^1][column];
                if (operandChar == ' ')
                    continue;

                var result = operandChar is '+' ? numList.Sum() : numList.Aggregate(1L, (x, y) => x * y);
                grandTotal += result;
                numList.Clear();
                column--;
            }
            return new(grandTotal.ToString());
        }
    }
}
