namespace Advent2018;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        Dictionary<(int, int), (char track, int cart)> tracks = [];
        Dictionary<int, (char, char, int)> carts = [];
        int gridWidth = 0, gridHeight = Inputs.Length, time = 0;
        (int x, int y) finalLocation = (-1, -1);

        for (int y = 0; y < Inputs.Length; y++)
        {
            string line = Inputs[y];
            gridWidth = Math.Max(gridWidth, line.Length);
            for (int x = 0; x < line.Length; x++)
            {
                char track = line[x];
                int cartNo = 0;
                if ("^v<>".Contains(track))
                {
                    cartNo = carts.Count + 1;
                    (char, char, int) cart = (track, 'L', 0);
                    track = track == '>' || track == '<' ? '-' : track == '^' || track == 'v' ? '|' : track;
                    carts.Add(cartNo, cart);
                }
                tracks.Add((x, y), (track, cartNo));
            }
        }
        #endregion Setup Variables and Parse Inputs

        do
        {
            time++;
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    (int, int) currPos = (x, y);
                    int cartNo = tracks[currPos].cart;
                    if (cartNo == 0) continue;
                    (char dirn, char turn, int lastMove) = carts[cartNo];
                    if (lastMove == time) continue;
                    (int x, int y) nextPos = (x + (dirn == '>' ? 1 : dirn == '<' ? -1 : 0), y + (dirn == '^' ? -1 : dirn == 'v' ? 1 : 0));
                    if (tracks[nextPos].cart != 0)
                    {
                        if (Part1)
                        {
                            finalLocation = nextPos;
                            break;
                        }
                        else
                        {
                            carts.Remove(tracks[currPos].cart);
                            carts.Remove(tracks[nextPos].cart);
                            tracks[currPos] = (tracks[currPos].track, 0);
                            tracks[nextPos] = (tracks[nextPos].track, 0);
                        }
                    }
                    else
                    {
                        tracks[currPos] = (tracks[currPos].track, 0);
                        tracks[nextPos] = (tracks[nextPos].track, cartNo);
                        char nextTurn = turn == 'L' ? 'C' : turn == 'C' ? 'R' : 'L';
                        switch (tracks[nextPos].track)
                        {
                            case '-':
                            case '|':
                                carts[cartNo] = (dirn, turn, time);
                                break;
                            case '/':
                                if (dirn == '^') carts[cartNo] = ('>', turn, time);
                                else if (dirn == 'v') carts[cartNo] = ('<', turn, time);
                                else if (dirn == '<') carts[cartNo] = ('v', turn, time);
                                else if (dirn == '>') carts[cartNo] = ('^', turn, time);
                                break;
                            case '\\':
                                if (dirn == '^') carts[cartNo] = ('<', turn, time);
                                else if (dirn == 'v') carts[cartNo] = ('>', turn, time);
                                else if (dirn == '<') carts[cartNo] = ('^', turn, time);
                                else if (dirn == '>') carts[cartNo] = ('v', turn, time);
                                break;
                            case '+':
                                if (turn == 'C') carts[cartNo] = (dirn, nextTurn, time);
                                else
                                    switch (dirn.ToString() + turn.ToString())
                                    {
                                        case "^L": case "vR": carts[cartNo] = ('<', nextTurn, time); break;
                                        case "^R": case "vL": carts[cartNo] = ('>', nextTurn, time); break;
                                        case "<L": case ">R": carts[cartNo] = ('v', nextTurn, time); break;
                                        case "<R": case ">L": carts[cartNo] = ('^', nextTurn, time); break;
                                    }
                                break;
                        }
                    }
                }
                if (finalLocation != (-1, -1)) break;
            }
            if (carts.Count == 1)
                foreach (KeyValuePair<(int, int), (char, int cart)> kvp in tracks)
                    if (kvp.Value.cart != 0)
                    {
                        finalLocation = kvp.Key;
                        break;
                    }
        } while (finalLocation == (-1, -1));

        Output = finalLocation.x.ToString() + "," + finalLocation.y.ToString();
    }
}
