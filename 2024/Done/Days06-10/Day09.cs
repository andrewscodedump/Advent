namespace Advent2024;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        List<int> disk = Expand(Inputs[0]);
        Defrag(disk, Part2);
        Output = CheckSum(disk).ToString();
    }

    private static List<int> Expand(string layout)
    {
        List<int> expanded = [];
        for (int i = 0; i < layout.Length; i++)
            expanded.AddRange(Enumerable.Repeat(i % 2 == 0 ? i / 2 : -1, layout[i] - 48));
        return expanded;
    }

    private static void Defrag(List<int> disk, bool keepFilesTogether)
    {
        if (!keepFilesTogether)
        {
            do
            {
                int firstSpace = disk.FindIndex(v => v == -1);
                int lastDigit = disk.FindLastIndex(v => v != -1);
                if (firstSpace > lastDigit) break;
                disk[firstSpace] = disk[lastDigit];
                disk[lastDigit] = -1;
            } while (true);
        }
        else
        {
            for (int fileID = disk.Max(); fileID > 0; fileID--)
            {
                (int fileStart, int fileLen) = Findfile(disk, fileID);
                if (TryFindSpace(disk, fileLen, fileStart, out int spaceStart))
                {
                    for (int i = 0; i < fileLen; i++)
                    {
                        disk[spaceStart + i] = fileID;
                        disk[fileStart + i] = -1;
                    }
                }
            }
        }
    }

    private static (int, int) Findfile(List<int> disk, int findWhat)
    {
        int start = disk.FindIndex(v => v == findWhat);
        int end = disk.FindLastIndex(v => v == findWhat);
        int len = end - start + 1;
        return (start, len);
    }

    private static bool TryFindSpace(List<int> disk, int desiredLength, int searchLimit, out int spaceStart)
    {
        int start = 0, end = -1;
        bool foundOne = false;
        do
        {
            start = disk.FindIndex(end + 1, v => v == -1);
            if (start > searchLimit) break;
            end = disk.FindIndex(start, v => v != -1) -1;
            int len = end - start + 1;
            foundOne = len >= desiredLength;
        } while (!foundOne);
        spaceStart = foundOne ? start : -1;
        return foundOne;
    }

    private static long CheckSum(List<int> disk) => disk.Select((val, index) => val == -1 ? 0 : (long)val * index).Sum();
}
