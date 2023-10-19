namespace Advent2021;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        int digits = InputSplit[0].Length, gamma = 0, epsilon = 0;
        List<string> oxyList = new(InputSplit), co2List = new(InputSplit);

        for (int i = 0; i < digits; i++)
        {
            int testBit = (int)Math.Pow(2, digits - i - 1);
            if (OnesCommonest(oxyList, i))
                gamma += testBit;
            else
                epsilon += testBit;
        }

        for (int i = 0; i < digits; i++)
        {
            bool oxyOne = OnesCommonest(oxyList, i), co2One = OnesCommonest(co2List, i);
            for (int j = oxyList.Count - 1; j >= 0 && oxyList.Count > 1; j--)
                if ((oxyOne && oxyList[j][i] == '0') || (!oxyOne && oxyList[j][i] == '1')) oxyList.RemoveAt(j);
            for (int j = co2List.Count - 1; j >= 0 && co2List.Count > 1; j--)
                if ((co2One && co2List[j][i] == '1') || (!co2One && co2List[j][i] == '0')) co2List.RemoveAt(j); ;
        }
        Output = (Part1 ? gamma * epsilon : Convert.ToInt32(oxyList[0], 2) * Convert.ToInt32(co2List[0], 2)).ToString();
    }

    private static bool OnesCommonest(List<string> numbers, int bitPos)
    {
        int bitSum = 0;
        foreach (string num in numbers)
            bitSum += num[bitPos] - 48;
        return (double)bitSum >= (double)numbers.Count / 2;
    }
}
