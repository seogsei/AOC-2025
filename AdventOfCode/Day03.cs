
using System.Runtime.InteropServices.Marshalling;

namespace AdventOfCode
{
    public class Day03 : BaseDay
    {
        private readonly string _input;

        public Day03()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            long totalJoltage = 0;
            var banks = _input.Split('\n',StringSplitOptions.TrimEntries);
            foreach (var bank in banks) 
            {
                char ones = bank[^1];
                char tens = bank[^2];
                for (int i = bank.Length - 2; i >= 0; i--)
                {
                    if (bank[i] >= tens)
                    {
                        if (tens > ones)
                            ones = tens;

                        tens = bank[i];
                    }
                }

                var maxJoltage = (tens - '0') * 10 + (ones - '0');
                totalJoltage += maxJoltage;
            }
            return new(totalJoltage.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var banks = _input.Split('\n', StringSplitOptions.TrimEntries);
            
            long totalJoltage = 0;
            const int batterySize = 12;
            
            Span<char> span = stackalloc char[batterySize];
            foreach (var bank in banks)
            {
                bank.AsSpan(bank.Length - batterySize, batterySize).CopyTo(span);
                for (int i = bank.Length - batterySize - 1; i >= 0; i--)
                {
                    char hold = bank[i];
                    if (hold < span[0]) continue;

                    int j;
                    for (j = 1; j < batterySize; j++)
                    {
                        if (span[j] > span[j - 1])
                            break;
                    }
                    for(j--; j >= 1; j--)
                    {
                        span[j] = span[j - 1];
                    }

                    span[0] = hold;
                }

                totalJoltage += long.Parse(span);
            }
            return new(totalJoltage.ToString());
        }
    }
}
