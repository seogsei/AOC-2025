namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;

    private readonly IEnumerable<int> _rotations;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
        _rotations = _input.Split("\n").Select(s =>
        {
            var direction = s[0] == 'R' ? +1 : -1;
            var distance = int.Parse(s[1..]);
            return direction * distance;
        });
    }

    public override ValueTask<string> Solve_1()
    {
        int position = 50;
        int counter = 0;

        foreach(int rotation in _rotations)
        {
            position += rotation;

            if (position % 100 == 0)
                counter++;
        }

        return new(counter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int position = 50;
        int counter = 0;

        foreach (int rotation in _rotations)
        {
            if(rotation > 0)
            {
                counter += (position + rotation) / 100;
                position = (position + rotation) % 100;
            }
            else
            {
                var reversedPosition = (100 - position) % 100;
                counter += (reversedPosition - rotation) / 100;
                position = (((position + rotation) % 100) + 100) % 100;
            }
        }

        return new(counter.ToString());
    }
}
