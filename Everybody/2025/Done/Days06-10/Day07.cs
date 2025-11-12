namespace Everybody2025;

public class Day07 : Advent.Day
{
    public override void DoWork()
    {
        List<string> names = [.. Input.Split(',')];
        Dictionary<char, char[]> rules = [];
        foreach (string rule in Inputs[2..])
            rules[rule.Split(" > ")[0][0]] = [.. rule.Split(" > ")[1].Replace(",","").ToCharArray()];

        switch (WhichPart)
        {
            case 1:
                Output = names.First(n => CheckName(n, rules));
                break;
            case 2:
                Output = names.Select((n, i) => CheckName(n, rules) ? i + 1 : 0).Sum().ToString();
                break;
            case 3:
                HashSet<string> foundNames = [];
                foreach (string name in names)
                {
                    if (!CheckName(name, rules)) continue;
                    Queue<string> q = new([name]);
                    while (q.Count > 0)
                    {
                        string currentName = q.Dequeue();
                        if (!rules.TryGetValue(currentName[^1], out char[] rule)) continue;
                        foreach (char letter in rule)
                        {
                            string newName = currentName + letter;
                            if (newName.Length < 11 && !foundNames.Contains(newName))
                                q.Enqueue(newName);
                            if (newName.Length >= 7 && newName.Length <= 11)
                                foundNames.Add(newName);
                        }
                    }
                }
                Output = foundNames.Count.ToString();
                break;
        }
    }

    private static bool CheckName (string name, Dictionary<char, char[]> rules)
    {
        bool nameOK = true;
        for (int i = 0; i < name.Length - 1; i++)
        {
            if (!rules[name[i]].Contains(name[i + 1]))
            {
                nameOK = false;
                break;
            }
        }
        return nameOK;
    }
}
