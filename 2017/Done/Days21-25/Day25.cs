namespace Advent2017;

public partial class Day25 : Advent.Day
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    public override void DoWork()
    {
        if (WhichPart == 2) return;

        Input = Regex.Replace(Input, @"[ :\-\.]", "")  + ";Stophere";
        Dictionary<string, Dictionary<int, (int, int, string)>> rules = new();
        Dictionary<int, int> tape = new() { { 0, 0 } };
        int steps = 0, curPos = 0, ones = 0;
        string curState = string.Empty;

        string state = string.Empty;
        int curValue = 0, newValue = 0, move = 0;
        Dictionary<int, (int, int, string)> stateRules = new();
        foreach (string ruleText in InputSplit)
        {
            if (ruleText.Length < 5) continue;
            switch (ruleText[..5])
            {
                case "Begin": curState = ruleText[^1..]; break;
                case "Perfo": steps = int.Parse(ruleText[31..^5]); break;
                case "Insta":
                    if (!string.IsNullOrEmpty(state))
                        rules.Add(state, stateRules);
                    stateRules = new();
                    state = ruleText[7..];
                    break;
                case "Ifthe": curValue = int.Parse(ruleText[19..]); break;
                case "Write": newValue = int.Parse(ruleText[13..]); break;
                case "Moveo": move = ruleText.Substring(16, 1) == "r" ? 1 : -1; ; break;
                case "Conti": stateRules.Add(curValue, (newValue, move, ruleText[17..])); break;
                case "Stoph": rules.Add(state, stateRules); break;
                default:
                    break;
            }
        }

        for (int step = 0; step < steps; step++)
        {
            curValue = tape[curPos];
            (int ones, int move, string state) rule = rules[curState][curValue];
            ones += rule.ones - curValue;
            tape[curPos] = rule.ones;
            curPos += rule.move;
            if (!tape.ContainsKey(curPos)) tape.Add(curPos, 0);
            curState = rule.state;
        }
        Output = ones.ToString();
    }
}
