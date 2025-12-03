namespace Advent2025;

public class Day03 : Advent.Day
{
    public override void DoWork()
    {
        long total = 0;
        int numberToSelect = WhichPart == 1 ? 2 : 12;
        foreach (string bank in Inputs)
        {
            int[] bankValues = [.. bank.ToCharArray().Select(x => x - 48)];
            for (int i = numberToSelect - 1; i >= 0; i--)
            {
                //find the biggest that isn't in the last i
                (long joltage, long pos) = bankValues[..^i].Select((val, pos) => (val, pos)).First(v => v.val == bankValues[..^i].Max());
                total += joltage * (long)Math.Pow(10, i);
                //remove the ones before the one we've just used
                bankValues = bankValues[(int)(pos + 1)..];
            }
        }

        Output = total.ToString();
    }
}
