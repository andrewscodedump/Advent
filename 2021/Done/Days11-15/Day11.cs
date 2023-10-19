namespace Advent2021;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        int rounds = 100, flashes = 0;
        Dictionary<(int, int), Octopus> map = FillMapWithBorders(InputSplit);

        int round = 0;
        bool allFlash = false;
        do
        {
            int interimFlashes;
            foreach ((int, int) pos in map.Keys) map[pos].Energise();
            do
            {
                interimFlashes = 0;
                Dictionary<(int, int), Octopus> newMap = CopyMap(map);
                foreach ((int x, int y) in newMap.Keys.Where(p => !newMap[p].IsDummy))
                {
                    interimFlashes += newMap[(x, y)].TryFire() ? 1 : 0;
                    foreach ((int dx, int dy) in Neighbours)
                        if (map[(x + dx, y + dy)].Energy >= 10) newMap[(x, y)].Energise();
                }
                flashes += interimFlashes;
                map = CopyMap(newMap);
            } while (interimFlashes > 0);
            foreach ((int x, int y) in map.Keys.Where(p => !map[p].IsDummy))
                    map[(x, y)].Reset();
            round++;
            allFlash = map.Where(o=>o.Value.Energy==0).Count() == map.Count;
        } while ((Part1 && round < rounds) || (Part2 && !allFlash));

        Output = (Part1 ? flashes : round).ToString();
    }
     
    private class Octopus
    {
        public int Energy { get; private set; }
        public bool IsSpent { get; private set; }
        public bool IsDummy { get; private set; }

        public Octopus(int energy,bool isSpent,bool isDummy)
        {
            Energy = energy;
            IsSpent = isSpent;
            IsDummy = isDummy;
        }

        public void Energise()
        {
            if (!IsDummy) Energy++;
        }
        public bool TryFire()
        {
            if (Energy < 10) return false;
            Energy = 0;
            IsSpent = true;
            return true;
        }

        public void Reset()
        {
            if(IsSpent) Energy = 0;
            IsSpent = false;
        }

        public Octopus DeepCopy() => new(Energy, IsSpent, IsDummy);
    }
    private static Dictionary<(int, int), Octopus> FillMapWithBorders(string[] inputs)
    {
        Dictionary<(int, int), Octopus> map = new();
        int height = inputs.Length, width = inputs[0].Length;
        for (int y = -1; y <= height; y++)
            for (int x = -1; x <= width; x++)
                map[(x, y)] = y == -1 || x == -1 || y == height || x == width ? new(0, false, true) : new(int.Parse(inputs[y][x].ToString()), false, false);
        return map;
    }

    private static Dictionary<(int, int), Octopus> CopyMap(Dictionary<(int, int), Octopus> old)
    {
        Dictionary<(int, int), Octopus> copy = new();
        foreach ((int, int) pos in old.Keys)
            copy[pos] = old[pos].DeepCopy();
        return copy;
    }
}
