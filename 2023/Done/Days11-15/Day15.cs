namespace Advent2023;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        int totalResult = 0;
        Dictionary<int, List<Step>> boxes = [];
        foreach (Step step in Input.Split(',').Select(i => new Step(i)))
        {
            if (Part1) totalResult += step.Hash;
            if (step.FocalLength == -1 && boxes.TryGetValue(step.Box, out List<Step> value))
                value.RemoveAll(b => b.LensCode == step.LensCode);
            else if (!boxes.TryGetValue(step.Box, out value))
                boxes!.Add(step.Box, [step]);
            else if (!value.Any(s => s.LensCode == step.LensCode))
                value.Add(step);
            else
                for (int i = 0; i < value.Count; i++)
                    if (value[i].LensCode == step.LensCode)
                        value[i] = step;
        }
        if (Part2)
            foreach (var pair in boxes)
            {
                int result = 0;
                if (pair.Value.Count > 0) { result = 0; }
                for (int i = 0; i < pair.Value.Count; i++)
                    result += (pair.Key + 1) * (i + 1) * pair.Value[i].FocalLength;
                totalResult += result;
            }

         Output = totalResult.ToString();
    }

    private sealed class Step
    {
        public Step(string Input)
        {
            LensCode = Input.Split('=', '-')[0];
            if (Input.Contains('=')) FocalLength = int.Parse(Input.Split('=')[1]);
            Hash = GetHash(Input);
            Box = GetHash(LensCode);
        }
        public int Hash { get; private set; }
        public string LensCode { get; private set; }
        public int Box { get; private set; }
        public int FocalLength { get; private set; } = -1;
        public override string ToString() => $"{LensCode}: Box {Box}, {FocalLength}";
        private static int GetHash(string input)
        {
            int result = 0;
            foreach (char c in input)
            {
                result += c;
                result *= 17;
                result %= 256;
            }
            return result;
        }
    }
}
