namespace AdventOfCode.Challenges;

public class Day04
{
    private InputReader? _inputReader;
    private IEnumerable<IEnumerable<IEnumerable<int>>> _cleanedData = null!;

    public static async Task<Day04> Initialize(InputReader? reader)
    {
        var thisObject = new Day04
        {
            _inputReader = reader
        };

        thisObject._cleanedData = await thisObject.CleanData();

        return thisObject;
    }

    public int GetAnswerPart01()
    {
        return _cleanedData.Select(row =>
        {
            var first = row.First();
            var second = row.Last();
            return Contains(first, second) ? 1 : 0;
        }).Sum();
    }
    
    public int GetAnswerPart02()
    {
        return _cleanedData.Select(row =>
        {
            var first = row.First();
            var second = row.Last();
            return Overlaps(first, second) ? 1 : 0;
        }).Sum();
    }

    private bool Contains(IEnumerable<int> first, IEnumerable<int> second)
    {
        return IsContained(first, second) || IsContained(second, first);
    }

    private static bool IsContained(IEnumerable<int> first, IEnumerable<int> second)
    {
        return first.First() >= second.First() && first.Last() <= second.Last();
    }
    
    private bool Overlaps(IEnumerable<int> first, IEnumerable<int> second)
    {
        return IsOverlapping(first, second) || IsOverlapping(second, first);
    }
    
    private static bool IsOverlapping(IEnumerable<int> first, IEnumerable<int> second)
    {
        return first.Last() >= second.First() && second.Last() >= first.First();
    }

    private async Task<IEnumerable<IEnumerable<IEnumerable<int>>>> CleanData()
    {
        var data = await GetData();
        
        return data.Select(r => r
            .Split(",")
            .Select(s => s
                .Split("-")
                .Select(l => int.Parse(l))));
    }

    private async Task<List<string?>> GetData()
    {
        return await _inputReader!.GetInput("2022/day/4/input");
    }
}