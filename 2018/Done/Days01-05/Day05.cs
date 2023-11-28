namespace Advent2018;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        string letters = Part1 ? "!" : "qwertyuiopasdfghjklzxcvbnm";
        int bestLen = int.MaxValue;

        foreach (char letter in letters)
        {
            int pos = 0;
            List<char> work = Inputs[0].Replace(letter.ToString(), "").Replace(char.ToUpper(letter).ToString(), "").ToList();
            do
            {
                if (pos + 1 == work.Count) break;
                if (Math.Abs(work[pos] - work[pos + 1]) == 32)
                {
                    work.RemoveAt(pos + 1);
                    work.RemoveAt(pos);
                    if (pos > 0) pos--;
                }
                else
                    pos++;
            } while (true);
            bestLen = Math.Min(bestLen, work.Count);
        }

        Output = bestLen.ToString();
    }
}
