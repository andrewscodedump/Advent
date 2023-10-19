namespace Advent2018;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        int[] nodes = Array.ConvertAll(Input.Split(' '), int.Parse);
        int pointer = 0, total1 = 0, total2 = 0;
        #endregion Setup Variables and Parse Inputs

        total2 = ProcessNode(ref pointer, ref total1, nodes);
        Output = (Part1 ? total1 : total2).ToString();
    }

    #region Private Classes and Methods
    private int ProcessNode(ref int pointer, ref int total, int[] nodes)
    {
        List<int> children = new();
        int localSum = 0;
        int numChildren = nodes[pointer];
        int numMeta = nodes[pointer + 1];
        pointer += 2;
        for (int child = 0; child < numChildren; child++)
            children.Add(ProcessNode(ref pointer, ref total, nodes));
        for (int pos = 0; pos < numMeta; pos++)
        {
            int val = nodes[pointer];
            total += val;
            localSum += numChildren == 0 ? val : val <= children.Count ? children[val - 1] : 0;
            pointer++;
        }
        return localSum;
    }
    #endregion Private Classes and Methods

}
