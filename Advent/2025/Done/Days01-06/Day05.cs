namespace Advent2025;

public class Day05 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        List<long[]> fresh = [.. InputNumbersPositive.Where(l => l.Length == 2).OrderBy(l => l[0])];
        long[] ingredients = [.. InputNumbers.Where(l => l.Length == 1).Select(l => l[0])];

        if(WhichPart==1)
            result = ingredients.Where(i => fresh.Exists(f => i >= f[0] && i <= f[1])).Count();
        else
        {
            bool found = false;
            do
            {
                found = false;
                for(int i = 0; i < fresh.Count; i++)
                {
                    for(int j = 0; j < i; j++)
                    {
                        if (fresh[i][0] >= fresh[j][0] && fresh[i][0] <= fresh[j][1])
                        {
                            if (fresh[i][1] > fresh[j][1])
                                fresh[j][1] = fresh[i][1];
                            fresh.RemoveAt(i);
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }

            } while (found);
            result = fresh.Sum(f => f[1] - f[0] + 1);
        }

        Output = result.ToString();
    }
}
