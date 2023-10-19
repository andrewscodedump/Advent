namespace Advent2017;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int[] input = Array.ConvertAll(InputSplit, int.Parse);
        List<int[]> combos = new();

        do
        {
            combos.Add((int[])input.Clone());
            int curVal = input.Max();
            int curPos = input.ToList().IndexOf(curVal);
            input[curPos] = 0;
            for (int pos = 1; pos <= curVal; pos++)
                input[(curPos + pos) % input.Length]++;
        } while (!combos.Any(combo => combo.SequenceEqual(input)));

        Output = (combos.Count - (Part1 ? 0 : combos.IndexOf(combos.First(combo => combo.SequenceEqual(input))))).ToString();
    }
}
