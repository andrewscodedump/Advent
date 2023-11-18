namespace Advent2022;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int total1 = 0, total2 = 0;

        foreach (string pair in Inputs)
        {
            int[] limits = pair.Split(",-".ToCharArray()).Select(l => Convert.ToInt32(l)).ToArray();
            var elf1 = Enumerable.Range(limits[0], limits[1] - limits[0] + 1);
            var elf2 = Enumerable.Range(limits[2], limits[3] - limits[2] + 1);

            int overlap = elf1.Intersect(elf2).Count();
            if (overlap == elf1.Count() || overlap == elf2.Count()) total1++;
            if (overlap > 0) total2++;
        }
        Output = (Part1 ? total1 : total2).ToString();
    }
}
