namespace Everybody2025;

public class Day09 : Advent.Day
{
    public override void DoWork()
    {
        List<string> genes = [.. Inputs.Select(i => i.Split(':')[1])];
        int result = 0;
        if (WhichPart == 1)
        {
            Output = GetScore(genes[2], genes[0], genes[1]).ToString();
            return;
        }

        List<List<int>> families = [];
        int length = Inputs.Length;
        for (int i = 0; i < length; i++)
        {
            string child = genes[i];
            bool foundParents = false;
            for (int j = 0; j < length; j++)
            {
                if (j == i) continue;
                string parent1 = genes[j];
                for (int k = 0; k < length; k++)
                {
                    if (k == i || k == j) continue;
                    string parent2 = genes[k];
                    if (IsMatch(child, parent1, parent2))
                    {
                        if (WhichPart == 2) result += GetScore(child, parent1, parent2);
                        if (WhichPart == 3) families.Add(new([i + 1, j + 1, k + 1]));
                        foundParents = true;
                        break;
                    }
                }
                if (foundParents) break;
            }
        }

        if (WhichPart == 2)
        {
            Output = result.ToString();
            return;
        }

        bool mergeDone;
        do
        {
            mergeDone = false;
            for (int i = 0; i < families.Count; i++)
            {
                for (int j = i + 1; j < families.Count; j++)
                {
                    if (families[i].Intersect(families[j]).Any())
                    {
                        families[i] = [.. families[i].Union(families[j])];
                        families[j] = [];
                        mergeDone = true;
                    }
                }
            }
            families = [.. families.Where(f => f.Count != 0)];
        } while (mergeDone);
        result = families.OrderByDescending(f => f.Count).First().Sum();
        Output = result.ToString();
    }

    private static int GetScore(string child, string parent1, string parent2)
    {
        int result1 = 0, result2 = 0;
        for (int i = 0; i < child.Length; i++)
        {
            if (child[i] == parent1[i]) result1++;
            if (child[i] == parent2[i]) result2++;
        }
        return result1 * result2;
    }

    private static bool IsMatch(string child, string parent1, string parent2)
    {
        for (int i = 0; i < child.Length; i++)
        {
            if(child[i] != parent1[i] && child[i] != parent2[i]) return false;
        }
        return true;
    }
}
