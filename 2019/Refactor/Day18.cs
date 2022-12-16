namespace Advent2019;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        int numKeys = 0;
        (int x, int y) startPos = (0, 0);
        for (int y = 0; y < InputSplit.Length; y++)
        {
            string line = InputSplit[y];
            for (int x = 0; x < line.Length; x++)
            {
                SimpleMap[(x, y)] = line[x];
                if (line[x] == '@') startPos = (x, y);
                if ("qwertyuiopasdfghjklzxcvbnm".Contains(line[x])) numKeys++;
            }
        }
        //DrawMap();
        int fewestSteps = Int32.MaxValue;
        Dictionary<((int, int), string), int> beenThere = new() { { (startPos, ""), Int32.MaxValue } };

        #endregion Setup Variables and Parse Inputs

        Queue<((int, int), (int, int), int, string, Dictionary<(int, int), char>)> bfs = new();


        foreach ((int x, int y) in DirectNeighbours)
        {
            (int x, int y) newPos = (startPos.x + x, startPos.y + y);
            if (SimpleMap[newPos] == '#') continue;
            bfs.Enqueue((newPos, startPos, 1, "", new Dictionary<(int, int), char>(SimpleMap)));
        }
        do
        {
            ((int x, int y) pos, (int x, int y) prevPos, int steps, string keysFound, Dictionary<(int, int), char> map) = bfs.Dequeue();
            char whatsThere = map[pos];
            bool pickedUpKey = false;

            //Debug.Print("{0}, {1}: {2}; {3}", pos.x.ToString(), pos.y.ToString(), steps, keysFound);
            // if we've been here before with the same set of keys (and in fewer steps), continue
            ((int, int), string) newHash = (pos, keysFound);
            if (beenThere.ContainsKey(newHash))
                if (steps > beenThere[newHash]) continue;
            beenThere[newHash] = steps;

            // If it's a key, pick it up
            if ("qwertyuiopasdfghjklzxcvbnm".Contains(whatsThere))
            {
                map[pos] = ' ';
                pickedUpKey = true;
                keysFound += whatsThere;
                keysFound = string.Concat(keysFound.OrderBy(c => c));
                if (keysFound.Length == numKeys)
                {
                    fewestSteps = Math.Min(fewestSteps, steps);
                }
            }
            // If it's a door and we don't have the key, stop.  If it's a door and we do have the key, remove from the map
            if ("QWERTYUIOPASDFGHJKLZXCVBNM".Contains(whatsThere))
            {
                if (keysFound.Contains(Convert.ToChar((int)whatsThere + 32)))
                    map[pos] = ' ';
                else
                    continue;
            }
            // If it's a dead end and we haven't picked up a key, stop
            int walls = 0;
            foreach ((int x, int y) in DirectNeighbours)
            {
                walls += map[(pos.x + x, pos.y + y)] == '#' ? 1 : 0;
            }
            if (walls == 3 && !pickedUpKey) continue;
            if (steps >= fewestSteps) continue;

            // queue the next set (after checking)
            foreach ((int x, int y) in DirectNeighbours)
            {
                (int x, int y) newPos = (pos.x + x, pos.y + y);
                // If we're trying to move into a wall, don't bother
                if (map[newPos] == '#') continue;
                // No point in going back to where we've just been unless we've just picked up a key
                if (newPos == prevPos && !pickedUpKey) continue;
                bfs.Enqueue((newPos, pos, steps + 1, keysFound, new Dictionary<(int, int), char>(map)));
            }
        } while (bfs.Count > 0);

        Output = fewestSteps.ToString();
    }

    #region Private Classes and Methods


    #endregion Private Classes and Methods
}
