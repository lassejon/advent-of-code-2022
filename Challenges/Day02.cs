namespace AdventOfCode.Challenges;

public class Day02
{
    private List<string?> _input = null!;
    private List<List<Hand>?> _transformedInput = null!;
    
    public static async Task<Day02> Initialize(InputReader reader)
    {
        var thisObject = new Day02
        {
            _input = await reader.GetInput("2022/day/2/input"),
        };
        thisObject._transformedInput = thisObject.TransformInput();
        
        return thisObject;
    }

    public int GetPointSumPart01()
    {
        return _transformedInput.Sum(hands => CalculatePoints(hands.First(), hands.Last()));
    }
    
    public int GetPointSumPart02()
    {
        return _transformedInput.Sum(hands => CalculatePointsEasy(hands.First(), hands.Last()));
    }

    private int CalculatePointsEasy(Hand oppenentHand, Hand moveToMake)
    {
        switch (moveToMake)
        {
            case Hand.Paper:
                return (int)oppenentHand + 3;
            case Hand.Rock:
                return Lose(oppenentHand);
            case Hand.Scissors:
                return Win(oppenentHand) + 6;
        }

        return 0;
    }
    
    private int Lose(Hand hand)
    {
        return hand switch
        {
            Hand.Rock => (int)Hand.Scissors,
            Hand.Paper => (int)Hand.Rock,
            Hand.Scissors => (int)Hand.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
        };
    }

    private int Win(Hand hand)
    {
        switch (hand)
        {
            case Hand.Rock:
                return (int)Hand.Paper;
            case Hand.Paper:
                return (int)Hand.Scissors;
            case Hand.Scissors:
                return (int)Hand.Rock;
        }

        return 0;
    }

    private int CalculatePoints(Hand oppenentHand, Hand myHand)
    {
        if (oppenentHand == myHand)
        {
            return 3 + (int)myHand;
        }
        
        if (oppenentHand == Hand.Paper && myHand == Hand.Scissors ||
            oppenentHand == Hand.Rock && myHand == Hand.Paper ||
            oppenentHand == Hand.Scissors && myHand == Hand.Rock)
        {
            return 6 + (int)myHand;
        }

        return (int)myHand;
    }

    private List<List<Hand>?> TransformInput()
    {
        return _input.Select(kv => kv?.Split(" ").Select(s => MapToHand(char.Parse(s))).ToList()).ToList();
    }

    private Day02() {}

    private Hand MapToHand(char hand)
    {
        switch (hand)
        {
            case 'A':
            case 'X':
                return Hand.Rock;
            case 'B':
            case 'Y':
                return Hand.Paper;
            case 'C':
            case 'Z':
                return Hand.Scissors;
        }

        return Hand.Paper;
    }
    
    private enum Hand
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }
}