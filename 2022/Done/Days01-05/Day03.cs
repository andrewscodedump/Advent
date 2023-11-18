namespace Advent2022;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        int total = 0;
        if (Part1)
            foreach (string rucksack in Inputs)
            {
                int size = rucksack.Length / 2;
                int dup = rucksack[..size].Intersect(rucksack[size..]).First();
                total += dup - (dup > 96 ? 96 : 38);
            }
        else
            for (int i = 0; i < Inputs.Length; i += 3)
            {
                int shared = Inputs[i].Intersect(Inputs[i + 1]).Intersect(Inputs[i + 2]).First();
                total += shared - (shared > 96 ? 96 : 38);
            }
        Output = total.ToString();
    }
}
