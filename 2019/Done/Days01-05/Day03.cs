namespace Advent2019;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        string[][] wires = [Input.Split(','), Inputs[1].Split(',')];
        Dictionary<(int, int), int>[] wirePaths = [[], []];

        for (int wire = 0; wire < wires.Length; wire++)
        {
            (int x, int y) pos = (0, 0);
            int distance = 0;
            foreach (string move in wires[wire])
            {
                (int x, int y) = Directions[move[0]];
                for (int i = int.Parse(move[1..]); i > 0; i--)
                {
                    distance++;
                    pos.x += x; pos.y += y;
                    wirePaths[wire].TryAdd(pos, distance);
                }
            }
        }
        List<(int x, int y)> intersects = wirePaths[0].Keys.Intersect(wirePaths[1].Keys).ToList();
        Output = (Part1 ? intersects.Min(p => Math.Abs(p.x) + Math.Abs(p.y)) : intersects.Min(p => wirePaths[0][p] + wirePaths[1][p])).ToString();
    }
}
