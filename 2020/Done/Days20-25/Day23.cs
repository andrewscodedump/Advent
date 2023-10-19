namespace Advent2020;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        Dictionary<long, long> cups = new();
        string cupString = InputSplit[0];
        int turns = int.Parse(InputSplit[1]);
        long current = int.Parse(cupString[0].ToString());
        long[] move = new long[3];

        for (int i = 0; i < cupString.Length; i++)
            cups[int.Parse(cupString[i].ToString())] = int.Parse(cupString[(i + 1) % cupString.Length].ToString());
        if (Part2)
        {
            cups[int.Parse(cupString[^1].ToString())] = 10;
            for (int i = 10; i < 1000000; i++)
                cups.Add(i, i + 1);
            cups[1000000] = current;
        }
        #endregion Setup Variables and Parse Inputs

        for (long turn = 1; turn <= turns; turn++)
        {
            long dest = current - 1;
            if (dest == 0) dest = cups.Count;
            for (int i = 0; i <= 2; i++)
            {
                long id = RemoveAfter(cups, current);
                move[i] = id;
            }

            while (move.Contains(dest))
            {
                dest--;
                if (dest == 0) dest = cups.Count + 3;
            }
            for (int i = 0; i <= 2; i++)
            {
                InsertAfter(cups, dest, move[i]);
                dest = cups[dest];
            }
            current = cups[current];

        }
        string result = string.Empty;
        if (Part1)
        {
            current = 1;
            while (cups[current] != 1)
            {
                result += cups[current].ToString();
                current = cups[current];
            }
        }
        else
            result = (cups[1] * cups[cups[1]]).ToString();

        Output = result;
    }

    private static long RemoveAfter(Dictionary<long, long> dict, long current)
    {
        long idToRemove = dict[current];
        long newPointer = dict[idToRemove];
        dict.Remove(idToRemove);
        dict[current] = newPointer;
        return idToRemove;
    }

    private static void InsertAfter(Dictionary<long, long> dict, long pos, long newID)
    {
        long nextID = dict[pos];
        dict[pos] = newID;
        dict.Add(newID, nextID);
    }

}
