namespace Advent2020;

public partial class Day20 : Advent.Day
{
    private readonly List<Tile> tiles = new();
    private readonly List<string> allSides = new();
    private List<string> bigGrid = new();

    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

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

        foreach (Tile tile1 in tiles)
        {
            allSides.AddRange(tile1.Combos[0].Sides);
            allSides.AddRange(tile1.Combos[2].Sides);
        }

        foreach (Tile tile1 in tiles)
        {
            int matches = 0;
            foreach (string side in tile1.Combos[0].Sides)
            {
                if (IsMatched(side)) matches++;
            }
            if (matches==2) result *= tile1.Id;
        }

        if (WhichPart == 2)
        {
            // build grid
            Queue queue = new();
            //  pick corner
            //   sides[0].notismatched and sides[2].notismatched
            //   add to queue
            queue.Enqueue((FindTopLeftCorner(), 0, 0));
            //  iterate through grid, matching r to l / t to b
            while (queue.Count > 0)
            {
                ((Tile currTile, Combo combo), int x, int y) = (((Tile, Combo), int, int))queue.Dequeue();
                //(Tile currTile, Combo combo) = FindMatch(text, side);
                if (currTile.IsPlaced) continue;
                AddToBigGrid(combo, x, y);
                currTile.IsPlaced = true;
                (Tile nextTile, Combo nextCombo) = FindMatch(combo.Sides[3], 2, currTile.Id);
                if (nextTile != null)
                    queue.Enqueue(((nextTile, nextCombo), x + 1, y));
                (nextTile, nextCombo) = FindMatch(combo.Sides[1], 0, currTile.Id);
                if (nextTile != null)
                    queue.Enqueue(((nextTile, nextCombo), x, y + 1));
            }
            //  create full grid in a new tile object (removing edges)
            Tile bigPicture = new();
            bigPicture.Raw = bigGrid;
            bigPicture.BuildCombos();
            // for each combo
            foreach (Combo combo in bigPicture.Combos)
            {
                result = FindMonsters(combo.Raw);
                if (result != 0) break;
            }
        }

        Output = result.ToString();
    }

    private void AddToBigGrid(Combo combo, int x, int y)
    {
        for (int i = 1; i < combo.Raw.Count - 1; i++)
        {
            if (x == 0)
                bigGrid.Add(combo.Raw[i][1..^1]);
            else
                bigGrid[(y * 8) + (i - 1)] += combo.Raw[i][1..^1];
        }
    }

    private (Tile, Combo) FindMatch(string text, int side, int id)
    {
        foreach(Tile tile in tiles)
        {
            if (tile.Id == id) continue;
            foreach(Combo combo in tile.Combos)
            {
                if(combo.Sides[side] == text)
                    return (tile, combo);
            }
        }
        return (null, null);
    }

    private (Tile, Combo) FindTopLeftCorner()
    {
        foreach(Tile tile in tiles)
        {
            foreach (Combo combo in tile.Combos)
            {
                if (!IsMatched(combo.Sides[0]) && IsMatched(combo.Sides[1]) && !IsMatched(combo.Sides[2]) && IsMatched(combo.Sides[3]))
                    return (tile, combo);
            }
        }
        return (null, null);
    }

    private bool IsMatched(string edge) => allSides.Where(s => s == edge).Count() > 1;

    private static int FindMonsters(List<string> grid)
    {
        // Monster is (21 wide)
        // #                  #     (18 spaces)
        //  #    ##    ##    ###    (4, 4, 4 spaces)
        //   #  #  #  #  #  #       (2, 2, 2, 2, 2 spaces)

        int waves = 0;
        int monsters = 0;
        int size = grid.Count;
        for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
            {
                if (grid[y][x] != '#') continue;
                waves++;
                if(x>size-19 || y>size-1 || y==0) continue;
                if (grid[y-1][x + 18] != '#') continue;
                if (grid[y][x + 5] != '#' || grid[y][x + 6] != '#' || grid[y][x + 11] != '#' || grid[y][x + 12] != '#' || grid[y][x + 17] != '#' || grid[y][x + 18] != '#' || grid[y][x + 19] != '#') continue;
                if (grid[y + 1][x + 1] != '#' || grid[y+1][x + 4] != '#' || grid[y + 1][x + 7] != '#' || grid[y + 1][x + 10] != '#' || grid[y + 1][x + 13] != '#' || grid[y + 1][x + 16] != '#') continue;


                grid[y - 1] = grid[y-1][..(x + 18)] + "O" + grid[y-1][(x + 18 + 1)..^0];
                grid[y] = grid[y][..x] + "O" + grid[y][(x + 1)..^0];
                grid[y] = grid[y][..(x + 5)] + "O" + grid[y][(x + 5 + 1)..^0];
                grid[y] = grid[y][..(x + 6)] + "O" + grid[y][(x + 6 + 1)..^0];
                grid[y] = grid[y][..(x + 11)] + "O" + grid[y][(x + 11 + 1)..^0];
                grid[y] = grid[y][..(x + 12)] + "O" + grid[y][(x + 12 + 1)..^0];
                grid[y] = grid[y][..(x + 17)] + "O" + grid[y][(x + 17 + 1)..^0];
                grid[y] = grid[y][..(x + 18)] + "O" + grid[y][(x + 18 + 1)..^0];
                grid[y] = grid[y][..(x + 19)] + "O" + grid[y][(x + 19 + 1)..^0];

                grid[y + 1] = grid[y + 1][..(x + 1)] + "O" + grid[y + 1][(x + 1 + 1)..^0];
                grid[y + 1] = grid[y + 1][..(x + 4)] + "O" + grid[y + 1][(x + 4 + 1)..^0];
                grid[y + 1] = grid[y + 1][..(x + 7)] + "O" + grid[y + 1][(x + 7 + 1)..^0];
                grid[y + 1] = grid[y + 1][..(x + 10)] + "O" + grid[y + 1][(x + 10 + 1)..^0];
                grid[y + 1] = grid[y + 1][..(x + 13)] + "O" + grid[y + 1][(x + 13 + 1)..^0];
                grid[y + 1] = grid[y + 1][..(x + 16)] + "O" + grid[y + 1][(x + 16 + 1)..^0];

                waves-=2;
                monsters++;
            }
        if (monsters>0)
            PrintGrid(grid);

        return monsters > 0 ? waves : 0;
        //return monsters > 0 ? waves - (monsters * 15) : 0;
    }

    private static void PrintGrid(List<string> grid)
    {
        int size = grid.Count;
        for (int y = 0; y < size; y++)
        {
            string line = "";
            for (int x = 0; x < size; x++)
            {
                if (line != "") line+= "\t";
                line+= grid[y][x];
            }
            Debug.Print(line);
        }
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
            List<string> newRaw = Enumerable.Repeat(string.Empty, raw.Count).ToList();
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
            List<string> newRaw = Enumerable.Repeat(string.Empty, raw.Count).ToList();
            for (int i = 0; i < raw.Count; i++)
                newRaw[i] += string.Concat(raw[i].Reverse());
            combo.Raw = newRaw;
            return combo;
        }

        public Combo FlipV()
        {
            Combo combo = new();
            List<string> newRaw = Enumerable.Repeat(string.Empty, raw.Count).ToList();
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
        public bool IsCorner { get; set; }
        public bool IsEdge { get; set; }
        public bool IsPlaced { get; set; }
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
