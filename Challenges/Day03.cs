namespace AdventOfCode.Challenges;

public class Day03
{
    private InputReader? _inputReader;
    private List<string?> _cleanedData = null!;

    public static async Task<Day03> Initialize(InputReader? reader)
    {
        var thisObject = new Day03
        {
            _inputReader = reader
        };

        thisObject._cleanedData = await thisObject.GetData();

        return thisObject;
    }
    
    public int TransformInput()
    {
        var result = _cleanedData.Sum(r =>
        {
            var left = r.Take(r.Length / 2).ToHashSet();
            var right = r.Skip(r.Length / 2).ToHashSet();

            var x = left.AsParallel().Where(c => right.Contains(c)).ToArray();

            var sum = x.Sum(c => char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27);
            return sum;
        });

        return result;
    }

    // TODO
    public int TransformInputPart02()
    {
        var result = _cleanedData
            .Select((r, i) => (r.ToHashSet(), i))
            .GroupBy(p => p.i / 3, i => i.Item1)
            .Select(g =>
            {
                var shared = new HashSet<char>();
                var x = g.Select((s, i) =>
                {
                    if (i == 0)
                    {
                        shared = s;
                    }

                    return s.AsParallel().Where(c => shared.Contains(c)).ToArray();
                });

                return x.Select(chars => chars.Sum(c => char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27));
            })
            .Sum(i => i.Sum());

        return result;
    }
    
    private Day03()
    {
    }

    private async Task<List<string?>> GetData()
    {
        return (await _inputReader!.GetInput("2022/day/3/input"));
    }
}