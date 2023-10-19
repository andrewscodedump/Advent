namespace Advent2018;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        (DateTime time, int guardNo, string action)[] shifts = new(DateTime, int, string)[InputSplit.Length];
        Dictionary<int, int> guardTotals = new();
        Dictionary<(int, int), int> guardMinutes = new();
        char[] separators = new char[] { '[', ']', ' ', '#' };
        int maxMins = 0, maxGuard = 0, maxMin = 0;

        for (int pos = 0; pos < InputSplit.Length; pos++)
        {
            string[] bits = InputSplit[pos].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            shifts[pos] = (DateTime.Parse(bits[0] + "T" + bits[1]),
                bits[2] == "Guard" ? int.Parse(bits[3]) : 0,
                bits[2] == "Guard" ? bits[4] : bits[2]);
        }
        Array.Sort(shifts);
        #endregion Setup Variables and Parse Inputs

        int offset = 1;
        for (int pos = 0; pos < shifts.Length; pos++)
        {
            if (shifts[pos + 1].guardNo != 0) continue;

            int guard = shifts[pos].guardNo;
            DateTime startTime = shifts[pos + offset].time;
            DateTime endTime = shifts[pos + offset + 1].time;

            guardTotals.TryGetValue(guard, out int currTime);
            guardTotals[guard] = currTime += (int)endTime.Subtract(startTime).TotalMinutes;

            for (int minute = startTime.Minute; minute < endTime.Minute; minute++)
            {
                guardMinutes.TryGetValue((guard, minute), out int currMin);
                guardMinutes[(guard, minute)] = currMin += 1;
            }
            if (guardTotals[guard] > maxMins) maxMins = guardTotals[maxGuard = guard];

            pos--;
            offset += 2;
            if (pos + offset + 1 >= shifts.Length) break;
            if (shifts[pos + offset + 1].guardNo != 0)
            {
                pos += offset;
                offset = 1;
            }
        }

        maxMins = 0;
        foreach (KeyValuePair<(int, int), int> kvp in guardMinutes)
            if ((Part2 || kvp.Key.Item1 == maxGuard) && kvp.Value > maxMins)
            {
                if (Part2) maxGuard = kvp.Key.Item1;
                maxMins = kvp.Value;
                maxMin = kvp.Key.Item2;
            }

        Output = (maxMin * maxGuard).ToString();
    }
}
