namespace Advent2020;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        int turn, lastNumber = 0;
        int[] lastPositions = new int[Part1 ? 2020 : 30000000];
        for (turn = 1; turn < InputNumbers[0].Length + 1; turn++) lastPositions[InputNumbers[0][turn - 1]] = turn;

        while (turn < (Part1 ? 2020 : 30000000))
        {
            int nextNumber = lastPositions[lastNumber] == 0 ? 0 : turn - lastPositions[lastNumber];
            lastPositions[lastNumber] = turn;
            lastNumber = nextNumber;
            turn++;
        }

        Output = lastNumber.ToString();
    }
}
