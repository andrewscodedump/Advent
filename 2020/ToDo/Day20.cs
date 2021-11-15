namespace Advent2020;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        List<Tile> tiles = new();
        long result = 1;

        #endregion Setup Variables and Parse Inputs

        Tile tile = new();

        for (int i = 0; i < InputSplit.Length; i++)
        {
            string line = InputSplit[i];
            if (line.StartsWith("Tile"))
            {
                if (i != 0)
                {
                    tile.BuildCombos();
                    tiles.Add(tile);
                    tile = new Tile();
                }
                    tile.Id = int.Parse(line[5..^1]);
            }
            else
            {
                tile.Raw.Add(line);
            }
        }
        tile.BuildCombos();
        tiles.Add(tile);

        List<string> allSides = new();
        foreach (Tile tile1 in tiles)
        {
            allSides.AddRange(tile1.Combos[0].Sides);
            allSides.AddRange(tile1.Combos[2].Sides);
        }

        foreach (Tile tile1 in tiles)
        {

            foreach (string  side in tile1.Combos[0].Sides)
            {
                if (allSides.Where(s => s == side).Count() == 1)
                {
                    tile1.Corner = tile1.Edge;
                    tile1.Edge = !tile1.Corner;
                    if (tile1.Corner) result *= tile1.Id;
                }
            }            
        }

        // build grid
        //  pick corner
        //  iterate through grid, matching r to l / t to b
        // remove edges
        // look for monsters
        // rotate/flip if none
        // count non-monster waves

        Output = result.ToString();
    }

    private class Combo
    {
        private List<string> raw;

        public Combo()
        {
            Raw = new();
            Sides = new string[4];
        }
        public List<string> Raw
        {
            get => raw;
            set
            {
                raw = value;
                if (raw.Count > 0) GetSides();
            }
        }
        public string[] Sides { get; set; }

        private void GetSides()
        {
            string left = string.Empty, right = string.Empty;
            foreach (string line in raw)
            {
                left += line[0];
                right += line.Last();
            }
            Sides = new string[] { raw[0], raw[^1], left, right };
        }

        public Combo Rotate()
        {
            Combo combo = new();
            List<string> newRaw = Enumerable.Repeat(string.Empty, 10).ToList();
            for (int i = 0; i < raw.Count; i++)
                for (int j = 0; j < raw[i].Length; j++)
                {
                    newRaw[j] = raw[i][j] + newRaw[j];
                }
            combo.Raw = newRaw;
            return combo;
        }

        public Combo FlipH()
        {
            Combo combo = new();
            List<string> newRaw = Enumerable.Repeat(string.Empty, 10).ToList();
            for (int i = 0; i < raw.Count; i++)
                newRaw[i] += string.Concat(raw[i].Reverse());
            combo.Raw = newRaw;
            return combo;
        }

        public Combo FlipV()
        {
            Combo combo = new();
            List<string> newRaw = Enumerable.Repeat(string.Empty, 10).ToList();
            for (int i = 0; i < raw.Count; i++)
                newRaw[i] += raw[Math.Abs(i - raw.Count + 1)];
            combo.Raw = newRaw;
            return combo;
        }
    }

    private class Tile
    {
        public Tile()
        {
            Raw = new();
            Combos = new Combo[8];
        }
        public List<string> Raw { get; set; }
        public int Id { get; set; }
        public Combo[] Combos { get; set; }
        public bool Corner { get; set; }
        public bool Edge { get; set; }

        public void BuildCombos()
        {
            Combo combo = new();
            combo.Raw = Raw;
            Combos[0] = combo;
            Combos[1] = Combos[0].Rotate();
            Combos[2] = Combos[1].Rotate();
            Combos[3] = Combos[2].Rotate();
            Combos[4] = Combos[0].FlipH();
            Combos[5] = Combos[4].Rotate();
            Combos[6] = Combos[0].FlipV();
            Combos[7] = Combos[6].Rotate();
        }

    }
}
