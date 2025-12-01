namespace Advent2015;

public partial class Day17 : Advent.Day
{
    private struct Move { public int NextIndex; public long VolumeSoFar; public int NumberUsed; }
    public override void DoWork()
    {
        int requiredVol = TestMode ? 25 : 150;
        int numCombos = 0;
        int bestNumber = int.MaxValue;
        int numberAtBest = 0;
        List<long> sizes = [];
        InputNumbers[0].ForEach(sizes.Add);
        Queue bfs = new();
        for (int i = 0; i < sizes.Count; i++)
            bfs.Enqueue(new Move { NextIndex = i, VolumeSoFar = 0, NumberUsed = 0 });

        do
        {
            Move move = (Move)bfs.Dequeue();
            if (move.VolumeSoFar + sizes[move.NextIndex] > requiredVol) continue;
            if (move.VolumeSoFar + sizes[move.NextIndex] == requiredVol)
            {
                numCombos++;
                if (move.NumberUsed + 1 < bestNumber)
                {
                    bestNumber = move.NumberUsed + 1;
                    numberAtBest = 1;
                }
                else if (move.NumberUsed + 1 == bestNumber)
                    numberAtBest++;
            }
            for (int i = move.NextIndex + 1; i < sizes.Count; i++)
                bfs.Enqueue(new Move { NextIndex = i, VolumeSoFar = move.VolumeSoFar + sizes[move.NextIndex], NumberUsed = move.NumberUsed + 1 });
        } while (bfs.Count > 0);
        Output = (Part1 ? numCombos : numberAtBest).ToString();
    }
}
