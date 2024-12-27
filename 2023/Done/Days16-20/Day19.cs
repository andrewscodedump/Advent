namespace Advent2023;

public partial class Day19 : Advent.Day
{
    readonly Dictionary<string, Workflow> workflows = [];
    public override void DoWork()
    {
        long total = 0;
        List<Part> parts = [];
        foreach (string input in Inputs)
        {
            if (input == "") continue;
            else if (input.StartsWith('{')) parts.Add(new(input));
            else workflows[input.Split('{')[0]] = new(input);
        }

        if (Part1)
            total = parts.Sum(ProcessPart);
        else
        {
            // Using the workflow rules, split each letter into ranges the members of which will all have the same effect.
            List<int> xRanges = [1, 4001], mRanges = [1, 4001], aRanges = [1, 4001], sRanges = [1, 4001];
            foreach (Workflow workflow in workflows.Values)
            {
                foreach(Rule rule in workflow.Rules)
                {
                    if (rule.Property == 'x') xRanges.Add(rule.Test == '<' ? rule.Value : rule.Value + 1);
                    if (rule.Property == 'm') mRanges.Add(rule.Test == '<' ? rule.Value : rule.Value + 1);
                    if (rule.Property == 'a') aRanges.Add(rule.Test == '<' ? rule.Value : rule.Value + 1);
                    if (rule.Property == 's') sRanges.Add(rule.Test == '<' ? rule.Value : rule.Value + 1);
                }
            }
            xRanges.Sort(); mRanges.Sort(); aRanges.Sort(); sRanges.Sort();

            // We only need to check the first item in each range (for each type)
            for (int x = 0; x < xRanges.Count - 1; x++)
            {
                long xLength = xRanges[x + 1] - xRanges[x] ;
                for (int m = 0; m < mRanges.Count - 1; m++)
                {
                    long mLength = mRanges[m + 1] - mRanges[m] ;
                    for (int a = 0; a < aRanges.Count - 1; a++)
                    {
                        long aLength = aRanges[a + 1] - aRanges[a] ;
                        for (int s = 0; s < sRanges.Count - 1; s++)
                        {
                            long sLength = sRanges[s + 1] - sRanges[s] ;
                            Part part = new(xRanges[x], mRanges[m], aRanges[a], sRanges[s]);
                            int result = ProcessPart(part);
                            if (result != 0)
                                // Number of possibilities is the product of the numbers in each range
                                total += xLength * mLength * aLength * sLength;
                        }
                    }
                }
            }
        }

        Output = total.ToString();
    }

    private int ProcessPart(Part part)
    {
        string workflowLabel = "in";
        int result = 0;
        bool done = false;
        do
        {
            Workflow workflow = workflows[workflowLabel];
            foreach (Rule rule in workflow.Rules)
            {
                workflowLabel = "";
                char property = rule.Property;
                if ((rule.Test == '<' && part.Properties[property] < rule.Value) || (rule.Test == '>' && part.Properties[property] > rule.Value))
                {
                    workflowLabel = rule.Result;
                }
                if (workflowLabel != "") break;
            }
            if (workflowLabel == "") workflowLabel = workflow.Default;
            if (workflowLabel == "R")
            {
                done = true;
            }
            else if (workflowLabel == "A")
            {
                result = part.Properties.Values.Sum();
                done = true;
            }
        } while (!done);
        return result;
    }
    private sealed class Workflow
    {
        public Workflow (string input)
        {
            Label=input.Split('{')[0];
            string rules = input[(Label.Length + 1)..^1];
            Default = rules.Split(',')[^1];
            rules.Split(',')[..^1].ForEach(r => Rules.Add(new Rule(r)));
        }
        public string Label { get; private set; }
        public List<Rule> Rules { get; private set; } = [];
        public string Default { get; private set; }
    }

    private sealed class Rule
    {
        public Rule(string input)
        {
            string[] bits = input.Split('>', '<', ':');
            Property = bits[0][0];
            Test = input[1];
            Value = int.Parse(bits[1]);
            Result = bits[2];
        }
        public char Property { get; private set; }
        public char Test { get; private set; }
        public int Value { get; private set; }
        public string Result { get; private set; }
    }

    private sealed class Part
    {
        public Dictionary<char, int> Properties { get; private set; } = [];
        public Part(string input) => input[1..^1].Split(',').ForEach(p => Properties[p[0]] = int.Parse(p[2..]));
        public Part(int x, int m, int a, int s)
        {
            Properties['x'] = x;
            Properties['m'] = m;
            Properties['a'] = a;
            Properties['s'] = s;
        }

    }
}
