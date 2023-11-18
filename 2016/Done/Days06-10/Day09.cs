namespace Advent2016;

public partial class Day09 : Advent.Day
{
    public override void DoWork() => Output = GetLength(Input).ToString();

    private long GetLength(string input)
    {
        if (input.StartsWith("("))
        {
            int nextBracket = input.IndexOf(')') - 1;
            string[] numbers = input.Substring(1, nextBracket).Split('x');
            int lettersToMultiply = int.Parse(numbers[0]);
            long multiplier = int.Parse(numbers[1]);
            return Part1
                ? (multiplier * lettersToMultiply) + GetLength(input[(nextBracket + 2 + lettersToMultiply)..])
                : (multiplier * GetLength(input.Substring(nextBracket + 2, lettersToMultiply))) + GetLength(input[(nextBracket + 2 + lettersToMultiply)..]);
        }
        else if (input.Contains('('))
        {
            int pos = input.IndexOf("(");
            return pos + GetLength(input[pos..]);
        }
        else
        {
            return input.Length;
        }
    }
}
