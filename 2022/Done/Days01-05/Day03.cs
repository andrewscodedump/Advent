namespace Advent2022;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        int total = 0;
        if (Part1)
            foreach (string rucksack in InputSplit)
            {
                int size = rucksack.Length / 2;
                int dup = rucksack[..size].Intersect(rucksack[size..]).First();
                total += dup - (dup > 96 ? 96 : 38);
            }
        else
            for (int i = 0; i < InputSplit.Length; i += 3)
            {
                int shared = InputSplit[i].Intersect(InputSplit[i + 1]).Intersect(InputSplit[i + 2]).First();
                total += shared - (shared > 96 ? 96 : 38);
            }
        Output = total.ToString();
    }
}
