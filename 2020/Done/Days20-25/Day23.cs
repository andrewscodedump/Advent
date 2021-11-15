﻿namespace Advent2020;

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
        if (WhichPart == 2)
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
        if (WhichPart == 1)
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

    #region Old Way

    private void DoWorkOld()
    {
        #region Setup Variables and Parse Inputs

        string cupString = InputSplit[0];
        int turns = int.Parse(InputSplit[1]);
        LinkedList<int> cups = new(cupString.ToCharArray().Select(i => int.Parse(i.ToString())).ToArray());
        if (WhichPart == 2)
            for (int i = 10; i <= 100; i++)
                cups.AddLast(i);

        #endregion Setup Variables and Parse Inputs

        LinkedListNode<int> current = cups.First;
        int[] move = new int[3];
        LinkedListNode<int> toMove = cups.First;

        for (long turn = 1; turn <= turns; turn++)
        {
            int dest = current.Value - 1;
            if (dest == 0) dest = cups.Count;
            for (int i = 0; i <= 2; i++)
            {
                toMove = LoopNext(current);
                move[i] = toMove.Value;
                cups.Remove(toMove);
            }

            toMove = null;
            while (move.Contains(dest))
            {
                dest--;
                if (dest == 0) dest = cups.Count + 3;
            }
            toMove = cups.Find(dest);
            //toMove = cups.First;  -- To test if First was the cause of the slowness.
            for (int i = 0; i <= 2; i++)
            {
                cups.AddAfter(toMove, move[i]);
                toMove = LoopNext(toMove);
            }
            current = LoopNext(current);

            // Trying (vainly) to find a pattern
            //cupString = string.Join("\t", cups);
            //Debug.Print("{0}\t{1}", turn, cupString);

        }
        string result = string.Empty;
        current = cups.Find(1);
        if (WhichPart == 1)
        {
            for (int i = 0; i < cups.Count - 1; i++)
            {
                current = LoopNext(current);
                result += current.Value.ToString();
            }
        }
        else
            result = (LoopNext(current).Value * LoopNext(LoopNext(current)).Value).ToString();
        Output = result;
    }

    private static LinkedListNode<int> LoopNext(LinkedListNode<int> current)
    {
        return current.Next ?? current.List.First;
    }

    #endregion Old Way
}
