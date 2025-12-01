namespace Advent2016;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        if (TestMode && Part2) return;

        List<uint[]> exclusions = [];
        uint firstOne = uint.MaxValue;
        uint count = 0;

        foreach (string item in Inputs)
            exclusions.Add([uint.Parse(item.Split('-')[0]), uint.Parse(item.Split('-')[1])]);

        // Sort the list into numerical order on first number
        exclusions.Sort(delegate (uint[] a, uint[] b) { return a[0].CompareTo(b[0]); });

        // Get rid of complete overlaps
        bool foundOne = false;
        do
        {
            int curr = 0;
            foundOne = false;
            while (curr < exclusions.Count - 1)
            {
                if (exclusions[curr][1] > exclusions[curr + 1][1])
                {
                    foundOne = true;
                    exclusions.RemoveAt(curr + 1);
                }
                curr++;
            }
        } while (foundOne);

        // Check the lower bounds (just in case)
        if (exclusions[0][0] != 0)
            firstOne = 0;

        // Loop round looking for gaps
        for (int i = 0; i < exclusions.Count - 1; i++)
            if (exclusions[i][1] + 1 < exclusions[i + 1][0])
            {
                firstOne = Math.Min(firstOne, exclusions[i][1] + 1);
                count += exclusions[i + 1][0] - exclusions[i][1] - 1;
            }

        // Check the upper bounds (just in case)
        if (firstOne == uint.MaxValue)
            firstOne = exclusions[^1][1];
        if (exclusions[^1][1] < uint.MaxValue)
            count += uint.MaxValue - exclusions[^1][1] - 1;

        Output = Part1 ? firstOne.ToString() : count.ToString();
    }
}
