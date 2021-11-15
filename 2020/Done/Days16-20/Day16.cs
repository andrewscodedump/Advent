namespace Advent2020;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        Dictionary<string, List<int>> rules = new();
        List<int> mine = new();
        List<List<int>> others = new();
        string parseMode = "rules";
        long result = WhichPart == 1 ? 0 : 1;

        foreach (string line in InputSplit)
        {
            if (line == string.Empty) continue;
            parseMode = line.StartsWith("your ") ? "mine" : line.StartsWith("nearby") ? "others" : parseMode;
            switch (parseMode)
            {
                case "rules":
                    string[] bits = line.Split(new string[] { ": ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                    string rule = bits[0];
                    List<int> validNumbers = new();
                    foreach (string range in bits[1..])
                        validNumbers.AddRange(Enumerable.Range(int.Parse(range.Split('-')[0]), int.Parse(range.Split('-')[1]) - int.Parse(range.Split('-')[0]) + 1));
                    rules.Add(rule, validNumbers);
                    break;
                case "mine":
                    if (line.StartsWith("your")) continue;
                    mine.AddRange(line.Split(',').Select(int.Parse));
                    break;
                case "others":
                    if (line.StartsWith("nearby")) continue;
                    List<int> numbers = line.Split(',').Select(int.Parse).ToList();
                    others.Add(numbers);
                    break;
            }
        }

        #endregion Setup Variables and Parse Inputs

        foreach (List<int> numbers in new List<List<int>>(others))
            foreach (int number in numbers)
            {
                bool foundIt = false;
                foreach (List<int> rule in rules.Values)
                    if (rule.Contains(number))
                    {
                        foundIt = true;
                        break;
                    }
                if (!foundIt)
                {
                    others.Remove(numbers);
                    if (WhichPart == 2) break;
                    result += number;
                }
            }

        if (WhichPart == 2)
        {
            List<int>[] positions = new List<int>[rules.Count];
            foreach (List<int> numbers in others)
                for (int i = 0; i < numbers.Count; i++)
                    if (positions[i] == null)
                        positions[i] = new() { numbers[i] };
                    else
                        positions[i].Add(numbers[i]);

            List<(int, string)> mappings = new();
            while (rules.Count > 0)
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    int numberFound = 0, rulesFound = 0;
                    string ruleName = string.Empty;
                    foreach (KeyValuePair<string, List<int>> rule in rules)
                    {
                        numberFound = 0;
                        foreach (int number in positions[i])
                            if (rule.Value.Contains(number))
                                numberFound++;
                        if (numberFound == positions[i].Count)
                        {
                            ruleName = rule.Key;
                            rulesFound++;
                            if (rulesFound > 1) continue;
                        }
                    }
                    if (rulesFound == 1)
                    {
                        mappings.Add((i, ruleName));
                        rules.Remove(ruleName);
                    }
                }
            }
            foreach ((int position, string name) in mappings)
                if (name.StartsWith("departure")) result *= mine[position];
        }

        Output = result.ToString();
    }
}
