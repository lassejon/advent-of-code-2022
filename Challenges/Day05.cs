using System.Collections;

namespace AdventOfCode.Challenges;

public class Day05
{
    private InputReader? _inputReader;
    private List<string?> _data = null!;
    private List<Stack<char>> _stacks = null!;
    private List<IEnumerable<int>> _instructions = null!;
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

        //var x = thisObject.GetMovedStacks();

        return thisObject;
    }

    public (char, int)[] GetAnswerPartOne()
    {
        return GetMovedStacks().Select((stacks, i) => stacks.Count > 0 ? (stacks.Pop(), i + 1) : ('å', i + 1)).ToArray();
    }

    private IEnumerable<Stack<char>> GetMovedStacks()
    {
        var index = 0;
        _instructions.ForEach(i =>
        {
            var pilesToMove = i.ElementAt(0);
            var moveFrom = i.ElementAt(1) -1;
            var moveTo = i.ElementAt(2) - 1;

            var range = new List<char>();
            while (pilesToMove > 0)
            {
                if (index == 13)
                {
                    
                }
                _stacks[moveTo].Push(_stacks[moveFrom].Pop());
                pilesToMove--;
            }

            index++;
        });

        return _stacks;
    }

    private List<IEnumerable<int>> GetInstructions()
    {
        var isPrevNum = false;
        var prev = int.MaxValue;
        var x = _data.Skip(_skipLength).Select(row => row.Select(c =>
        {
            var toParse = c.ToString();
            if (isPrevNum && char.IsNumber(c))
            {
                toParse = $"{prev}{c}";
            }

            var result = -1;
            if (int.TryParse(toParse, out var parsed))
            {
                result = parsed;
                prev = parsed;
                isPrevNum = true;
            }
            else
            {
                isPrevNum = false;
            }

            return result;
        }).Where(n => n != -1)).Select(x => x.Skip(x.Count() - 3));
        return x.ToList();
    }
    
    private async Task<List<Stack<char>>> GetStacks()
    {
        var matrixLength = _data.First().Length;

        var matixList = _data.Where(row => row.Length == matrixLength);

        var matrixArray = matixList.Select(row => row.ToCharArray());

        var matrix = RotateMatrixCounterClockwise(matrixArray);
        var stacks = matrix
            .Where(stack => char.IsNumber(stack.Last()))
            .Select(stack => new Stack<char>(stack.Take(stack.Length - 1).Where(char.IsLetter)))
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