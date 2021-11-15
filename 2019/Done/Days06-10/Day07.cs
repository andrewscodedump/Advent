namespace Advent2019;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        List<int[]> combinations = new();
        int[] combination = new int[5] { 99, 99, 99, 99, 99 };
        int offset = WhichPart == 1 ? 0 : 5;
        long maxPower = 0;
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 5; j++)
                for (int k = 0; k < 5; k++)
                    for (int l = 0; l < 5; l++)
                        for (int m = 0; m < 5; m++)
                        {
                            combination[0] = i + offset; combination[1] = j + offset; combination[2] = k + offset; combination[3] = l + offset; combination[4] = m + offset;
                            if (combination.Length == combination.Distinct().Count()) combinations.Add(combination.ToArray());
                        }

        #endregion Setup Variables and Parse Inputs

        foreach (int[] combo in combinations)
        {
            IntCode[] codes = new IntCode[5] { new IntCode(Input), new IntCode(Input), new IntCode(Input), new IntCode(Input), new IntCode(Input) };
            long power = 0;
            bool firstTime = true;
            do
            {
                for (int i = 0; i < 5; i++)
                {
                    long[] inputs = firstTime ? (new long[] { combo[i], power }) : (new long[] { power });
                    codes[i].RunCodeWithNoReset(inputs);
                    power = codes[i].Output;
                }
                firstTime = false;
            } while (WhichPart == 2 && !codes[4].CodeComplete);
            maxPower = Math.Max(power, maxPower);
        }
        Output = maxPower.ToString();
    }
}
