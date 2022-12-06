namespace AdventOfCode.Challenges;

public class Day05
{
    private InputReader? _inputReader;
    private List<string?> _data = null!;
    private List<List<char>> _stacks = null!;
    private List<IEnumerable<char>> _instructions = null!;
    private int _skipLength;

    public static async Task<Day05> Initialize(InputReader? reader)
    {
        var thisObject = new Day05
        {
            _inputReader = reader
        };

        thisObject._data = await thisObject.GetData();
        thisObject._stacks = await thisObject.GetStacks();
        thisObject._skipLength = thisObject.GetSkipLength();
        thisObject._instructions = thisObject.GetInstructions();

        var x = thisObject.GetMovedStacks();

        return thisObject;
    }

    private IEnumerable<IEnumerable<char>> GetMovedStacks()
    {
        _instructions.ForEach(i =>
        {
            var pilesToMove = int.Parse(i.ElementAt(0).ToString());
            var moveFrom = int.Parse(i.ElementAt(1).ToString());
            var moveTo = int.Parse(i.ElementAt(2).ToString());

            var range = new List<char>();
            while (pilesToMove > 0)
            {
                var last = _stacks[moveFrom].Count - 1;
                range.Add(_stacks[moveFrom][last]);
                _stacks[moveFrom].RemoveAt(last);

                pilesToMove--;
            }
            
            _stacks[moveTo].AddRange(range);
        });

        return _stacks;
    }

    private List<IEnumerable<char>> GetInstructions()
    {
        var x = _data.Skip(_skipLength).Select(row => row.Where(char.IsNumber));
        return x.ToList();
    }
    
    private async Task<List<List<char>>> GetStacks()
    {
        var matrixLength = _data.First().Length;

        var matixList = _data.Where(row => row.Length == matrixLength);

        var matrixArray = matixList.Select(row => row.ToCharArray());

        var matrix = RotateMatrixCounterClockwise(matrixArray);
        var stacks = matrix
            .Where(stack => char.IsNumber(stack.Last()))
            .Select(stack => stack.Reverse().Skip(1).Where(char.IsLetter).ToList())
            .Reverse();
        
        return stacks.ToList();
    }

    private int GetSkipLength()
    {
        return _stacks.Max(x => x.Count()) + 2;
    }
    
    private static IEnumerable<char[]> RotateMatrixCounterClockwise(IEnumerable<char[]> oldMatrix)
    {
        var newMatrix = new char[oldMatrix.First().Length][];

        for (var i = 0; i < oldMatrix.First().Length; i++)
        {
            newMatrix[i] = new char[oldMatrix.Count()];
        }
        
        var newRow = 0;
        for (var oldColumn = oldMatrix.First().Length - 1; oldColumn >= 0; oldColumn--)
        {
            var newColumn = 0;
            for (var oldRow = 0; oldRow < oldMatrix.Count(); oldRow++)
            {
                newMatrix[newRow][newColumn] = oldMatrix.ElementAt(oldRow)[oldColumn];
                newColumn++;
            }
            newRow++;
        }
        return newMatrix;
    }

    private async Task<List<string?>> GetData()
    {
        return await _inputReader!.GetInput("2022/day/5/input");
    }
}