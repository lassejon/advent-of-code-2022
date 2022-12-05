namespace AdventOfCode.Challenges;

public class Day01
{
    private InputReader? _inputReader;
    private IEnumerable<int> _cleanedData = null!;

    public static async Task<Day01> Initialize(InputReader? reader)
    {
        var thisObject = new Day01
        {
            _inputReader = reader
        };

        thisObject._cleanedData = await thisObject.GetSums();

        return thisObject;
    }
    
    private Day01()
    {
    }

    public int GetPartOneAnswer()
    {
        return _cleanedData.Max();
    }

    public int GetPartTwoAnswer()
    {
        return _cleanedData.OrderByDescending(c => c).Take(3).Sum();
    }

    private async Task<IEnumerable<int>> GetSums()
    {
        var list = await _inputReader.GetInput("2022/day/1/input");

        var sum = 0;
        var result = list.Select(s =>
        {
            if (int.TryParse(s, out var a))
            {
                sum += a;
            }
            else
            {
                sum = 0;
            }

            return sum;
        });

        return result;
    }
}