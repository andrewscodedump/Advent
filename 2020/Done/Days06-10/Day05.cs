namespace Advent2020;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        int maxSeat = 0, minSeat = int.MaxValue;
        HashSet<int> allSeats = Enumerable.Range(0, 1023).ToHashSet();

        foreach (string pass in InputSplit)
        {
            int minRow = 0, maxRow = 127, minCol = 0, maxCol = 7;
            foreach (char letter in pass)
            {
                if (letter == 'F') maxRow -= (maxRow - minRow + 1) / 2;
                if (letter == 'B') minRow += (maxRow - minRow + 1) / 2;
                if (letter == 'L') maxCol -= (maxCol - minCol + 1) / 2;
                if (letter == 'R') minCol += (maxCol - minCol + 1) / 2;
            }
            int currentSeat = (maxRow * 8) + maxCol;
            maxSeat = Math.Max(maxSeat, currentSeat);
            minSeat = Math.Min(minSeat, currentSeat);
            allSeats.Remove(currentSeat);
        }
        allSeats.RemoveWhere(seat => seat < minSeat || seat > maxSeat);
        Output = (WhichPart == 1 ? maxSeat : allSeats.First()).ToString();
    }
}
