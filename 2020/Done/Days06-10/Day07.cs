namespace Advent2020;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        List<(int, string, string)> combinations = [];
        foreach (string rule in Inputs)
            foreach (string inner in rule.Split(" bags contain")[1].Replace("bags", " ").Replace("bag", " ").Replace(".", "").Replace("no ", "0 ").Split(','))
                combinations.Add((int.Parse(inner.Trim().Split(' ')[0]), inner.Replace(int.Parse(inner.Trim().Split(' ')[0]).ToString(), "").Trim(), rule.Split(" bags contain ")[0]));

        Output = (Part1 ? GetNumber("shiny gold", combinations, []) : GetNumber2("shiny gold", combinations) - 1).ToString();
    }

    private static int GetNumber(string colour, List<(int n, string inner, string)> combinations, List<string> coloursUsed)
    {
        int number = 0;
        List<(int, string, string)> possibilites = combinations.Where(c => c.inner == colour).ToList();
        foreach ((int _, string _, string o) in possibilites)
            if (!coloursUsed.Contains(o))
            {
                coloursUsed.Add(o);
                number += 1 + GetNumber(o, combinations, coloursUsed);
            }
        return number;
    }

    private static int GetNumber2(string colour, List<(int n, string inner, string outer)> combinations)
    {
        int number = 1;
        List<(int, string, string)> possibilites = combinations.Where(c => c.outer == colour).ToList();
        foreach ((int n, string i, string _) in possibilites)
            number += n * GetNumber2(i, combinations);
        return number;
    }
}
