namespace Everybody2025;

public class Day11 : Advent.Day
{
    public override void DoWork()
    {
        List<long> cols = [.. InputNumbers.Select(n => n[0])];
        int moves, rounds = 0;
        long checkSum = 0;
        //phase 1
        do
        {
            moves = 0;
            for (int i = 0; i < cols.Count - 1; i++)
            {
                if (cols[i] > cols[i + 1])
                {
                    cols[i]--;
                    cols[i + 1]++;
                    moves++;
                }
            }
            if (moves > 0) rounds++;
        } while (moves > 0);
        if (WhichPart == 1)
        {
            //phase 2
            do
            {
                moves = 0;
                for (int i = 0; i < cols.Count - 1; i++)
                {
                    if (cols[i] < cols[i + 1])
                    {
                        cols[i]++;
                        cols[i + 1]--;
                        moves++;
                    }
                }
                if (moves > 0) rounds++;
            } while (rounds < 10);
            checkSum = cols.Select((x, i) => (i + 1) * x).Sum();
        }
        long targetSize = (long)cols.Average();
        long result = cols.Where(n => n > targetSize).Select(n => n - targetSize).Sum();
        Output = WhichPart switch
        {
            1 => checkSum.ToString(),
            _ => (rounds + result).ToString(),
        };
    }
}
