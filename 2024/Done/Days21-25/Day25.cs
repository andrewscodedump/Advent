namespace Advent2024;

public partial class Day25 : Advent.Day
{
    public override void DoWork()
    {
        List<List<int>> locks = [], keys = [];
        List<int> current = [0, 0, 0, 0, 0];
        bool isKey = false, isLock = false;
        int currentRow = 0;

        foreach (string line in Inputs)
        {
            if (line.Length == 0) { currentRow = 0; continue; }
            if (currentRow == 0) { isLock = line == "#####"; isKey = line == "....."; }
            else if (currentRow == 6)
            {
                if (isKey) keys.Add(current);
                if (isLock) locks.Add(current);
                current = [0, 0, 0, 0, 0];
                isKey = false; isLock = false;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    current[i] += line[i] == '#' ? 1 : 0;
            }
            currentRow++;
        }

        int validPairs = locks.SelectMany(@lock => keys.Select(key => (l: @lock, k:key))).Count(p => p.l.Zip(p.k).All(p => p.First + p.Second <= 5));

        Output = validPairs.ToString();
    }
}