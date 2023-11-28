namespace Advent2017;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        string work = Inputs[0];
        int removals = 0, sum = 0, level = 0;

        // Remove cancellations
        while (work.Contains('!')) work = work.Remove(work.IndexOf('!'), 2);

        // Remove garbage
        while (work.Contains('<'))
        {
            int charsToRemove = work.IndexOf('>', work.IndexOf('<')) - work.IndexOf('<') + 1;
            work = work.Remove(work.IndexOf('<'), charsToRemove);
            removals += charsToRemove - 2;
        }

        // Count
        for (int i = 0; i < work.Length; i++)
        {
            if (work[i] == '{')
                level++;
            if (work[i] == '}')
            {
                sum += level;
                level--;
            }
        }
        Output = (Part1 ? sum : removals).ToString();
    }

}
