namespace Advent2020;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        List<(int, string, string)> combinations = new();
        foreach (string rule in InputSplit)
            foreach (string inner in rule.Split(" bags contain")[1].Replace("bags", " ").Replace("bag", " ").Replace(".", "").Replace("no ", "0 ").Split(','))
                combinations.Add((int.Parse(inner.Trim().Split(' ')[0]), inner.Replace(int.Parse(inner.Trim().Split(' ')[0]).ToString(), "").Trim(), rule.Split(" bags contain ")[0]));

        Output = (WhichPart == 1 ? GetNumber("shiny gold", combinations, new()) : GetNumber2("shiny gold", combinations) - 1).ToString();
    }

    private int GetNumber(string colour, List<(int n, string inner, string)> combinations, List<string> coloursUsed)
    {
        int number = 0;
        List<(int, string, string)> possibilites = (List<(int, string, string)>)combinations.Where(c => c.inner == colour).ToList();
        foreach ((int n, string i, string o) in possibilites)
            if (!coloursUsed.Contains(o))
            {
                coloursUsed.Add(o);
                number += 1 + GetNumber(o, combinations, coloursUsed);
            }
        return number;
    }

    private int GetNumber2(string colour, List<(int n, string inner, string outer)> combinations)
    {
        int number = 1;
        List<(int, string, string)> possibilites = (List<(int, string, string)>)combinations.Where(c => c.outer == colour).ToList();
        foreach ((int n, string i, string o) in possibilites)
            number += n * GetNumber2(i, combinations);
        return number;
    }
}
