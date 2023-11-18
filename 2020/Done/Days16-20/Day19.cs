namespace Advent2020;

public partial class Day19 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        List<(int, List<string>)> unresolved = new();
        List<string> messages = new(), validMessages = new(), c42 = new(), c31 = new();
        Queue<(int, List<string>)> resolved = new();

        foreach (string input in Inputs)
        {
            if (string.IsNullOrEmpty(input)) continue;
            string[] bits = input.Split(new char[] { ':', '|' });
            if (bits.Length == 1)
                messages.Add(bits[0]);
            else
            {
                int ruleNo = int.Parse(bits[0]);
                string rule1 = bits[1];
                if (bits.Length == 2)
                    if (!rule1.Any(char.IsDigit))
                        resolved.Enqueue((ruleNo, new() { rule1.Replace("\"", "").Trim() }));
                    else
                        unresolved.Add((ruleNo, new() { rule1 + ' ' }));
                else
                    unresolved.Add((ruleNo, new() { rule1 + ' ', bits[2] + ' ' }));
            }
        }

        #endregion Setup Variables and Parse Inputs

        while (resolved.Count > 0)
        {
            (int ruleNo, List<string> combos) = resolved.Dequeue();
            for (int i = 0; i < unresolved.Count; i++)
            {
                bool changeMade = false;
                (int partial, List<string> rules) = unresolved[i];
                if (rules.Count == 0) continue;
                string pattern = ' ' + ruleNo.ToString() + ' ';

                while (rules.Any(r => r.Contains(pattern)))
                {
                    List<string> newList = new();
                    foreach (string rule in rules)
                    {
                        if (!rule.Contains(pattern)) newList.Add(rule);
                        else
                        {
                            int pos = rule.IndexOf(pattern);
                            foreach (string combo in combos)
                            {
                                string newRule = rule[0..pos] + " " + combo + rule[(pos + pattern.Length - 1)..^0];
                                if (!newRule.Any(char.IsDigit) && partial == 0)
                                    validMessages.Add(newRule.Replace(" ", ""));
                                else
                                    newList.Add(newRule);
                            }
                            changeMade = true;
                        }
                    }
                    rules = newList.ToList();
                }

                if (!rules.Any(s => s.Any(char.IsDigit)))
                {
                    rules = rules.Select(s => s.Replace(" ", "")).ToList();
                    if (partial == 42) c42 = rules;
                    else if (partial == 31) c31 = rules;
                    else resolved.Enqueue((partial, rules));
                    unresolved[i] = (partial, new List<string>());
                }
                else if (changeMade)
                    unresolved[i] = (partial, rules);
            }
            unresolved.RemoveAll(t => t.Item2.Count == 0);
        }
        Output = messages.Count(m => IsValid(m, c42, c31, Part1)).ToString();
    }

    private static bool IsValid(string message, List<string> c42, List<string> c31, bool isPart1)
    {
        // 0 = 8 11;  8 = 42 | 42 8; 11 = 42 31 | 42 11 31
        // so 0 = one or more combos of 42 (16 of these, all length 5 (128 x 8 for live))
        // followed by one or more combos of 42s plus an equal no of 31s (16 of these, all length 5 (128 x 8 for live))
        int len = c42[0].Length, slices = message.Length / len, max31s = (slices - 1) / 2;
        if (message.Length % len != 0                               // Message length is not a multiple of the lengths of the constituent parts (in reality, there are none of these)
            || (isPart1 && message.Length != len * 3)
            || !c42.Contains(message[0..len])                       // It doesn't begin with a valid 42 
            || !c31.Contains(message[^len..^0])) return false;      // It doesn't end with a valid 31
        if (isPart1) return c42.Contains(message[len..(2 * len)]);  // Part 1 must be 42 42 31

        int[] v42 = new int[slices], v31 = new int[slices];
        for (int i = 0; i < slices; i++)
        {
            string slice = message[(i * len)..((i + 1) * len)];
            if (c42.Contains(slice)) v42[i] = 1;
            if (c31.Contains(slice)) v31[i] = 1;
        }

        for (int n = 1; n <= max31s; n++)
            if (v31[^n..^0].Sum() + v42[0..(slices - n)].Sum() == slices) return true;
        return false;
    }
}
