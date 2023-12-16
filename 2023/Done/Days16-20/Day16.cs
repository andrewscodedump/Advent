namespace Advent2023;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        PopulateMapFromInputWithBorders('X', out int width, out int height);
        int maxEnergised = ProcessBeam((-1,0),(1,0));

        if (Part2)
        {
            for (int x = 0; x < width; x++)
            {
                maxEnergised = Math.Max(ProcessBeam((x, -1), (0, 1)), maxEnergised);
                maxEnergised = Math.Max(ProcessBeam((x, height), (0, -1)), maxEnergised);
            }
            for (int y = 0; y < height; y++)
            {
                maxEnergised = Math.Max(ProcessBeam((-1, y), (1, 0)), maxEnergised);
                maxEnergised = Math.Max(ProcessBeam((width, y), (-1, 0)), maxEnergised);
            }
        }
        Output = maxEnergised.ToString();
    }

    private int ProcessBeam((int, int) start, (int, int) direction)
    {
        HashSet<(int, int)> energizedCells = [];
        HashSet<((int, int), (int, int))> knownStates = [];
        List<Beam> beams = [new(start, direction)];
        do
        {
            for (int i = beams.Count - 1; i >= 0; i--)
            {
                if (beams[i].Move(SimpleMap, beams))
                {
                    energizedCells.Add(beams[i].Location);
                    // We've been here before heading in the same direction: infinite loop! - drop this beam
                    if (!knownStates.Add((beams[i].Location, beams[i].Direction)))
                        beams.Remove(beams[i]);
                }
            }
        } while (beams.Count > 0);
        return energizedCells.Count;
    }

    public class Beam()
    {
        public Beam((int, int) location, (int, int) direction) : this()
        {
            Location = location;
            Direction = direction;
        }
        public (int x, int y) Location { get; private set; }
        public (int x, int y) Direction { get; private set; }
        public bool Move(Dictionary<(int x, int y), char> map, List<Beam> beams)
        {
            Location = (Location.x + Direction.x, Location.y + Direction.y);
            switch (map[Location])
            {
                case '.': return true;
                case '/':
                    Direction = (-Direction.y, -Direction.x);
                    return true;
                case '\\':
                    Direction = (Direction.y == 0 ? 0 : Direction.y, Direction.x == 0 ? 0 : Direction.x);
                    return true;
                case '-':
                    if (Direction.y == 0) return true;
                    Direction = (-Direction.y, -Direction.x);
                    beams.Add(new(Location, (-Direction.x, 0)));
                    return true;
                case '|':
                    if (Direction.x == 0) return true;
                    Direction = (Direction.y == 0 ? Direction.y : 0, Direction.x == 0 ? 0 : Direction.x);
                    beams.Add(new(Location, (0, -Direction.y)));
                    return true;
                default:
                    beams.Remove(this);
                    return false;
            }
        }
    }
}