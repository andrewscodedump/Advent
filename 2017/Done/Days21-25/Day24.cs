namespace Advent2017;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        List<(int, int)> parts = InputSplit.Select(i => (int.Parse(i.Split('/')[0]), int.Parse(i.Split('/')[1]))).ToList();
        int strongest = 0, longest = 0, strengthOfLongest = 0;
        Queue<(int, int, int, List<(int, int)>)> bfs = new();

        bfs.Enqueue((0, 0, 0, parts));
        do
        {
            (int nextPart, int strengthOfLongest, int longest, List<(int, int)> remaining) state = bfs.Dequeue();
            strongest = Math.Max(strongest, state.strengthOfLongest);
            if (state.longest > longest)
                (longest, strengthOfLongest) = (state.longest, state.strengthOfLongest);
            else if (state.longest == longest)
                strengthOfLongest = Math.Max(strengthOfLongest, state.strengthOfLongest);

            foreach ((int X, int Y) part in state.remaining)
                if (part.X == state.nextPart || part.Y == state.nextPart)
                {
                    parts = new List<(int, int)>(state.remaining);
                    parts.Remove(part);
                    bfs.Enqueue((part.X == state.nextPart ? part.Y : part.X, state.strengthOfLongest + part.X + part.Y, state.longest + 1, parts));
                }
        } while (bfs.Count > 0);

        Output = (WhichPart == 1 ? strongest : strengthOfLongest).ToString();
    }
}
