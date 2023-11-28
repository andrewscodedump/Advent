namespace Advent2018;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        long[] nodes = InputNumbers[0];
        int pointer = 0;
        long total1 = 0;

        long total2 = ProcessNode(ref pointer, ref total1, nodes);

        Output = (Part1 ? total1 : total2).ToString();
    }

    private long ProcessNode(ref int pointer, ref long total, long[] nodes)
    {
        List<long> children = new();
        long localSum = 0;
        long numChildren = nodes[pointer];
        long numMeta = nodes[pointer + 1];
        pointer += 2;
        for (int child = 0; child < numChildren; child++)
            children.Add(ProcessNode(ref pointer, ref total, nodes));
        for (int pos = 0; pos < numMeta; pos++)
        {
            long val = nodes[pointer];
            total += val;
            localSum += numChildren == 0 ? val : val <= children.Count ? children[(int)val - 1] : 0;
            pointer++;
        }
        return localSum;
    }
}
